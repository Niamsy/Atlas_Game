using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tools;

public class QuestRenderer : MonoBehaviour
{
    public Text title;
    public Text questNumber;
    public GameObject Panel;
    public int QuestIndex = -1;
    public int MaxQuestIndex = -1;
    public Text QuestTitle;
    public Text QuestDesc;
    public Text QuestObjs;

    private int i = 0;

    public void OnOffQuestMenu()
    {
        if (Panel != null)
        {
            Animator animator = Panel.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("Display", !animator.GetBool("Display"));
                updateQuestPanel();
            }

        }
    }
    
    public void AddOneIndex()
    {
        if (QuestIndex == -1)
        {
            return;
        }

        if (QuestIndex + 1 < MaxQuestIndex)
        {
            QuestIndex++;
        }
        updateQuestPanel();
    }

    public void takeOneIndex()
    {
        if (QuestIndex == -1)
        {
            return;
        }

        if (QuestIndex == 0)
        {
            if (MaxQuestIndex != 0)
            {
                QuestIndex = MaxQuestIndex;
            }
            
        } else
        {
            QuestIndex--;
        }
        updateQuestPanel();
    }

    public void tryToApplyReward()
    {
        if (QuestIndex >= 0)
        {
            if (Singleton<QuestManager>.Instance.applyRewardIdx(null, QuestIndex))
            {
                updateQuestPanel();
            }
        }
    }

    public void updateQuestPanel()
    {
        title.text = "Quests";
        if (MaxQuestIndex != Singleton<QuestManager>.Instance.getActiveQuestQte())
        {
            QuestIndex = -1;
        }
        MaxQuestIndex = Singleton<QuestManager>.Instance.getActiveQuestQte();
        if (MaxQuestIndex > 0 && QuestIndex == -1)
        {
            QuestIndex = 0;
        }
        questNumber.text = (QuestIndex + 1).ToString() + " / " + MaxQuestIndex.ToString();
        if (QuestIndex >= 0)
        {
            Quest currentQuest = Singleton<QuestManager>.Instance.getQuestData(QuestIndex);
            QuestTitle.text = currentQuest.getQuestName();
            QuestDesc.text = currentQuest.getQuestDesc();
            QuestObjs.text = "";

            List<Objective> currentObjs = currentQuest.getQuestObjs();
            foreach (Objective obj in currentObjs) {
                string currentObjLine = "";
                switch (obj.getObjType())
                {
                    case ObjType.PICKUP:
                        currentObjLine = "Pickup " + obj.getObjDesc() + " | Status :";
                        if (obj.isComplete())
                            currentObjLine += " Done";
                        else
                            currentObjLine += " Not Done";
                        break;
                    case ObjType.PLANT:
                        break;
                    case ObjType.REACH:
                        break;
                }
                currentObjLine += "\n";
                QuestObjs.text += currentObjLine;
            }
        }
        else
        {
            QuestTitle.text = "Aucune Quete en cours";
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        title.text = "Quests";
        updateQuestPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Panel.GetComponent<Animator>().GetBool("Display") && Singleton<QuestManager>.Instance.callForUpdate)
        {
            updateQuestPanel();
            Singleton<QuestManager>.Instance.callForUpdate = false;
        }

        if (Input.GetKeyDown("j") && i > 15)
        {
            OnOffQuestMenu();
            i = 0;
        }
        if (i < 31)
        {
            ++i;
        }
        
    }
}
