using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem<T> where T : class
{
    public static T ReadFile(string _filePath)    {
        if (File.Exists(_filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(_filePath, FileMode.Open);

            T _data = formatter.Deserialize(stream) as T;

            stream.Close();

            return _data;
        }
        else
        {
            return null;
        }
    }

    public static void WriteFile(string _filePath, T _data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(_filePath, FileMode.Create);

        formatter.Serialize(stream, _data);

        stream.Close();
    }

    public static void DeleteFile(string _filePath)
    {
        Debug.Log($"Deleting Data at: {_filePath}");
        try
        {
            File.Delete(_filePath);
        }
        catch (IOException ex)
        {
            Debug.LogException(ex);
        }
    }
}
