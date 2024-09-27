using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour, IBeatListener, IComparable<Monster>
{
    [SerializeField] protected GameObject target;
    [SerializeField] protected int hp;
    [SerializeField] protected int dmg;
    [SerializeField] protected int period;
    [SerializeField] protected int priority;
    [SerializeField] protected int[] dx;
    [SerializeField] protected int[] dy;
    [SerializeField] protected int d_i;

    protected MonsterContatiner[] contatiner;
    protected float cellSize;
    protected int cnt;

    private void Start()
    {
        contatiner = GameManager.Instance.monsters;
        contatiner[GameManager.Instance.bufferIdx].Insert(this);

        target = GameObject.FindGameObjectWithTag("Player");
        cellSize = GameManager.Instance.map.cellSize.x;
        cnt = 0;
    }

    private void OnDestroy()
    {
        contatiner[GameManager.Instance.bufferIdx].RemoveAt(contatiner[GameManager.Instance.bufferIdx].FindIndex(this));
    }

    public abstract void OnBeat();

    public int CompareTo(Monster other)
    {
        if (other == null) return 1;
        return this.priority.CompareTo(other.priority);
    }

    public virtual void Move()
    {
        Vector3 movement = cellSize * new Vector3(dx[d_i], dy[d_i]);
        Collider2D collider = Physics2D.OverlapCircle(transform.position + movement, 0.2f);
        // Tile occupied
        if (collider != null)
            return;

        transform.position += movement;
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
