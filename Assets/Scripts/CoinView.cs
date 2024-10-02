using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoinView : MonoBehaviour
{
    [SerializeField] CoinModel model;
    [SerializeField] Sprite[] spriteDigits;

    private Image coin10Image;
    private Image coin1Image;

    private void Awake()
    {
        coin10Image = transform.GetChild(0).GetComponent<Image>();
        coin1Image = transform.GetChild(1).GetComponent<Image>();
    }

    private void Start()
    {
        UpdateCoin(model.Coin);
    }

    public void UpdateCoin(int coin)
    {
        int coin10 = coin / 10;
        int coin1 = coin % 10;

        coin10Image.sprite = spriteDigits[coin10];
        coin1Image.sprite = spriteDigits[coin1];
    }
}
