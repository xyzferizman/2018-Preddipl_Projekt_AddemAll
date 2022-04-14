using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quest : MonoBehaviour {

    public PlayerMovement playerMovementScript;
    public Battle battleScript;
    public float sekundeCekanje = 5f;
    public Text introText;
    public Text winningText;
    public Text losingText;
    public Button readyButton;
    public Text readyButtonText;
    public GameObject donjiPanel;
    public Text questNotEligibleText;

    //bool pobijedio = true;                 // OVO BI MOGLO RADIT PROBLEME
    // OVO JE ZA DEBUG
    public InputField inputField;
    public InputField resultField;

    private bool ingame = false;

    private int thisQuest;
    private bool restart;

    private void Start()
    {
        readyButton.onClick.AddListener(StartBattle);
        playerMovementScript.gameObject.transform.position = GlobalVariables.position[GlobalVariables.lastQuestCompleted];

    }

    private void Update()
    {
        if (ingame)
        {
            if (thisQuest == GlobalVariables.lastQuestCompleted + 1)
            {


                if (battleScript.Done())
                {
                    if (battleScript.isWin())
                    {
                        battleScript.Read();

                        ingame = false;

                        GlobalVariables.lastQuestCompleted++;
                        StartCoroutine(QuestCompleted(sekundeCekanje));

                    }
                    else
                    {
                        battleScript.Read();
                        StartCoroutine(QuestFailed(sekundeCekanje));
                        ingame = false;
                    }
                }
            }
            
        }
        if (GlobalVariables.lastQuestCompleted == 0)
        {
            this.GetComponent<CircleCollider2D>().enabled = true;
        }

    }

    IEnumerator QuestFailed(float cekanje)
    {
        losingText.gameObject.SetActive(true);
        losingText.enabled = true;
        yield return new WaitForSecondsRealtime(cekanje);
        losingText.enabled = false;
        losingText.gameObject.SetActive(false);
        playerMovementScript.enabled = true;
        ingame = false;

    }

    void OnTriggerEnter2D()  // ako triggeran
    {

        string activatedTriggerName = gameObject.name;
        string lastChar = activatedTriggerName.Substring(activatedTriggerName.Length - 1);

        

        if (int.TryParse(lastChar, out thisQuest)) {}
        else Debug.Log("BUG : String could not be parsed.");

        Debug.Log("lastComplited: "+GlobalVariables.lastQuestCompleted+", thisQuest:  "+ thisQuest);
        //Debug.Log("parsiranje gotovo , thisQuest = '" + thisQuest + "'");

        //Debug.Log("triggeran je sljedeci trigger : " + gameObject.name);

        // trebam znati koji quest trigger je triggeran , i koji je zadnji quest completan

        // provjerit jeli eligible za izvrsit 
        // quest 1 je uvijek eligible, al za svaki slucaj , zasad ovdje if 


        if ((GlobalVariables.lastQuestCompleted +1) == thisQuest) Debug.Log("ovaj quest JE eligible za napravit");
        else
        {
            Debug.Log("ovaj quest NIJE ELIGIBLE za napravit");
            
            // disable movement , pokazi message , pricekaj par sekundi , makni message , enable movement
            StartCoroutine(QuestUneligible(sekundeCekanje));
            

            // ne nastavljamo izvođenje , posto je quest ineligible
            return;
        }
        // disable movement na pocetku questa
        playerMovementScript.enabled = false;

        // par sekundi za tekst
        StartCoroutine(QuestStart(sekundeCekanje));

    }

    IEnumerator QuestUneligible(float cekanje)
    {
        playerMovementScript.enabled = false;
        questNotEligibleText.enabled = true;
        yield return new WaitForSecondsRealtime(cekanje);
        questNotEligibleText.enabled = false;
        playerMovementScript.enabled = true;
    }

    IEnumerator QuestStart(float cekanje)
    {
        //Debug.Log("poceo cekanje");
        battleScript.LoadData();
        if (GlobalVariables.lastQuestCompleted == 1 || GlobalVariables.lastQuestCompleted == 3)
        {
            GlobalVariables.current = 5;
            GlobalVariables.victory = 10;
        }
        else if (GlobalVariables.lastQuestCompleted == 4)
        {
            GlobalVariables.current = 5;
            GlobalVariables.victory = 20;
        }
        else
        {
            GlobalVariables.current = 7;
            GlobalVariables.victory = 10;
        }
        if (battleScript.NoQuestions())
        {
            StartCoroutine(Winner(cekanje));
        }
        else
        {
            introText.enabled = true;
            yield return new WaitForSecondsRealtime(cekanje);
            introText.enabled = false;
            //Debug.Log("zavrsio cekanje");
       

            // READY TIPKA enabling
            readyButton.enabled = true;
            readyButton.image.enabled = true;
            readyButton.interactable = true;
            readyButtonText.enabled = true;
            readyButton.Select();
        }
        
        //Debug.Log("checkpoint");

        // sada se ceka da se stisne tipka READY , koja ce pokrenuti funkciju za borbu
        // onda pocinje "borba" -- pocnu padat pitanja itd
        // uključi Canvas ( pobrini se da je sve na svom mjestu )
    }

    

    public void StartBattle()
    {
        //Debug.Log("lul");

        // makni ready button
        readyButton.image.enabled = false;
        readyButtonText.enabled = false;

        // enable donji panel
        donjiPanel.SetActive(true);

        Debug.Log("input field enabled : " + inputField.enabled);
        Debug.Log("result field enabled : " + resultField.enabled);

        // osposobi padajuce panele
        //panel1.SetActive(true);
        //panel2.SetActive(true);
        //panel3.SetActive(true);


        // popunit strukture podakata koje su potrebne za "borbu" pa onda pokrenut 
        
        
        battleScript.Play();
        ingame = true;
        

        // zapocne play , i nastavlja izvoditi ovo u nastavku
        // tako da mozda if ( pobijedio ) segment staviti u neku drugu skriptu koja u Update prati jeliPobijedio boolean varijablu

        //Debug.Log("XDXDXD");

        // ako pobijedio
        //if (pobijedio)
        //{
        //    // tekst ( Congratulations blablabla )
        //    StartCoroutine(QuestCompleted(sekundeCekanje));

        //    // cekaj da coroutine zavrsi , onda nastavi

        //    //GlobalVariables.lastQuestCompleted++;
        //    // ako ovo bio zadnji quest 
        //    //if (GlobalVariables.lastQuestCompleted == listaPitanjaUTestIgri.size())
        //    //{
        //    //    //ENDGAME procedura / izađi iz igre u menu ( mozda stavi button Continue )
        //    //}            
        //}




        // inače ( ako izgubio )
        // tekst ( Unfortunately blablabla , Try Again )
    }

    private IEnumerator Winner(float cekanje)
    {
        winningText.text = "Čestitamo, prošli ste igru. Ne posoji više pitanja za ovaj predmet.";
        winningText.gameObject.SetActive(true);
        winningText.enabled = true;
        yield return new WaitForSecondsRealtime(cekanje);
        winningText.gameObject.SetActive(false);
        playerMovementScript.enabled = true;
        SceneManager.LoadScene(4);
    }

    IEnumerator QuestCompleted(float cekanje)
    {
        winningText.gameObject.SetActive(true);
        winningText.enabled = true;
        yield return new WaitForSecondsRealtime(cekanje);
        winningText.gameObject.SetActive(false);
        playerMovementScript.enabled = true;
        if(thisQuest == GlobalVariables.lastQuestCompleted) this.GetComponent<CircleCollider2D>().enabled = false;
        GlobalVariables.strenght +=1;
        if(GlobalVariables.strenght == 6)
        {
            GlobalVariables.strenght = 1;
            GlobalVariables.order += 1;
            GlobalVariables.lastQuestCompleted = 0;
            playerMovementScript.gameObject.transform.position = GlobalVariables.position[0];
        }
        ingame = false;

    }


}
