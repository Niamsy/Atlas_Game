using DigitalRuby.RainMaker;
using UnityEngine;

public class RainManager : MonoBehaviour
{
    public float Frequency = 0f;
    public float Duration = 0;

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
            if (rand < Frequency)
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
            timeleft -= Time.deltaTime;
        }
        
    }
}
