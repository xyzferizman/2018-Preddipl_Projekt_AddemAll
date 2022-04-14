using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListController : MonoBehaviour {

    public GameObject ContentPanel;
    public GameObject ListItemPrefab;
    public Button mojaPitanja;
    public Button svaPitanja;


    public ArrayList Pitanja;

    void Start()
    {
         
    }

    public void fillList()
    {

        // 2. Iterate through the data, 
        //	  instantiate prefab, 
        //	  set the data, 
        //	  add it to panel

        GameObject[] destroyStuff = GameObject.FindGameObjectsWithTag("ListItem");
        Debug.Log("broj itema: " + destroyStuff.Length);
        foreach (GameObject o in destroyStuff)
        {
            Destroy(o);
        }


        foreach (string p in Pitanja)
        {
            GameObject newPitanje = Instantiate(ListItemPrefab) as GameObject;
            ListItemController controller = newPitanje.GetComponent<ListItemController>();
            controller.pitanje.text = p;
            newPitanje.transform.parent = ContentPanel.transform;
            newPitanje.transform.localScale = Vector3.one;


        }
    }

    public void myQuestion()
    {
        mojaPitanja.interactable = false;
        svaPitanja.interactable = true;
        generateList(GlobalVariables.user.Getid());
    }

    public void allQuestion()
    {
        mojaPitanja.interactable = true;
        svaPitanja.interactable = false;
        generateList(0);
    }

    public void generateList(int id)
    {
        Pitanja = new ArrayList();

        Pitanja.Add("Problem | rjesenje | broj tocno odgovenih | broj svih odgovorenih ");

        WWWForm form = new WWWForm();
        form.AddField("admin_id", id);
        WWW www = new WWW(GlobalVariables.LoginURL + "statisticsQuest.php", form); // promjeni skriptu
        while (www.keepWaiting) ;
        string[] lines = www.text.Split('\n');
        foreach (string line in lines)
        {
            if (line != "")
                Pitanja.Add(line);
        }


        
        fillList();

    }


    
    public void Nazad()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
