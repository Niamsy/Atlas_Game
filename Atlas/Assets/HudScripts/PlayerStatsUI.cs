using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    public Image currentHealthBar;
    public PlayerStats stats;
    
    void Start()
    {
        updateBar();    
    }

    private void Update()
    {
        updateBar();
    }
    private void updateBar()
    {
        float ratio = stats.playerHealth.getCurrent() / stats.playerHealth.getMax();
        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }
    
}
