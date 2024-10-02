using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HpModel : MonoBehaviour
{
    public UnityEvent<int> OnHpChanged;

    [SerializeField] int maxHp;
    [SerializeField] int hp;
    public int Hp {  get { return hp; } set { hp = Mathf.Clamp(value, 0, maxHp); OnHpChanged?.Invoke(hp); } }

    private void Awake()
    {
        hp = maxHp;
    }
}
