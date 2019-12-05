using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Game.ResourcesManagement;
using UnityEngine.UI;

public class Tent : AInteractable
{
    public Image blackscreen;
    private bool fading = false;

    private void Awake()
    {
        if (_hidedCanvasName != null && _hidedCanvasUsage != null)
            triggerCanvas(false);
    }

    public override void Interact(PlayerController playerController)
    {
        if (fading == false)
        {
            fading = true;
            var energy = playerController.GetPlayerStats().Resources[Resource.Energy];
            energy.Quantity = energy.Limit;
            var corout = StartCoroutine(Fade());
        }
    }

    private void triggerCanvas(bool visible)
    {
        Color colName = _hidedCanvasName.color;
        Color colUsage = _hidedCanvasUsage.color;
        colName.a = 0;
        colUsage.a = 0;
        if (visible)
        {
            colName.a = 255;
            colUsage.a = 255;
        }
        _hidedCanvasUsage.color = colUsage;
        _hidedCanvasName.color = colName;
    }

    protected override void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (_hidedCanvasName != null && _hidedCanvasUsage != null)
                triggerCanvas(true);
        }
    }

    protected override void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (_hidedCanvasName != null && _hidedCanvasUsage != null)
                triggerCanvas(false);
        }
    }

    IEnumerator Fade()
    {
        var tempColor = blackscreen.color;

        Time.timeScale = 10;
        while (blackscreen.color.a < 1f)
        {
            tempColor.a += Time.deltaTime * 0.05f;
            blackscreen.color = tempColor;
            yield return null;
        }
        Time.timeScale = 1;
        while (blackscreen.color.a >= 0f)
        {
            tempColor.a -= Time.deltaTime * 0.2f;
            blackscreen.color = tempColor;
            yield return null;
        }
        fading = false;
    }
}
