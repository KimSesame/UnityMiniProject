using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [SerializeField] protected GameObject target;
    [SerializeField] protected int hp;
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
}
