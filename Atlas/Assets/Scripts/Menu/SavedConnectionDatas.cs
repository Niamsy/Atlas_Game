using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SavedConnectionDatas", menuName = "Menu/SavedConnection", order = 2)]
public class SavedConnectionDatas : ScriptableObject
{
    [SerializeField] string IdConnection;


    public string getId()
    {
        return IdConnection;
    }

    public void setId(string newId)
    {
        IdConnection = newId;
    }
}
