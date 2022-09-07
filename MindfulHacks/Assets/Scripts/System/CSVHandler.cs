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

    public static List<string> ReadCSV(TextAsset textAsset)
    {
        List<string> lines = new List<string>();
        string AssetLines = textAsset.text;
        int startAt = 0;
        while (true)
        {
            int EndAt = AssetLines.IndexOf("\n", startAt);

            if (EndAt <= 0)
                break;

            lines.Add(AssetLines.Substring(startAt, EndAt - startAt - 1));

            startAt = EndAt + 1;
        }
        return lines;
    }
}
