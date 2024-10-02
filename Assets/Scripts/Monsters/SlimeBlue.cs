using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBlue : Slime
{
    protected override void Awake()
    {
        base.Awake();

        minCoin = 1;
        maxCoin = 10;

        dx = new int[] { 0, 0 };
        dy = new int[] { 1, -1 };
    }

    private void OnDestroy()
    {
        // Remove at monter conatainer
        contatiner[GameManager.Instance.bufferIdx].RemoveAt(contatiner[GameManager.Instance.bufferIdx].FindIndex(this));

        // Remove coroutines
        StopAllCoroutines();
    }
}
