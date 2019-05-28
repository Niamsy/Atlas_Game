using Player;
using System;
using UnityEngine;

public abstract class AInteractable : MonoBehaviour
{
    public enum InteractType {
        none = 0,
        pick = 1,
        activate = 2
    }

    public abstract void Interact(PlayerController playerController);
}
