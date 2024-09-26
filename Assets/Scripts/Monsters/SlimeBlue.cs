using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBlue : Slime
{
    private void Awake()
    {
        dx = new int[] { 0, 0 };
        dy = new int[] { 1, -1 };
    }
}
