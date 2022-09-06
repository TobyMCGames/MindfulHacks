using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private AccDatabaseManager databaseManager;

    [SerializeField] private InputField InputEmail;
    [SerializeField] private InputField InputPassword;
    [SerializeField] private InputField InputConfirmPassword;
    [SerializeField] private Text LoginStatus;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo _data = PlayerManager.Instance.data;
        if (_data.loggedIn)
        {
            InputEmail.text = _data.email;
            InputPassword.text = _data.password;
            Login();
        }
    }

    public void ResetParameters()
    {
        InputEmail.text = "";
        InputPassword.text = "";
        InputConfirmPassword.text = "";
        LoginStatus.text = "";
    }

    public void Login()
    {
        if (databaseManager == null)
        {
            LoginStatus.text = "Can't connect to database";
            return;
        }

        if (InputEmail.text.IndexOf('@') <= 0)
        {
            LoginStatus.text = "Invalid email";
            return;
        }

        string password = databaseManager.GetPassword(InputEmail.text);
        if (password == null)
        {
            LoginStatus.text = "Account does not exist";
            return;
        }
        
        if (password != InputPassword.text)
        {
            LoginStatus.text = "Password is incorrect";
            return;
        }

        PlayerInfo data = PlayerManager.Instance.data;
        data.email = InputEmail.text;
        data.password = InputPassword.text;
        data.loggedIn = true;
        PlayerManager.Instance.SaveProgress();
        SceneManager.LoadScene("HomeScreen");
    }

    public void Register()
    {
        if (databaseManager == null)
        {
            LoginStatus.text = "Can't connect to database";
            return;
        }

        if (InputEmail.text.IndexOf('@') <= 0)
        {
            LoginStatus.text = "Invalid email";
            return;
        }

        if (InputPassword.text != InputConfirmPassword.text)
        {
            LoginStatus.text = "Both passwords are not the same";
            return;
        }

        if (databaseManager.GetPassword(InputEmail.text) != null)
        {
            LoginStatus.text = "Account is already registered";
            return;
        }

        LoginStatus.text = "Registration successful";
        databaseManager.AddAccount(InputEmail.text, InputPassword.text);
    }
}
