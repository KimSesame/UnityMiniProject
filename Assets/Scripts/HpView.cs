using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpView : MonoBehaviour
{
    [SerializeField] HpModel model;
    [SerializeField] Sprite[] spriteHearts;

    private Image hp0Image;
    private Image hp1Image;

    private void Awake()
    {
        hp0Image = transform.GetChild(0).GetComponent<Image>();
        hp1Image = transform.GetChild(1).GetComponent<Image>();
    }

    private void Start()
    {
        UpdateHp(model.Hp);
    }

    public void UpdateHp(int hp)
    {
        /*
         *      hp  /  hp0  /  hp1
         *      0   |   0   |   0
         *      1   |   0   |   1
         *      2   |   0   |   2
         *      3   |   1   |   2
         *      4   |   2   |   2
         */
        int hp0 = Mathf.Max(hp - 2, 0);
        int hp1 = (hp >= 2) ? 2 : hp;

        hp0Image.sprite = spriteHearts[hp0];
        hp1Image.sprite = spriteHearts[hp1];
    }
}
