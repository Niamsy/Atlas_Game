using Game.SavingSystem.Datas;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Main
{
    [RequireComponent(typeof(Button))]
    public class ProfilDisplay : MonoBehaviour
    {
        private ProfilData _data;
        
        [SerializeField] private Text _index = null;
        [SerializeField] private Text _profilName = null;
        private Button _button;

        public delegate void ProfilSelected(ProfilData data);

        public ProfilSelected OnClick;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            if (OnClick != null)
                OnClick(_data);
        }
        
        public void SetProfilData(ProfilData data)
        {
            _data = data;
            _index.text = data.ID.ToString();
            if (data.Used)
                _profilName.text = data.Name;
            else
                _profilName.text = "Empty";
        }
    }
}
