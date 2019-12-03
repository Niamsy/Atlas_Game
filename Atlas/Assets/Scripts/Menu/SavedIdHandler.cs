using UnityEngine;
using UnityEngine.UI;

public class SavedIdHandler : MonoBehaviour
{
    [SerializeField] private SavedConnectionDatas _data = null;
    [SerializeField] private InputField _field = null;
    [SerializeField] private InputField _fieldMdp = null;

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
        _save = value;
    }

    public void setPasswd(string pass)
    {
        _data.setPassword(pass);
    }

    public void setId(string id)
    {
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
