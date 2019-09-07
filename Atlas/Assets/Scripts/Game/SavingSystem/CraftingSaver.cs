using System;
using Game.Crafting;
using Game.SavingSystem.Datas;

namespace Game.SavingSystem
{
    public class CraftingSaver : MapSavingBehaviour
    {
        private Crafter _crafter;

        protected override void Awake()
        {
            base.Awake();
            _crafter = GetComponent<Crafter>();
        }

        protected override void SavingMapData(MapData data)
        {
            data.Crafting = new MapData.CraftingSaveData(_crafter);
        }

        protected override void LoadingMapData(MapData data)
        {
            _crafter.LoadFromSavedData(data.Crafting);
        }
    }
}