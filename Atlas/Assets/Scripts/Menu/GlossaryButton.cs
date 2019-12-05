using Localization;
using Menu.Inventory.ItemDescription;
using Plants.Plant;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class GlossaryButton : MonoBehaviour
    {
        public Text                Title;
        private Button _button;
        
        private PlantStatsDescriptionHUD   _description;
        private PlantStatistics            _plantStat;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
            LocalizationManager.Instance.LocaleChanged += ReloadDataForNewLanguage;
        }

        private void OnDestroy()
        {
            LocalizationManager.Instance.LocaleChanged -= ReloadDataForNewLanguage;
        }

        public void SetPlantStat(PlantStatistics plantStatistics, PlantStatsDescriptionHUD description)
        {
            _plantStat = plantStatistics;
            
            if (_description == null)
                _description = description;
            
            UpdateButton();
        }

        private void OnClick()
        {
            _description.SetPlant(_plantStat);
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