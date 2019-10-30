using TMPro;
using UnityEngine;

namespace Game.Questing
{
    public class RequirementHud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name = null;
        [SerializeField] private TextMeshProUGUI _count = null;
        [SerializeField] private Color validColor = Color.green;
        private LiveRequirement _requirement = null;
        public void SetRequirement(LiveRequirement requirement)
        {
            if (_requirement != null)
            {
                _requirement.IncrementDelegate = null;
            }
            
            _requirement = requirement;
            _requirement.IncrementDelegate = Refresh;
            _name.text = _requirement.Requirement.Description;
            _count.text = $"{_requirement.CurrentlyAccomplished}/{_requirement.Requirement.Count}";
            SetTextColor();
        }

        private void SetTextColor()
        {
            if (_requirement.CurrentlyAccomplished == _requirement.Requirement.Count)
            {
                _count.color = validColor;
                _name.color = validColor;
            }
            else
            {
                _count.color = Color.white;
                _name.color = Color.white;
            }
        }

        private void Refresh()
        {
            _name.text = _requirement.Requirement.Description;
            _count.text = $"{_requirement.CurrentlyAccomplished}/{_requirement.Requirement.Count}";
            SetTextColor();
        }
    }
}