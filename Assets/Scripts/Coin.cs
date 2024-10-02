using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int amount;
    
    private AudioSource audioSource;
    private float effectTime;

    public int Amount { get { return amount; } set { amount = value; } }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        effectTime = 0.2f;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Coin Up
        coinUpCoroutine = StartCoroutine(CoinUpRoutine());
    }

    private void CoinUp() => GameManager.Instance.coinModel.Coin += amount;

    Coroutine coinUpCoroutine;
    IEnumerator CoinUpRoutine()
    {
        // Effects
        audioSource.Play();
        CoinUp();
        yield return new WaitForSeconds(effectTime);

        Destroy(gameObject);
    }
}
