using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProfileManager : MonoBehaviour
{
    public Text Name;
    public Text Email;
    public Text UserType;
    public Button UpdateProfileBtn;
    // Start is called before the first frame update
    void Start()
    {
        Name.text = "Name: " + PlayerManager.Instance.data.email;
        Email.text = "Email: " + PlayerManager.Instance.data.email;
        UserType.text = "User Type: " + PlayerManager.Instance.data.UserType;
        Button btn = UpdateProfileBtn.GetComponent<Button>();
        btn.onClick.AddListener(UpdateProfile);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void UpdateProfile()
    {
        Name.text = "Name: " + PlayerManager.Instance.data.email;
        Email.text = "Email: " + PlayerManager.Instance.data.email;
        UserType.text = "User Type: " + PlayerManager.Instance.data.UserType;
        Debug.Log("You have clicked the button!");
    }
}
