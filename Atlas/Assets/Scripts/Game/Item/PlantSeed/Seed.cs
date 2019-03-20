﻿using System;
using Game.Inventory;
using Plants.Plant;
using UnityEngine;

namespace Game.Item.PlantSeed
{
    [Serializable,
     CreateAssetMenu(fileName = "Seed", menuName = "Item/Seed", order = 1)]
    public class Seed : ItemAbstract
    {
        private Vector3 _location = new Vector3(0,0,0);

        public override void Use(ItemStack stack)
        {
            stack.ModifyQuantity(stack.Quantity - 1);
            GameObject plantModel = Instantiate(PrefabPlanted, _location, new Quaternion(0,0,0,1));
            PlantStatistics = plantModel.GetComponent<PlantModel>().PlantStatistics;
            plantModel.GetComponent<PlantModel>().Sow();       
        }

        public override bool CanUse(Transform transform)
        {
            LayerMask layerMask = LayerMask.GetMask("Ground");
            Camera camera = Camera.main; 
            Ray ray = new Ray(transform.position, camera.transform.forward * 2000.0f);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 2000.0f, layerMask))
            {
                if (raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Ground") && raycastHit.distance < 2.0f)
                {
                    _location = raycastHit.point;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        [Header("Seed variables")]
        [SerializeField] private GameObject _prefabPlanted;
        public GameObject PrefabPlanted
        {
            get { return (_prefabPlanted); }
        }
        
        [SerializeField] private PlantStatistics _plantStatistics;
        public PlantStatistics PlantStatistics
        {
            get { return (_plantStatistics); }
            set { _plantStatistics = value; }
        }
    }
}
