using System;
using System.Collections.Generic;
using UnityEngine;

struct Position
{
    public int x;
    public int y;
}
internal class GlobalVariables
{
    public static User user = new Administrator(46, "Adam", "Adamić", "adam@fer.hr");
    public static DateTime start = DateTime.Now;

    public static string LoginURL = "https://addemall.000webhostapp.com/";
    
    
    internal static int order = 1;
    internal static int strenght =1;
    internal static int year = 1;
    internal static int subject = 1;

    
    internal static int victory = 10;
    internal static int current = 7;

    // Danijel dodao slijedece varijable
    public static int lastQuestCompleted = 0;           // koji je zadnji quest completan , poveca se na 1 nakon completanja 1.questa
    public static int sceneIteration = 0;               // varijabla koja prati koja je trenutno po redu iteracija scene , 0 za 1.put


    // Danijel pretvorio u Vector3 jer je player.transform.position tipa Vector3 sa Z vrijednosti jednakom 0
    public static Vector3[] position = new Vector3[5] { new Vector3(1706, 4,0), new Vector3(1706, 4,0), new Vector3(2036,-14,0), new Vector3(2357, -56,0), new Vector3(2286,-450,0) };


    

}

