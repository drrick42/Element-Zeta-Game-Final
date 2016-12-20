using UnityEngine;
using System.Collections;

public static class SaveLoadManager
{

    private static string filePath = "/SaveData/";
    private static string fileName = "save.dat";

    static SaveLoadManager()
    {
        System.IO.Directory.CreateDirectory(System.Environment.CurrentDirectory + filePath);
    }

    public static void SaveGameData(int[] values)
    {

        string[] lines = new string[values.Length];
        for (int i = 0; i < values.Length; i++)
            lines[i] = values[i] + "";

        System.IO.File.WriteAllLines(System.Environment.CurrentDirectory + filePath + fileName, lines);

    }

    public static int[] LoadGameData()
    {
        try
        {
            string[] lines = System.IO.File.ReadAllLines(System.Environment.CurrentDirectory + filePath + fileName);

            int[] values = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                values[i] = int.Parse(lines[i]);
            }

            return values;
        }
        catch(System.IO.FileNotFoundException e)
        {
            return null;
        }
    }

}
