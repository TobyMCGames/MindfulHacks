using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProfileManager : MonoBehaviour
{
    public Text Name;
    public Text Email;

    // Start is called before the first frame update
    void Start()
    {
        Name.text = "Name: " + PlayerManager.Instance.data.email;
        Email.text = "Email: " + PlayerManager.Instance.data.email;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
