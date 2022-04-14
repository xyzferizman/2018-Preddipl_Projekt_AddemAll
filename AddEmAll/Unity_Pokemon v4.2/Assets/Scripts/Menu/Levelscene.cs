using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levelscene : MonoBehaviour {

    public Dropdown subject;
    public Button[] buttons;//moraju biti 4!
    public Button[] change;//moraju biti 2
    public Button[] move;//moraju bit 2
    public Button enableAdd;
    public InputField inputAdd;

    private List<string> listSubjects;
    private List<string> listLevels;
    private Administrator admin = (Administrator) GlobalVariables.user;
    private bool canEdit = false;
    private int select;
    private int selectLevel;
    private bool newLevel;
    private bool repeat;
    private bool adding;
    private bool update = false;





    // Use this for initialization
    public void Start () {
        
        listSubjects = admin.getSubjects(true);
        subject.AddOptions(listSubjects);
        listLevels = Level.getLevelForSubject(Int32.Parse(subject.captionText.text.Split('-')[0]));
        if (listLevels.Count < 4)
        {
            selectLevel = 0;
        }
        else selectLevel = 1;

        select = 1;

        changeButtons();
        buttons[select].Select();
        buttons[select].interactable = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDown();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveUp();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            ChangeLevelUp();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            ChangeLevelDown();
        }
        if(repeat)
        {
            EnableAddLevel();
            repeat = false;
        }
        if(adding)
        {
            listLevels = Level.getLevelForSubject(Int32.Parse(subject.captionText.text.Split('-')[0]));
            if (listLevels.Count < 4) selectLevel = 0;
            else selectLevel = 1;

            select = 1;
            changeButtons();
            buttons[select].interactable = false;
            adding = false;
        }
    }

    public void changeButtons()
    {
        if (subject.captionText.text != "")
        {
            listLevels = Level.getLevelForSubject(Int32.Parse(subject.captionText.text.Split('-')[0]));
        }
        else
        {
            return;
        }
        if (listLevels.Count < 2)
        {
            for (int i = 0; i < 2; i++)
            {
                move[i].gameObject.SetActive(false);
                change[i].gameObject.SetActive(true);
                change[i].interactable = false;
            }
        }
        else
        {
            if(selectLevel == 0)
            {
                move[0].gameObject.SetActive(false);

                change[0].gameObject.SetActive(true);
                change[0].interactable = false;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    move[i].gameObject.SetActive(true);
                    change[i].gameObject.SetActive(true);
                    change[i].interactable = true;
                }
            }
        }
        shedule();
    }



    private void shedule()
    {
        int position = select;
        int numberOfElements = listLevels.Count;
        //ako nema niti jedne razine za zadani objekt, isključi sve gumbe
        if (listLevels == null || numberOfElements == 0 )
        {

            for(int i = 0; i< buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
        
        //ako postoji samo jedna razina, 3 gumba isključi
        else if(numberOfElements == 1)
        {
            for(int i = 0; i< buttons.Length; i++)
            {

                if(i != position)
                {
                    buttons[i].gameObject.SetActive(false);
                }
                else
                {
                    string data = listLevels[0];
                    string[] datas = data.Split('-');
                    buttons[i].GetComponentInChildren<Text>().text = datas[0] + "/" + numberOfElements + "  " + datas[1];
                    buttons[i].interactable = false;
                    buttons[i].Select();
                    select = i;//pamtimo koji gumb je selektiran
                }
            }
        }
        else if(numberOfElements == 2)
        {
            for(int i = 0; i< buttons.Length; i++)
            {
                if(i == position)
                {
                    buttons[i].gameObject.SetActive(true);
                    string data = listLevels[0];
                    string[] datas = data.Split('-');
                    buttons[i].GetComponentInChildren<Text>().text = datas[0] + "/" + numberOfElements + "  " + datas[1];
                    
                }
                else if(i == position+1)
                {
                    buttons[i].gameObject.SetActive(true);
                    string data = listLevels[i - 1];
                    string[] datas = data.Split('-');
                    buttons[i].GetComponentInChildren<Text>().text = datas[0] + "/" + numberOfElements + "  " + datas[1];
                }
                else
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }
        //tri uključena, jedan isključen
        else if(numberOfElements == 3)
        {
            for(int i = 0; i< buttons.Length; i++)
            {
                if (i >= position)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].interactable = true;
                    string data = listLevels[i-position];
                    string[] datas = data.Split('-');
                    buttons[i].GetComponentInChildren<Text>().text = datas[0]+"/"+numberOfElements+"  "+datas[1];
                    
                }
                else
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }

        }
        //svi uključeni
        else
        {
            for(int i = 0; i< buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].interactable = true;
                Debug.Log("broj u listi:" + (selectLevel - select + i));
                string data = listLevels[selectLevel-select+i];
                string[] datas = data.Split('-');
                buttons[i].GetComponentInChildren<Text>().text = datas[0] + "/" + numberOfElements + "  " + datas[1];
            }
        }
    }


    


    public void ChangeLevelUp()
    {
        if(update)
        {
            return;
        }

        if(select == 0)
        {
            return;
        }

        if(selectLevel == 0)
        {
            return;
        }
        else if(selectLevel == listLevels.Count-1)
        {
            move[1].gameObject.SetActive(true);
            change[1].interactable = true;
        }
        int position = Int32.Parse(listLevels[selectLevel].Split('-')[0]);
        int subject = Int32.Parse(this.subject.captionText.text.Split('-')[0]);
        Level.change(subject, position - 1, position);
        StartCoroutine(Change(position-1, position));
        listLevels = Level.getLevelForSubject(subject);
        select--;
        selectLevel--;
        shedule();
        if(selectLevel==0)
        {
            move[0].gameObject.SetActive(false);
            change[0].interactable = false;
        }
        
        buttons[select].interactable = false;
        buttons[select].Select();



    }

    

    public void ChangeLevelDown()
    {
        if(update)
        {
            return;
        }
        if(selectLevel == listLevels.Count-1)
        {
            return;
        }
        if(select == 3)
        {
            return;
        }
        else if(selectLevel == 0)
        {
            move[0].gameObject.SetActive(true);
            change[0].interactable = true;
        }
        int position = Int32.Parse(listLevels[selectLevel].Split('-')[0]);
        int subject = Int32.Parse(this.subject.captionText.text.Split('-')[0]);
        Level.change(subject, position , position+1);
        StartCoroutine(Change(position, position+1));
        listLevels = Level.getLevelForSubject(subject);
        select++;
        selectLevel++;
        shedule();
        
        if(selectLevel == listLevels.Count-1)
        {
            move[1].gameObject.SetActive(false);
            change[1].interactable = false;
        }
        
        buttons[select].interactable = false;
        buttons[select].Select();

    }

    public void moveDown()
    {
        if(update)
        {
            return;
        }
        if (selectLevel < listLevels.Count-1)
        {
            selectLevel++;
            move[0].gameObject.SetActive(true);
            change[0].interactable = true;
            if(selectLevel == listLevels.Count-1)
            {
                move[1].gameObject.SetActive(false);
                change[1].interactable = false;
            }
            
            if (listLevels.Count-selectLevel <= (3-select) || select == 0 )
            {
                buttons[select].interactable = true;
                select++;
                buttons[select].interactable = false;
                buttons[select].Select();

            }
            else
            {
                for(int i = 0; i< buttons.Length; i++)
                {
                    if(i == buttons.Length-1)
                    {
                        string data = listLevels[selectLevel + 3- select];
                        string[] datas = data.Split('-');
                        buttons[i].GetComponentInChildren<Text>().text = datas[0] + "/" + listLevels.Count + "  " + datas[1];
                    }
                    else
                    {
                        buttons[i].GetComponentInChildren<Text>().text = buttons[i + 1].GetComponentInChildren<Text>().text;
                    }
                }
            }
        }
        
    }

    public void moveUp()
    {
        if(update)
        {
            return;
        }
        if (selectLevel > 0)
        {
            selectLevel--;
            move[1].gameObject.SetActive(true);
            change[1].interactable = true;
            if (selectLevel == 0)
            {
                move[0].gameObject.SetActive(false);
                change[0].interactable = false;
            }
            if (selectLevel+1 <= select || select == 3 )
            {
                buttons[select].interactable = true;
                select--;
                buttons[select].interactable = false;
                buttons[select].Select();

            }
            else
            {
                for (int i = buttons.Length-1 ; i >=0 ; i--)
                {
                    if (i == 0)
                    {
                        string data = listLevels[selectLevel-4+select];
                        string[] datas = data.Split('-');
                        buttons[i].GetComponentInChildren<Text>().text = datas[0] + "/" + listLevels.Count + "  " + datas[1];
                    }
                    else
                    {
                        buttons[i].GetComponentInChildren<Text>().text = buttons[i - 1].GetComponentInChildren<Text>().text;
                    }
                }
            }
        }

    }

    public void EnableAddLevel()
    {
        if (!newLevel)
        {
            enableAdd.GetComponentInChildren<Text>().text = "Spremi";
            newLevel = true;
            for(int i = 0; i< 2; i++)
            {
                move[i].gameObject.SetActive(false);
                change[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < 3; i++)
            {
                int size = listLevels.Count;
                if (size- 1 - i >= 0)
                {
                    string data = listLevels[size - 1 - i];
                    string[] datas = data.Split('-');
                    buttons[2 - i].gameObject.SetActive(true);
                    buttons[2-i].GetComponentInChildren<Text>().text = datas[0] + "/" + size + "  " + datas[1]; 
                }
                else
                {
                    buttons[2 - i].gameObject.SetActive(false);
                }
            }
            buttons[3].gameObject.SetActive(false);
            inputAdd.gameObject.SetActive(true);
            inputAdd.Select();
        }
        else
        {

            enableAdd.GetComponentInChildren<Text>().text = "Dodaj Razinu";
            newLevel = false;
            StartCoroutine(InsertLevel(inputAdd.text));
            inputAdd.gameObject.SetActive(false);
            changeButtons();

        }

    }

    
    // Update is called once per frame


    public void Back()
    {
        if (newLevel)
        {
            enableAdd.GetComponentInChildren<Text>().text = "Dodaj Razinu";
            newLevel = false;
            buttons[3].gameObject.SetActive(true);
            inputAdd.gameObject.SetActive(false);
            changeButtons();
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }


    private IEnumerator Change(int position1, int position2)
    {
        update = true;
        WWWForm form = new WWWForm();
        form.AddField("subject", subject.captionText.text.Split('-')[0]);
        form.AddField("position1", position1);
        form.AddField("position2", position2);



        WWW www = new WWW(GlobalVariables.LoginURL + "changeLevels.php", form);
        yield return www;
        update = false;
    }

    private IEnumerator InsertLevel(string name)
    {
        
        update = true;
        int subject = Int32.Parse(this.subject.captionText.text.Split('-')[0]);
        int order = Level.getOrder(subject);
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("subject", subject);
        form.AddField("order", order+1);


        WWW www = new WWW(GlobalVariables.LoginURL + "insertLevel.php", form);
        yield return www;
        Debug.Log("id levela je: " + www.text);
        if (www.text == "postoji")
        {
            inputAdd.text = "ovo ime već postoji";
            yield return new WaitForSecondsRealtime(2);
            inputAdd.text = "";
            repeat = true;
        }
        else
        {
            Level.AddLevel(Int32.Parse(www.text), name, subject, order+1);
            adding = true;
        }
        update = false;
    }

}
