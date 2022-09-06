using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVHandler
{
    public static List<string> ReadCSV(string filePath)
    {
        StreamReader reader = new StreamReader(filePath);
        List<string> lines = new List<string>();

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            lines.Add(line);
        }

        return lines;
    }
}
