using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player
{
    static public int GameNum { get; set; }

    static private string path = Application.dataPath+ "win32.dl";


    static public void LoadPlayer()
    {
        if (!File.Exists(path)) File.WriteAllText(path, "1\n1");

        string[] playerInfo = File.ReadAllLines(path);

        GameNum = int.Parse(playerInfo[0]);

    }

    static public void SavePlayer()
    {
        string contetnts = $"{GameNum}\n";
        File.WriteAllText(path, contetnts);
    }

}
