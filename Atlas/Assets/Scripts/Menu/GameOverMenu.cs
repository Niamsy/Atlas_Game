using Game.Player.Stats;
using Localization;
using UnityEngine;
using SceneManagement;
using Player;
using Player.Scripts;
using UnityEngine.UI;
using Localization;
    
namespace Menu
{
    [RequireComponent(typeof(PlayerController))]
    public class GameOverMenu : MenuWidget
    {
        private PlayerController _playerController;
        private PlayerStats      _playerStats;
        private Canvas           _canvas;
        private Spawner          _spawner;
        private bool             _isAlreadyDead; 
        
        private LocalizedTextBehaviour             _textComposant;
        public LocalizedText     OxygenDeath;
        public LocalizedText     DrowningDeath;
        
        protected override void InitialiseWidget()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {
            _canvas = GetComponent<Canvas>();
            _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            _playerStats = _playerController.GetComponentInChildren<PlayerStats>();
            _spawner = _playerController.GetComponentInChildren<Spawner>();

            _textComposant = transform.GetChild(0).GetChild(0).GetComponent<LocalizedTextBehaviour>();

            Show(false);
        }
    
        // Update is called once per frame
        void Update()
        {
            if (_playerController.IsDead && !_isAlreadyDead)
            {
                _isAlreadyDead = true;
                _textComposant.LocalizedAsset = OxygenDeath;
                Show(true);
            }
        }

        public void Quit()
        {
            SceneLoader.Instance.QuitTheGame();
        }

        public void ContinueGame()
        {
            _playerController.Respawn();
            _spawner.Spawn();
            _isAlreadyDead = false;
            Show(false);
        }
    }
}