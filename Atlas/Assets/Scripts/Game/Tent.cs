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
        if (fading == false)
        {
            playerController.Asleep = true;
            fading = true;
            var energy = playerController.GetPlayerStats().Resources[Resource.Energy];
            energy.Quantity = energy.Limit;
            var corout = StartCoroutine(Fade(playerController));
        }
    }

    IEnumerator Fade(PlayerController playerController)
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
        playerController.Asleep = false;
        while (blackscreen.color.a >= 0f)
        {
            tempColor.a -= Time.deltaTime * 0.2f;
            blackscreen.color = tempColor;
            yield return null;
        }
        fading = false;
    }
}
