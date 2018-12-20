using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AtlasFileSystem fs = AtlasFileSystem.Instance;
        fs.saveConfig();
        Debug.Log(fs.getConfigValue("APIDevAddr"));
    }

   
   
}
