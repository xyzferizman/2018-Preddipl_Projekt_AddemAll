using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour {

    
    public InputField answer;
    public Text[] quest;
    public Animator[] anims;
    public InputField result;
    public GameObject donji;



    private int size;
    private int[] set;//oznaka pitanja koje je na kojem polju, tj. panelu
    private bool[] active;//zastavice za provjeru radi li animacija
    private DateTime now;
    private List<Task> tasks;
    public InputField expirience;


    private int level;
    private int strenght;
    private int year;
    private int subject;
    private bool inplay = false;
    private Player player;

    private int victory;

    private int current;

    private bool done = false;
    private bool win = false;
    private bool noquestions = false;





    
    void Start () {
             
        player =(Player) GlobalVariables.user;
        expirience.text ="Bodovi: " +player.Expirience;
        size = quest.Length;
        active = new bool[size];
        set = new int[size];

    }
	
	void Update () {
        if(inplay)
        {

            Schedule();
            //provjera dali su postavljena pitanja, ako ne pošalji
            for (int i = 0; i< size; i++)
            {
                if(!active[i])
                {
                    anims[i].speed = 0.5f; 
                    anims[i].gameObject.SetActive(true);
                    anims[i].enabled = true;
                    anims[i].StartRecording(0);
                    active[i] = true;
                }
            }
            //kada se odgovori na pitanje
            if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) && answer.text != "")
            {
                //provjera točnosti
                now = DateTime.Now;
                bool incorrect = true;
                List<Task> forDataBase = new List<Task>();
                for(int i = 0; i < size; i++ )
                {
                    if(set[i]== 0)
                    {
                        continue;
                    }
                    if(incorrect)
                    {
                        forDataBase.Add(tasks[set[i]-1]);
                    }
                    if(tasks[set[i]-1].isCorrectAnswer(answer.text))
                    {
                        //Debug.Log(tasks[set[i] - 1].Problem + "=" + answer.text+"|"+i);
                        anims[i].StopRecording();
                        anims[i].gameObject.SetActive(false);
                        
                        current++;
                        result.text = current + "/" + victory;
                        player.AddCorrect(Level.GetLevelId(subject, level));
                        if (incorrect)
                        {
                            incorrect = false;
                            forDataBase = new List<Task>();
                            forDataBase.Add(tasks[set[i] - 1]);
                        }
                        else
                        {
                            forDataBase.Add(tasks[set[i] - 1]);
                        }
                        
                        set[i] = 0;

                        active[i] = false;
                        if (current == victory)
                        {
                            Win();
                        }

                    }
                }
                //ako smo unijeli netočan odgovor, bodovi se smanjuju za 1
                if(incorrect)
                {
                    current--;
                    
                    result.text = current + "/" + victory;
                    if (current == 0)
                    {
                        GameOver();
                    }

                }
                foreach(Task t in forDataBase)
                {
                    StartCoroutine(sendStatistics(t,answer.text));
                    if(incorrect)
                    {
                        player.AddIncorrect(Level.GetLevelId(subject, level));
                    }
                }
                answer.text = "";
                answer.ActivateInputField();

            }
            for (int i = 0; i< size; i++)//provjera da li su pitanja došla do kraja
            {
                now = DateTime.Now;
                if(anims[i].GetCurrentAnimatorStateInfo(0).normalizedTime>1)
                {
                    current--;

                    result.text = current + "/" + victory;
                    if (current == 0)
                    {
                        GameOver();
                    }
                    anims[i].StopRecording();
                    anims[i].gameObject.SetActive(false);
                    set[i] = 0;
                    active[i] = false;
                    player.AddIncorrect(Level.GetLevelId(subject, level));
                    StartCoroutine(sendStatistics(tasks[set[i]], ""));
                }
            }

        }
        
       

    }

    internal void Read()
    {
        done = false;
        win = false;
    }

    private IEnumerator sendStatistics(Task t, string answered)
    {
            WWWForm form = new WWWForm();
            form.AddField("user", GlobalVariables.user.Getid());
            form.AddField("question", t.Id);
            form.AddField("answered", answered);
            form.AddField("time", now.ToString("yyyy-MM-dd HH:mm:ss"));

            WWW www = new WWW(GlobalVariables.LoginURL + "insertstatistics.php", form);
            yield return www;
            //Debug.Log(www.text);
    }

    private void Win()
    {
        inplay = false;
        for (int i = 0; i < size; i++)
        {
            anims[i].StopRecording();
            anims[i].gameObject.SetActive(false);
            set[i] = 0;
            active[i] = false;
        }
        player.Expirience += 5;
        expirience.text = "Bodovi: " + player.Expirience;
        answer.gameObject.SetActive(false);
        donji.SetActive(false);
        //this.GetComponent<Canvas>().enabled = false;
        done = true;
        win = true;

    }

    private void GameOver()
    {
        inplay = false;
        for(int i = 0; i< size; i++)
        {
            anims[i].StopRecording();
            anims[i].gameObject.SetActive(false);
            set[i] = 0;
            active[i] = false;
        }
        answer.gameObject.SetActive(false);
        //answer.gameObject.SetActive(false);   // nepotrebno jer donji.setactive radi istu stvar 
        donji.SetActive(false);
        //this.GetComponent<Canvas>().enabled = false;
        done = true;
    }


    public void LoadData()
    {
        level = GlobalVariables.order;
        strenght = GlobalVariables.strenght;
        subject = GlobalVariables.subject;

        tasks = new List<Task>();
        if (strenght == 2 || strenght == 4 || strenght == 5)
        {
            for (int i = 1; i <= strenght; i++)
            {
                foreach (Task t in Questions.getListQuestion(subject, level, i))
                {
                    tasks.Add(t);
                }
            }
        }
        else
        {
            tasks = Questions.getListQuestion(subject, level, strenght);
        }
        if (tasks == null || tasks.Count == 0)
        {
            noquestions = true;
            return;
        }
    }

    public void Play()
    {
        
        
        victory = GlobalVariables.victory;
        current = GlobalVariables.current;
        result.text = current + "/" + victory;
        
        answer.enabled = true;
        
        answer.gameObject.SetActive(true);
        answer.Select();
        inplay = true;

        
        
    }

    public bool NoQuestions()
    {
        return noquestions;
    }
        

    public bool Done()
    {
        return done;
    }

    public bool isWin()
    {
        return win;
    }



    private void Schedule()
    {
        if (tasks == null||tasks.Count<3)
        {
            for (int i = 0; i < size; i++)
            {
                quest[i].text = "Nepoznato";
            }
            return;
        }
        int sizeOfTask = tasks.Count;
        for (int i = 0; i< size; i++)
        {
            //Debug.Log("postavljeno: " + set[i]);
            if(set[i]==0)
            {
                
                int numberOfQuestion = 0;
                System.Random random = new System.Random();
                numberOfQuestion = random.Next(1, sizeOfTask+1);
                //Debug.Log("numberOFQuesiton: " + numberOfQuestion);
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                    {
                            continue;
                    }
                    if(numberOfQuestion == set[j])
                    {
                        numberOfQuestion++;
                        if(numberOfQuestion >= tasks.Count)
                        {
                            numberOfQuestion = 1;
                        }
                        j = 0;
                    }
                }
                set[i] = numberOfQuestion;
                Debug.Log(strenght+" pitanje broj: "+tasks.Count+" "+numberOfQuestion+" "+i);
                quest[i].text = tasks[numberOfQuestion-1].Problem;
            }

            sizeOfTask--;
        }
        
    }


    
    
}


    
