using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tip
{
    public int id;
    public string text;


    public string GetLoc(string loc = "FR")
    {
        string[] texts = text.Split('$');
        switch(loc)
        {
            case "FR": return texts[0];
            case "ENG": return texts[1];
            default: return texts[0];
        }
    }
}
