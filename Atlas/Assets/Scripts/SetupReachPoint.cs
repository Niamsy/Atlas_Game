using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupReachPoint : MonoBehaviour
{
    [SerializeField] List<GameObject> _zones = new List<GameObject>();
    private bool canSetup = true;
    
    private void Start()
    {
        Invoke(nameof(init), 3.0f);
    }

    public void init()
    {
        if (!canSetup) return;
        foreach (var go in _zones)
        {
            go.SetActive(true);
            if (transform.parent != null)
            {
                go.transform.parent = transform.parent.transform;
            }
        }
    }

    public void CancelSetup()
    {
        canSetup = false;
    }
}
