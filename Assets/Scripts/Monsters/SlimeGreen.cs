using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGreen : Slime
{
    protected override void Awake()
    {
        base.Awake();

        minCoin = 1;
        maxCoin = 5;

        dx = new int[] { 0 };
        dy = new int[] { 0 };
    }

    private void OnDestroy()
    {
        // Remove at monter conatainer
        contatiner[GameManager.Instance.bufferIdx].RemoveAt(contatiner[GameManager.Instance.bufferIdx].FindIndex(this));

        // Remove coroutines
        StopAllCoroutines();
    }
}
