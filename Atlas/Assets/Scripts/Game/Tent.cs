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
    public override void Interact(PlayerController playerController)
    {
        Debug.Log("TIME BEFORE :" + Time.timeScale);
        if (fading == false)
        {
            fading = true;
            var energy = playerController.GetPlayerStats().Resources[Resource.Energy];
            energy.Quantity = energy.Limit;
            var corout = StartCoroutine(Fade());
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
            Debug.Log(blackscreen.color.a);
            tempColor.a -= Time.deltaTime * 0.2f;
            blackscreen.color = tempColor;
            yield return null;
        }
        fading = false;
        Debug.Log("DONE");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
