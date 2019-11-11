using UnityEngine;
using UnityEngine.UI;

public class UnscaledTime : MonoBehaviour
{
    [SerializeField] private string timeProperty = "Vector1_E78B2588";

    private Material material = null;

    private float RandomValue = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        var image = GetComponent<Image>();
        material = image.material;
        RandomValue = Random.Range(0f, 10000f);
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat(timeProperty, Time.unscaledTime + RandomValue);
    }
    
}
