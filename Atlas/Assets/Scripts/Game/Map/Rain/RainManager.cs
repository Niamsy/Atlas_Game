using DigitalRuby.RainMaker;
using Game.Grid;
using Game.ResourcesManagement.Consumer;
using UnityEngine;

public class RainManager : MonoBehaviour
{
    public float Frequency = 0f;
    public float Duration = 0;
    public WorldGrid WorldGrid;

    private float timeleft = 0;
    private float intensity = 0f;
    private RainScript rainScript;

    // Start is called before the first frame update
    void Start()
    {
        rainScript = gameObject.GetComponentInParent<RainScript>();
        intensity = rainScript.RainIntensity;
        rainScript.RainIntensity = 0;
        timeleft = Duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeleft <= 0)
        {
            float rand = Random.value;
            if (rand <= Frequency)
            {
                rainScript.RainIntensity = intensity;
            }
            else
            {
                rainScript.RainIntensity = 0;
            }
            timeleft = Duration;
        }
        else if (timeleft > 0)
        {
            if (WorldGrid.Grid != null)
            {
                GiveWater();
            }
            else
            {
                Debug.Log("Null grid");
            }

            timeleft -= Time.deltaTime;
        }
    }

    void GiveWater()
    {
        Debug.Log("x: " + WorldGrid.gridWorldSize.x + " y: " + WorldGrid.gridWorldSize.y);
        for (var x = 0; x < WorldGrid.gridWorldSize.x; x++)
        {
            for (var y = 0; y < WorldGrid.gridWorldSize.y; y++)
            {
                if (WorldGrid.Grid[x, y] == null)
                    continue;

                var plant = WorldGrid.Grid[x, y].Plant;
                if (plant == null)
                    continue;

                var consumer = WorldGrid.Grid[x, y].Plant.GetComponent<PlantConsumer>();
                if (consumer != null)
                {
                    Debug.Log("Consumer not null, give to " + WorldGrid.Grid[x, y].Plant.name);
                    consumer.ReceiveResource(Game.ResourcesManagement.Resource.Water, (int)(rainScript.RainIntensity * 10));
                }
            }
        }
        
    }
}
