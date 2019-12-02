using UnityEngine;
using UnityEngine.UI;

public class RefreshPopup : MonoBehaviour
{
    [SerializeField] private Text _text = null;
    [SerializeField] private float _timeDuration = 0;
    [SerializeField] private float _currentTimer = 0;
    [SerializeField] private Image _exclamationPoint = null;

    private bool _success = false;

    private void Start()
    {
        _currentTimer = 0;
    }

    public void SetDisplay(bool display)
    {
        enabled = display;
    }

    public void SetColor(Color exclamationColor)
    {
        _exclamationPoint.color = exclamationColor;
    }

    public void StartPopup(string text)
    {
        _currentTimer = 0;
        SetText(text);
    }

    public void SetText(string text)
    {
        _text.text = (_success) ? "Success :\n" : "Failure :\n";
        _text.text += text;
    }


    private void Update()
    {
        _currentTimer += 0.1f;
        if (_currentTimer > _timeDuration)
        {
            enabled = false;
        }
    }
}
