using UnityEngine;
using UnityEngine.EventSystems;

public class GlowDeactivator : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject glow = null;

    public void SetGlowActive()
    {
        if (glow != null)
        {
            glow.SetActive(true);
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (glow != null)
        {
            glow.SetActive(false);
        }    
    }
}
