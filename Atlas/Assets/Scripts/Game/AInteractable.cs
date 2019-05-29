using Player;
using System;
using UnityEngine;

public abstract class AInteractable : MonoBehaviour
{
    public enum InteractAnim {
        none = 0,
        pick = 1,
        activate = 2
    }

    public InteractAnim anim;

    public abstract void Interact(PlayerController playerController);
}
