using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerInfo
{
    // Account Details
    public string email = "";
    public string password = "";
    public bool loggedIn = false;

    // Tutorial Info
    public bool FirstLogin = true;
}
