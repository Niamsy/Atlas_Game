using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SavedIdHandler : MonoBehaviour
{
    [SerializeField] SavedConnectionDatas _data;
    [SerializeField] InputField _field;
    public bool _save;

    private void Start()
    {
        if (_data.getId().Length > 0)
        {
            _field.text = _data.getId();
        }
    }

    public void setSave(bool value)
    {
        _save = value;
    }

    public void setId(string id)
    {
        _data.setId(id);
    }

    public string getSavedId()
    {
        return _data.getId();
    }
}
