using Game.SavingSystem;
using Game.SavingSystem.Datas;
using UnityEngine;

namespace Game.SavingSystem
{
    [RequireComponent(typeof(Leveling.PlayerExperience))]
    public class PlayerExperienceSaver : MapSavingBehaviour
    {
        private Leveling.PlayerExperience _PlayerExperience;

        protected override void Awake()
        {
            base.Awake();
            _PlayerExperience = GetComponent<Leveling.PlayerExperience>();
        }


        protected override void SavingMapData(MapData data)
        {
            data.XPData.PlayerXP = _PlayerExperience.CurrentXP;
            data.XPData.PlayerLevel = _PlayerExperience.Level;
            data.XPData.RoofLevel = _PlayerExperience.LevelRoof;
            data.XPData.FloorLevel = _PlayerExperience.LevelFloor;
        }

        protected override void LoadingMapData(MapData data)
        {
            _PlayerExperience.CurrentXP = (int)data.XPData.PlayerXP;
            _PlayerExperience.LevelFloor = (int)data.XPData.FloorLevel;
            _PlayerExperience.LevelRoof = (int)data.XPData.RoofLevel;
            _PlayerExperience.Level = (int)data.XPData.PlayerLevel;
        }
    }
}