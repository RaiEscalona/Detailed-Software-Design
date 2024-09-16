namespace Fire_Emblem.Characters;
using System;
using System.IO;
using System.Collections.Generic;

public class ReadFiles
{
    public static List<string> ReadTeams(string path)
    {
        string[] files = Directory.GetFiles(path, "*.txt");
        List<string> fileNames = new List<string>();
        foreach (string file in files)
        {
            fileNames.Add(Path.GetFileName(file));
        }

        return fileNames;
    }
}