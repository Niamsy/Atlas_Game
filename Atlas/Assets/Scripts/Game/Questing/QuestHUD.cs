using TMPro;
using Tools;
using UnityEditor;
using UnityEngine;

namespace Game.Questing
{
    public class QuestHUD : MonoBehaviour
    {
        public struct Save
        {
            public Save(GUID questId, int currentCount)
            {
                QuestId = questId;
                CurrentCount = currentCount;
            }

            public GUID QuestId { get; }
            public int CurrentCount { get; }
        }
        
        [SerializeField] private TextMeshProUGUI _name = null;
        [SerializeField] private Transform _requirementsTransform = null;
        
        private ObjectPool _pool = null;
        private Quest _quest = null;
        private int _count = 0;
        
        public void SetData(Quest quest, ObjectPool pool)
        {
            _quest = quest;
            _pool = pool;
            _name.text = quest.Name;
            _count = 0;
        }

        public void SetData(Save save)
        {
            
        }

        public void ClearRequirements()
        {
            while (_requirementsTransform.childCount > 0)
            {
                var toRemove = _requirementsTransform.GetChild(0).gameObject;
                _pool.ReturnObject(toRemove);
            }
        }

        public void AddRequirements()
        {
            foreach (var requirement in _quest.Requirement)
            {
                var requirementObj = _pool.GetObject();
                requirementObj.transform.SetParent(_requirementsTransform);
                requirementObj.transform.localScale = new Vector3(1, 1, 1);
                var requirementHud = requirementObj.GetComponent<RequirementHUD>();
                // TODO Get from save
                requirementHud.SetRequirement(requirement, 0);
            }
        }

        public void Refresh()
        {
            ClearRequirements();
            AddRequirements();
        }
    }
}