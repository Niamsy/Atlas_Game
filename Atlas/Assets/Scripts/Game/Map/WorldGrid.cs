using Plants.Plant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Grid
{
    [ExecuteInEditMode]
    public class WorldGrid : MonoBehaviour
    {
        public LayerMask plantGrowthLayers;
        public LayerMask plantLayer;
        public float findPlantDistance = 100.0f;
        public Vector2 gridWorldSize;
        public float nodeRadius;

        [Header("Gizmo Debug")]
        public bool drawGizmo;
        public int drawEveryX = 1;
        public bool drawnOnlyNonGrowable;

        [Header("Plant Spawning Test")]
        public bool spawnPlant = false;
        public int numberOfPlantsInParentGameobject = 100;
        public GameObject[] plantsToSpawn;

        public Node[,] Grid { get; protected set; }

        public Node this[int x, int y, bool worldPos = false]
        {
            //todo convert to worldPOs;
            get { return worldPos ? Grid[x, y] : Grid[x, y]; }
        }

        private float m_NodeDiameter;
        private int gridSizeX, gridSizeY;
        private LayerMask inversePlantGrowthLayers;
        private int numberOfGrowableNode;

        private void Start()
        {
            InitGrid();
        }

        private void OnEnable()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
               //InitGrid();
                SpawnPlant();
            }
#endif
        }


        private void InitGrid()
        {
            m_NodeDiameter = nodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / m_NodeDiameter);
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / m_NodeDiameter);
            CreateGrid();
            inversePlantGrowthLayers = ~plantGrowthLayers.value;
            Debug.Log("Grid init");
        }

        private void CreateGrid()
        {
            Grid = new Node[gridSizeX, gridSizeY];
            Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
            for (int x = 0; x < gridSizeX; ++x)
            {
                for (int y = 0; y < gridSizeY; ++y)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * m_NodeDiameter + nodeRadius) + Vector3.forward * (y * m_NodeDiameter + nodeRadius);
                    Grid[x, y] = new Node(worldPoint, new Vector2(x, y), false, FindPlant(worldPoint));
                    bool growable = CanPlantGrow(Grid[x, y]);
                    if (growable)
                        numberOfGrowableNode += 1;
                    Grid[x, y].CanPlantGrow = growable;
                }
            }
        }

        private bool CanPlantGrow(Node node)
        {
            float minPlantSizeY = 0.5f;

            if (Physics.Raycast(new Vector3(node.WorldPosition.x, 524, node.WorldPosition.z), Vector3.down, out RaycastHit hit, 1000, plantGrowthLayers, QueryTriggerInteraction.Collide))
            {
                node.GroundLevel = hit.point.y;
                node.Normal = hit.normal;
                //Debug.Log(hit.point.y);
                if (Physics.SphereCast(node.WorldPosition, nodeRadius, Vector3.up, out RaycastHit hitY, findPlantDistance, inversePlantGrowthLayers, QueryTriggerInteraction.Collide))
                {
                    //Debug.Log(hit.point.y + " " + hitY.point.y);
                    if (hitY.point.y < minPlantSizeY + hit.point.y)
                        return false;
                    node.MaxPlantSizeY = hitY.point.y;
                }
            }
            return true;
        }

        private PlantModel FindPlant(Vector3 worldPoint)
        {
            PlantModel plant = null;
            if (Physics.SphereCast(worldPoint, (nodeRadius - nodeRadius / 5), Vector3.up, out RaycastHit hit, findPlantDistance, plantLayer, QueryTriggerInteraction.Collide))
            {
                //Debug.Log("Plant " + hit.transform.name);
                plant = hit.transform.parent.GetComponent<PlantModel>();
            }
            return plant;
        }

        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
            float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
            return Grid[x, y];
        }

        public float GetGridPlantGrowthCompletionPercent()
        {

            int plantMaxStageLevel = 3;
            /*Get the comparaison value for the percentage calcul. We use the Max plant level or average plant level 
            *so that the more level your plant has the higher the percentage of grid completion
            */
            int gridPercentMaxValue = numberOfGrowableNode * plantMaxStageLevel;
            float completion = 0.0f;

            foreach (Node cell in Grid)
            {
                if (cell.Plant)
                {
                    completion += cell.Plant.CurrentStageInt; // Add +1 or +0.5f if stage == 0 ?
                }
            }

            return (completion * 100) / gridPercentMaxValue;
        }

#if UNITY_EDITOR
        // to test
        public void SpawnPlant()
        {
            if (spawnPlant && plantsToSpawn.Length > 0)
            {
                int numberOfPlant = 0;
                GameObject parent = null;
                foreach (Node n in Grid)
                {
                    int r = Random.Range(0, plantsToSpawn.Length);
                    GameObject plant = plantsToSpawn[r];
                    if (n.CanPlantGrow && n.Plant == null)
                    {
                        if (numberOfPlant % numberOfPlantsInParentGameobject == 0)
                        {
                            parent = new GameObject();
                            parent.name = "PlantsHolder_" + numberOfPlant;
                        }

                        GameObject go = Instantiate(plant, parent.transform);
                        go.name = "Plant_" + numberOfPlant;
                        go.transform.position = new Vector3(n.WorldPosition.x, n.GroundLevel, n.WorldPosition.z);
                        go.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
                        go.transform.rotation = Quaternion.FromToRotation(transform.up, n.Normal) * go.transform.rotation;
                        n.Plant = go.GetComponent<PlantModel>();
                        ++numberOfPlant;
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (drawGizmo)
            {
                int i = 0;
                Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 10, gridWorldSize.y));
                if (Grid != null)
                {
                    foreach (Node n in Grid)
                    {
                        if (((i / gridSizeX) % drawEveryX == 0)
                            || ((i % gridSizeX) % drawEveryX == 0)
                            || !n.CanPlantGrow)
                        {
                            Gizmos.color = n.CanPlantGrow ? Color.white : Color.red;
                            if (n.CanPlantGrow && n.Plant)
                                Gizmos.color = Color.green;
                            if ((drawnOnlyNonGrowable && !n.CanPlantGrow) || !drawnOnlyNonGrowable)
                                Gizmos.DrawCube(n.WorldPosition, Vector3.one * (m_NodeDiameter - m_NodeDiameter / 10));
                        }
                        ++i;
                    }
                }
            }
        }
#endif
    }
}
