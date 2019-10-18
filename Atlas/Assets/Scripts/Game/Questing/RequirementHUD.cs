using TMPro;
using UnityEngine;

namespace Game.Questing
{
    public class RequirementHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name = null;
        [SerializeField] private TextMeshProUGUI _count = null;
        [SerializeField] private Color validColor;
        
        public void SetRequirement(Requirement requirement, int currentCount)
        {
            _name.text = requirement.Description;
            _count.text = $"{currentCount}/{requirement.Count}";
            if (currentCount == requirement.Count)
            {
                _count.color = validColor;
                _name.color = validColor;
            }
        }
    }
}