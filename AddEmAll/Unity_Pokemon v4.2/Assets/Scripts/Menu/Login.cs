using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Login : MonoBehaviour { 

    public InputField email;
    public InputField password;
    public Text messege;
    
    private string result = "";

    private EventSystem system;
   
    private void Start()
    {
        system = EventSystem.current;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) )
        {
            Log();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }

        string[] split;
        if (result.Contains('|'))
        {
            messege.text = "Prijava....";
            split = result.Split('|');
            if (split[6] == "0")
            {
                GlobalVariables.user = new Player(Int32.Parse(split[0]), split[1], split[2], split[3], Int32.Parse(split[5]), Int32.Parse(split[4]));
            }
            else
            {
                GlobalVariables.user = new Administrator(Int32.Parse(split[0]), split[1], split[2], split[3]);
            }

            GlobalVariables.start = DateTime.Now;
            int scena = GlobalVariables.user.isAdministrator() ? 0 : 1;
            SceneManager.LoadScene(scena + 3);

        }
        else
        {
            messege.text = result;
        }

        if (system.currentSelectedGameObject == null || !Input.GetKeyDown(KeyCode.Tab))
            return;

        Selectable current = system.currentSelectedGameObject.GetComponent<Selectable>();
        if (current == null)
            return;

        bool up = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        Selectable next = up ? current.FindSelectableOnUp() : current.FindSelectableOnDown();

        // We are at the end or the beginning, go to either, depends on the direction we are tabbing in
        // The previous version would take the logical 0 selector, which would be the highest up in your editor hierarchy
        // But not certainly the first item on your GUI, or last for that matter
        // This code tabs in the correct visual order
        if (next == null)
        {
            next = current;

            Selectable pnext;
            if (up) while ((pnext = next.FindSelectableOnDown()) != null) next = pnext;
            else while ((pnext = next.FindSelectableOnUp()) != null) next = pnext;
        }

        // Simulate Inputfield MouseClick
        InputField inputfield = next.GetComponent<InputField>();
        if (inputfield != null) inputfield.OnPointerClick(new PointerEventData(system));

        // Select the next item in the taborder of our direction
        system.SetSelectedGameObject(next.gameObject);
    
    
    }

   

    public void Log()

    {
        StartCoroutine(ConnectWithDataBase());
        
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator ConnectWithDataBase()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email.text);
        form.AddField("password", password.text);


        WWW www = new WWW(GlobalVariables.LoginURL + "login.php", form);
        yield return www;

        result = www.text;
    }

}

