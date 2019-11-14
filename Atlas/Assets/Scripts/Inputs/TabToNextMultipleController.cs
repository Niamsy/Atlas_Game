using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabToNextMultipleController : MonoBehaviour
{
    public InputField[] fields;
    private int _index = 1;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && _index < 4)
        {
            fields[_index].ActivateInputField();
            _index++;
        }

        if (fields[0].isFocused)
        {
            _index = 1;
        }

        if (fields[1].isFocused)
        {
            _index = 2;
        }

        if (fields[2].isFocused)
        {
            _index = 3;
        }

        if (fields[3].isFocused)
        {
            _index = 0;
        }
    }
}
