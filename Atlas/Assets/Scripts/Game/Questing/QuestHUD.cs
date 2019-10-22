using TMPro;
using Tools;
using UnityEditor;
using UnityEngine;

namespace Game.Questing
{
    public class QuestHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name = null;
        [SerializeField] private Transform _requirementsTransform = null;
        
        private ObjectPool _pool = null;
        private LiveQuest _data;

        public LiveQuest LiveQuest => _data;
        
        public void SetLiveQuest(LiveQuest data, ObjectPool pool)
        {
            _data = data;
            _name.text = _data.Quest.Name;
            _pool = pool;
            Refresh();
        }

        public void ClearRequirements()
        {
            Debug.Log("Clear Requirements CALLED");
            while (_requirementsTransform.childCount > 0)
            {
                var toRemove = _requirementsTransform.GetChild(0).gameObject;
                _pool.ReturnObject(toRemove);
            }
        }

        public void AddRequirements()
        {
            foreach (var requirement in _data.Requirements)
            {
                var requirementObj = _pool.GetObject();
                requirementObj.transform.SetParent(_requirementsTransform);
                requirementObj.transform.localScale = new Vector3(1, 1, 1);
                var requirementHud = requirementObj.GetComponent<RequirementHUD>();
                // TODO Get from save
                requirementHud.SetRequirement(requirement);
            }
        }

        public void Refresh()
        {
            ClearRequirements();
            AddRequirements();
        }
    }
}