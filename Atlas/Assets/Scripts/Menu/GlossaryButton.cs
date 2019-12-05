using Localization;
using Plants.Plant;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class GlossaryButton : MonoBehaviour
    {
        public Text                Title;

        private PlantStatistics    _plantStat;

        private void Awake()
        {
            LocalizationManager.Instance.LocaleChanged += ReloadDataForNewLanguage;
        }

        private void OnDestroy()
        {
            LocalizationManager.Instance.LocaleChanged -= ReloadDataForNewLanguage;
        }

        public void SetPlantStat(PlantStatistics plantStatistics)
        {
            _plantStat = plantStatistics;
            UpdateButton();
        }

        private void ReloadDataForNewLanguage(object sender, LocaleChangedEventArgs e)
        {
            UpdateButton();
        }
        
        public void UpdateButton()
        {
            if (_plantStat == null)
                return;
            
            Title.text = _plantStat.Name;
        }
        
    }
}