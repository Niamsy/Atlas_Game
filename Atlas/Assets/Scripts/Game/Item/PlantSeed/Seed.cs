﻿using System;
using Game.Inventory;
using InputManagement;
using Plants.Plant;
using UnityEngine;

namespace Game.Item.PlantSeed
{
    [Serializable,
     CreateAssetMenu(fileName = "Seed", menuName = "Item/Seed", order = 1)]
    public class Seed : ItemAbstract
    {
        private Vector3 _location = new Vector3(0,0,0);

        public override void Use(ItemStack selfStack)
        {
            Debug.Log("Use Seed");
            //if (status != InputKeyStatus.Pressed)
            //    return;
            selfStack.ModifyQuantity(selfStack.Quantity - 1);
            PlantModel plantModel = Instantiate(PlantStatistics.Prefab, _location, new Quaternion(0,0,0,1)).GetComponent<PlantModel>();
            plantModel.Sow();
            plantModel.SetPlantName();
        }

        public override bool CanUse(Transform transform)
        {
            LayerMask layerMask = LayerMask.GetMask("Ground");
            Camera camera = Camera.main; 
            Ray ray = new Ray(transform.position, camera.transform.forward * 2000.0f);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 2.0f, layerMask))
            {
                _location = raycastHit.point;
                return true;
            }
            return false;
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
