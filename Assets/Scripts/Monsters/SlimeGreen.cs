using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGreen : Slime
{
    private void Awake()
    {
        dx = new int[] { 0 };
        dy = new int[] { 0 };

        audioSource = GetComponent<AudioSource>();
        vfx = transform.GetChild(0).GetComponent<SpriteRenderer>();

        effectTime = 0.1f;
    }

    private void OnDestroy()
    {
        // Remove at monter conatainer
        contatiner[GameManager.Instance.bufferIdx].RemoveAt(contatiner[GameManager.Instance.bufferIdx].FindIndex(this));

        // Remove coroutines
        StopAllCoroutines();
    }
}
