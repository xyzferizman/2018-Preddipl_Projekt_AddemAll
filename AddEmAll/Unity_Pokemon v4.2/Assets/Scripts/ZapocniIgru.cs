using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZapocniIgru : MonoBehaviour {

    public Button tipkaZaPocetakIgre;
    public Text tekstTipke;
    public PlayerMovement movementSkripta ;     // vjerojatno ce trebati koristiti getComponent pri reloadanju scene
    public Text dobrodosli;        // vjerojatno ce trebati koristiti getComponent pri reloadanju scene
    public float cekanjeSekunde;
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject[] panelsForDisable;

    void Start()
    {
        tipkaZaPocetakIgre.Select();
        int strenght = GlobalVariables.strenght;
        player.transform.position = GlobalVariables.position[strenght - 1];

        //movementSkripta.enabled = false;
        //dobrodosli.enabled = false;
    }
    public void Pause()
    {
        foreach(GameObject go in panelsForDisable)
        {
            go.SetActive(false);
        }
        movementSkripta.enabled = false;
        pauseMenu.SetActive(true);
    }

    public void Return()
    {
        foreach (GameObject go in panelsForDisable)
        {
            go.SetActive(true);
        }
        movementSkripta.enabled = true;
        pauseMenu.SetActive(false);
    }

    

    public void Exit()
    {
        SceneManager.LoadScene(4);
    }
    public void pocniIgru()
    {
        tipkaZaPocetakIgre.enabled = false;         // disablea samo button , ne disablea image
        tipkaZaPocetakIgre.image.enabled = false;           
        tekstTipke.enabled = false;
        
        // prikazi text i pricekaj par sekundi
        StartCoroutine(PricekajSekundiISredi(cekanjeSekunde));
        
    }
    
    // za ostvarivanje real time cekanja
    IEnumerator PricekajSekundiISredi(float cekanje)
    {
        dobrodosli.enabled = true;  
        movementSkripta.enabled = false;
        yield return new WaitForSecondsRealtime(cekanje);
        movementSkripta.enabled = true;
        dobrodosli.enabled = false;  
    }

}
