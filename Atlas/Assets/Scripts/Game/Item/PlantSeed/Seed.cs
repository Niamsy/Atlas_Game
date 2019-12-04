using System;
using Game.Grid;
using Game.Inventory;
using Game.Questing;
using Game.Map;
using Plants.Plant;
using UnityEngine;

namespace Game.Item.PlantSeed
{
    [Serializable,
     CreateAssetMenu(fileName = "Seed", menuName = "Item/Seed", order = 1)]
    public class Seed : ItemAbstract
    {
        [SerializeField] private ConditionEvent _sowQuestEvent = null;
        [SerializeField] private Condition _sowCondition = null;
        private Vector3 _location = new Vector3(0,0,0);
        private Node _node = null;
        private Transform _sowDummy = null;
        private Transform SowDummy
        {
            get
            {
                if (!_sowDummy)
                    _sowDummy = GameObject.Find("SowDummy")?.transform;
                return _sowDummy;
            }
        }

        public override void Use(ItemStack selfStack)
        {
            //if (status != InputKeyStatus.Pressed)
            //    return;
            selfStack.ModifyQuantity(selfStack.Quantity - 1);
            if (_node != null)
            {
                Debug.Log("node " + _node.WorldPosition);
                _node.SowPlant(PlantStatistics.Prefab);
                _sowQuestEvent.Raise(_sowCondition, this, 1);
            }
        }

        public override bool CanUse(Transform transform)
        {
            LayerMask layerMask = LayerMask.GetMask("Ground");
            Camera camera = Camera.main;
            // Ray ray = new Ray(transform.position, camera.transform.forward * 2000.0f);
            //  Debug.Log("Use position: " + SowDummy.position);
            if (LevelManager.WorldGrid == null || SowDummy == null) return false;
            
            _node = LevelManager.WorldGrid.NodeFromWorldPoint(SowDummy.position);
            return _node.CanPlantGrow;
        }

        [Header("Seed variables")]
        [SerializeField] private PlantStatistics _plantStatistics;
        public PlantStatistics PlantStatistics
        {
            get { return (_plantStatistics); }
            set { _plantStatistics = value; }
        }
    }
}