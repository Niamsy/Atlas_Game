using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupReachPoint : MonoBehaviour
{
    [SerializeField] List<GameObject> _zones;

    private void Awake()
    {
        init();
    }

    public void init()
    {
        foreach (GameObject go in _zones)
        {
            go.SetActive(true);
            if (transform.parent != null)
            {
                go.transform.parent = transform.parent.transform;
            }
            print("Reach point parent set to : " + go.transform.parent);
        }
    }
}
