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
        Debug.Log("Give Water begins");

        foreach (Node cell in WorldGrid.Grid)
        {
            var plant = cell.Plant;
            if (plant == null)
                continue;

            var consumer = plant.GetComponent<PlantConsumer>();
            if (consumer != null)
            {
                Debug.Log("Consumer not null, give to " + plant.name);
                consumer.ReceiveResource(Game.ResourcesManagement.Resource.Water, (int)(rainScript.RainIntensity * 10));
            }
        }
        Debug.Log("Give Water done");
    }
}
