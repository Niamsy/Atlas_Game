using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupReachPoint : MonoBehaviour
{
    [SerializeField] List<GameObject> _zones = new List<GameObject>();

    void Start()
    {
        foreach(GameObject go in _zones)
        {
            go.SetActive(true);
        }    
    }


}
