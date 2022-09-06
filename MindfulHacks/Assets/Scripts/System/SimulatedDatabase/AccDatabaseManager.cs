using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AccDatabaseManager : MonoBehaviour
{
    private string filePath;
    [SerializeField]private MyDictionary<string, string> accounts;

    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.dataPath + "/Scripts/System/SimulatedDatabase/Accounts.csv";
        GetAccounts();
    }

    private void GetAccounts()
    {
        if (!File.Exists(filePath))
        {
            accounts = new MyDictionary<string, string>();
            WriteAccounts();
            return;
        }

        List<string> database = CSVHandler.ReadCSV(filePath);
        for (int i = 0; i < database.Count; i++)
        {
            string data = database[i];
            string email = data.Substring(0, data.IndexOf(","));
            string password = data.Substring(data.IndexOf(",") + 1, data.Length - data.IndexOf(",") - 1);
            accounts.Add(email, password);
        }
    }

    private void WriteAccounts()
    {
        StreamWriter writer = new StreamWriter(filePath);

        for (int  i = 0; i < accounts.Count; i++)
        {
            MyKeyPair<string, string> account = accounts.ElementAt(i);
            writer.WriteLine($"{account.a},{account.b}");
        }

        writer.Flush();
        writer.Close();
    }

    public string GetPassword(string _email)
    {
        if (!accounts.ContainsKey(_email))
            return null;

        return accounts[_email];
    }

    public void AddAccount(string _email, string _password)
    {
        accounts.Add(_email, _password);
        WriteAccounts();
    }
}
