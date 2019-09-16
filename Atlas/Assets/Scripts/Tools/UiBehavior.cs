using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetActiveInactive(GameObject gameObject) => gameObject.SetActive((gameObject.activeSelf == true) ? false : true);
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
