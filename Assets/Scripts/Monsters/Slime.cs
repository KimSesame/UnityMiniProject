using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    public override void OnBeat()
    {
        // Work periodically
        if (!InPeriod()) return;

        Move();
    }

    private bool InPeriod() => period > 0 && ++cnt % period == 0;
}
