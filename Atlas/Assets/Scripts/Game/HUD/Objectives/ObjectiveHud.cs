using Localization;
using TMPro;
using UnityEngine;

namespace Game.HUD.Objectives
{
    public class ObjectiveHud : MonoBehaviour
    {
        [SerializeField] private ObjectiveIcon objective1 = null;
        [SerializeField] private ObjectiveIcon objective2 = null;
        [SerializeField] private ObjectiveIcon objective3 = null;

        [SerializeField] private TextMeshProUGUI levelComplete = null; 
        
        void ValidateOneObjective()
        {
            if (!objective1.IsComplete)
            {
                objective1.SetComplete(true);
                return;
            }

            if (!objective2.IsComplete)
            {
                objective2.SetComplete(true);
                return;
            }

            if (!objective3.IsComplete)
            {
                objective3.SetComplete(true);
                levelComplete.enabled = true;
            }
        }
    }
}
