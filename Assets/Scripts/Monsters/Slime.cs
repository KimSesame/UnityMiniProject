using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    public override void Behave()
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

    private void Move()
    {
        transform.position += cellSize * new Vector3(dx[d_i], dy[d_i]);
        d_i = (d_i + 1) % dx.Length;
    }

    private void Attack()
    {
        Debug.Log("Slime Attack!");
    }
}
