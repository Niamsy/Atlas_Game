using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList
{
    public QuestList()
    {
        __availableQuest = new List<Quest>();
        __setupQuest();
    }

    public List<Quest> getQuestList()
    {
        return __availableQuest;
    }

    private void __setupQuest()
    {
        __questOne();
    }


    #region Quest Declaration

    private void __questOne()
    {
        string name = "one";
        string desc = "a description";
        Objective obj = new Objective(ObjType.PICKUP, "test", 1);
        List<Objective> objs = new List<Objective>();
        objs.Add(obj);
        Quest one = new Quest(name, desc, objs, (GameObject player) => {
            Debug.LogError("JE SUIS UNE RECOMPENSE");
            return null;
        });
        __availableQuest.Add(one);
    }


    #endregion

    List<Quest> __availableQuest;
}
