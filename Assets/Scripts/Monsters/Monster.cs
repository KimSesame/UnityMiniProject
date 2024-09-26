using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [SerializeField] protected GameObject target;
    [SerializeField] protected int hp;
    [SerializeField] protected int dmg;
    [SerializeField] protected int period;
    [SerializeField] protected int[] dx;
    [SerializeField] protected int[] dy;

    protected float cellSize;
    protected int d_i;
    protected int cnt;

    private void Start()
    {
        InputManager.OnTurnEnd += Behave;

        target = GameObject.FindGameObjectWithTag("Player");
        cellSize = GameManager.Instance.map.cellSize.x;
        d_i = 0;
        cnt = 0;
    }

    private void OnDestroy()
    {
        InputManager.OnTurnEnd -= Behave;
    }

    public abstract void Behave();

    public virtual void Move()
    {
        transform.position += cellSize * new Vector3(dx[d_i], dy[d_i]);
        d_i = (d_i + 1) % dx.Length;
    }

    public virtual void Attack()
    {
        PlayerController player = target.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(dmg);
        }
    }
}
