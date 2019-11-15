using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SavedIdHandler : MonoBehaviour
{
    [SerializeField] SavedConnectionDatas _data;
    [SerializeField] InputField _field;
    [SerializeField] InputField _fieldMdp;

    public bool _save;

    private void Start()
    {
        if (_data.getId().Length > 0)
        {
            _field.text = _data.getId();
        }
        if (_data.getPasswd().Length > 0)
        {
            _fieldMdp.text = _data.getPasswd();
        }
    }

    public void setSave(bool value)
    {
        print("Set Save:" + value);
        _save = value;
    }

    public void setPasswd(string pass)
    {
        print("Set Passwd:" + pass);
        _data.setPassword(pass);
    }

    public void setId(string id)
    {
        print("Set Id");
        _data.setId(id);
    }

    public string getSavePass()
    {
        return _data.getPasswd();
    }

    public string getSavedId()
    {
        return _data.getId();
    }
}
