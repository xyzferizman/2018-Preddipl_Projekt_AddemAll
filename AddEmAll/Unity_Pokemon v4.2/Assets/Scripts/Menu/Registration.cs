using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Registration : MonoBehaviour {
    public InputField Name;
    public InputField surname;
    public InputField email;
    public Dropdown year;
    public InputField password;
    public InputField password_2;
    public Text message;

    private string result = "";
    private EventSystem system;

        private void Start()
    {
        system = EventSystem.current;
        message.text = "";
        year.AddOptions(Year.getYearsInString());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            Register();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }

        if (result != "Registracija...." && result != "")
        {
            try
            {
                int id = Int32.Parse(result);
                GlobalVariables.user = new Player(id, Name.text, surname.text, email.text, 0, Int32.Parse(year.captionText.text.Split('-')[0]));
                SceneManager.LoadScene(4);
            }
            catch
            {
                message.text = "email adresa je već korištena";
            }
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
    public void Register()
    {
        if(Name.text != "" && surname.text != "" &&
            email.text != "" && year.captionText.text != "" && password.text != "" 
            && password_2.text != "")
        {
            if (email.text.Contains("@")) {
                if(password.text == password_2.text)
                {
                    message.text = result;
                    StartCoroutine(ConnectWithDataBase());
                                        
                }
                else
                {
                    password.Select();
                    message.text = "Lozinke se ne poklapaju";
                }
            }
            else
            {
                email.Select();
                message.text = "Upisi ispravan email!";
            }
        }
        else
        {
            message.text = "Unesite sva polja";
        }
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator ConnectWithDataBase()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", Name.text);
        form.AddField("surname", surname.text);
        form.AddField("email", email.text);
        form.AddField("year", Int32.Parse(year.captionText.text.Split('-')[0]));
        form.AddField("password", password.text);


        WWW www = new WWW(GlobalVariables.LoginURL + "insert.php", form);
        yield return www;
        Debug.Log("registracija: " + www.text);
        if (www.text.Contains("Dodano"))
        {
            string[] split = www.text.Split('o');
            result = split[2];
        }
        else
        {
            result = "e-mail adresa je već korištena";
        }


    }


}
