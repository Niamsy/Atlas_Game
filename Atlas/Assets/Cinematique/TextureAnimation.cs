using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimation : MonoBehaviour
{
	public Material material;
	public float scrollSpeed;
	public Vector3 Axe;

    void Update()
    {
		float offset = Time.time * scrollSpeed;
		material.SetTextureOffset("_MainTex", new Vector2(offset * Axe.x, offset * Axe.y));
    }
}
