using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    public override void OnBeat()
    {
        // Work periodically
        if (!InPeriod()) return;

        if (InRange(target))
        {
            Attack();
        }
        else
        {
            Move();
        }
    }

    private bool InPeriod() => period > 0 && ++cnt % period == 0;

    private bool InRange(GameObject target)
    {
        Vector3 pos = transform.position + cellSize * new Vector3(dx[d_i], dy[d_i]);

        if (Vector2.Distance(pos, target.transform.position) < 0.1f)
            return true;

        return false;
    }
}
