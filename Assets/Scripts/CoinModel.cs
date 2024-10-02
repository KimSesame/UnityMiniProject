using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinModel : MonoBehaviour
{
    public UnityEvent<int> OnCoinChanged;

    private int coin;
    public int Coin { get { return coin; } set { coin = Mathf.Clamp(value, 0, 99); OnCoinChanged?.Invoke(coin); } }

    private void Awake()
    {
        coin = 0;
    }
}
