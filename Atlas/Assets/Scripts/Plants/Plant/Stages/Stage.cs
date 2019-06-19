using System.Collections.Generic;
using Game.Item.PlantSeed;
using Game.ResourcesManagement;
using Plants.Plant;
using UnityEngine;

namespace Plants
{
    [CreateAssetMenu(menuName = "Plant System/Stage")]
    public class Stage : ScriptableObject
    {
        [System.Serializable]
        public struct Need
        {
            public Resource       type;
            public int             quantity;
        }
        [Header("Gameplay")]
        [SerializeField]
        private SoilType            _soils;
        [SerializeField]
        private List<Need>          _needs;
        [SerializeField]
        private float               _height = 0;
        [SerializeField]
        private float               _temperature = 0;
        [SerializeField]
        [Header("Miscellaneous")]
        private GameObject          _model = null;
        [SerializeField]
        private Material[]          _materials = null;
        [SerializeField]
        private AtlasAudio.Audio    _audio = null;
        [SerializeField]
        private GameObject          _growEffect = null;
        [SerializeField]
        private GameObject          _deathEffect = null;

        public SoilType Soils => _soils;

        public List<Need> Needs => _needs;

        public GameObject Model => _model;

        public AtlasAudio.Audio Audio => _audio;

        public float Height => _height;

        public float Temperature => _temperature;

        public Material[] Materials => _materials;

        public GameObject GrowEffect => _growEffect;

        public GameObject DeathEffect => _deathEffect;
    }
}
