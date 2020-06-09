using Player;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class AInteractable : MonoBehaviour
{
    [SerializeField]
    public Text _hidedCanvasName;
    [SerializeField]
    public Text _hidedCanvasUsage;

    public enum InteractAnim {
        none = 0,
        pick = 1,
        activate = 2
    }

    public InteractAnim anim;

    public abstract void Interact(PlayerController playerController);

    protected abstract void OnTriggerEnter(Collider col);

    protected abstract void OnTriggerExit(Collider col);
}
