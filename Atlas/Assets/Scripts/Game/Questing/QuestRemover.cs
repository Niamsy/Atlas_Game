using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Game.Questing
{
    public class QuestRemover : MapSavingBehaviour
    {
        [SerializeField] private Quest quest = null;
        private QuestingEventTrigger eventTrigger = null;
        private SetupReachPoint _setupReachPoint = null;
        protected override void Awake()
        {
            base.Awake();
            eventTrigger = GetComponent<QuestingEventTrigger>();
            _setupReachPoint = GetComponent<SetupReachPoint>();
        }

        protected override void LoadingMapData(MapData data)
        {
            if (data.Questing.QuestsDone == null || quest == null) return;
            if (!FindAndDestroy(data.Questing.QuestsDone))
            {
                FindAndDestroy(data.Questing.Quests);
            }
        }

        private bool FindAndDestroy(IEnumerable<MapData.QuestData> quests)
        {
            if (!quests.Any(questData => quest.Id == questData.Id || questData.Name == quest.Name)) return false;
            
            if (eventTrigger != null)
            {
                eventTrigger.triggerOnDestroy = false;
                eventTrigger.triggerOnStart = false;
                eventTrigger.quest = null;
            }

            if (_setupReachPoint != null)
            {
                _setupReachPoint.CancelSetup();
            }    
            Destroy(gameObject);
            return true;

        }
    }
}
