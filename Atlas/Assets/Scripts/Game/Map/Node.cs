using Plants.Plant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Grid
{
    public class Node
    {
        private PlantModel m_Plant;
        private bool _canGrowPlant = true;

        public bool CanPlantGrow { get { return _canGrowPlant && Plant == null; } set { _canGrowPlant = value; } }
        public float MaxPlantSizeY { get; set; } = 0f;
        public PlantModel Plant
        {
            get { return m_Plant; }
            set
            {
              //  if (CanPlantGrow && MaxPlantSizeY != 0 /*&& value.MeshRender.bounds.size.y <= MaxPlantSizeY*/)
                    m_Plant = value;
            }
        }

        //Value on Y to spawn the plant
        public float GroundLevel { get; set; }
        //Normal of the ground at this node to orient the plant when spawning
        public Vector3 Normal { get; set; }

        public Vector3 WorldPosition { get; set; }
        public Vector2 GridPosition { get; set; }

        public Node(Vector3 worldPosition, Vector2 gridPosition, bool canPlantGrow = false, PlantModel plant = null)
        {
            CanPlantGrow = canPlantGrow;
            Plant = plant;
            WorldPosition = worldPosition;
            GridPosition = gridPosition;
        }

        public void SowPlant(GameObject prefab)
        {
            //TODO check last level plant size with MaxLevelPlantSize
            if (!CanPlantGrow)
                return;
            Plant = Object.Instantiate(prefab, new Vector3(WorldPosition.x, GroundLevel, WorldPosition.z), new Quaternion(0, 0, 0, 1)).GetComponent<PlantModel>();
            Plant.Sow();
            Plant.SetPlantName();
        }
    }
}
