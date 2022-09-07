using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private string dataFilePath;
    public PlayerInfo data;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Init()
    {
        dataFilePath = Application.persistentDataPath + "/playerData.txt";
        data = SaveSystem<PlayerInfo>.ReadFile(dataFilePath);
        if (data == null)
        {
            data = new PlayerInfo();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SaveProgress();
    }

    public void SaveProgress()
    {
        SaveSystem<PlayerInfo>.WriteFile(dataFilePath, data);
    }

    public void ReadData()
    {
        data = SaveSystem<PlayerInfo>.ReadFile(dataFilePath);
    }
}
