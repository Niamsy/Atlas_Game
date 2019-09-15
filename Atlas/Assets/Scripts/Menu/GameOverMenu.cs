using Game.Player.Stats;
using UnityEngine;
using SceneManagement;
using Player;
using Player.Scripts;

namespace Menu
{
    [RequireComponent(typeof(PlayerController))]
    public class GameOverMenu : MenuWidget
    {
        private PlayerController _playerController;
        private PlayerStats      _playerStats;
        private Canvas           _canvas;
        private Spawner          _spawner;
        private bool _isAlreadyDead;
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
            Show(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (_playerController.IsDead && !_isAlreadyDead)
            {
                _isAlreadyDead = true;
                Show(true);
            }
        }

        public void Quit()
        {
            SceneLoader.Instance.QuitTheGame();
        }

        public void ContinueGame()
        {
            _playerStats.Resources[Game.ResourcesManagement.Resource.Oxygen].Quantity = _playerStats.Resources[Game.ResourcesManagement.Resource.Oxygen].Limit;
            _playerController.CheckForDeath();
            _spawner.Spawn();
            _isAlreadyDead = false;
            Show(false);
        }
    }
}