using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IInteractor
{
    [SerializeField] bool isSmooth;

    private AudioSource audioSource;
    private SpriteRenderer vfx;
    private float effectTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        vfx = transform.GetChild(0).GetComponent<SpriteRenderer>();

        effectTime = 0.2f;
    }

    private void OnDestroy()
    {
        if (shovelCoroutine != null)
            StopCoroutine(shovelCoroutine);
    }

    public void Interaction()
    {
        shovelCoroutine = StartCoroutine(ShovelRoutine());
    }

    Coroutine shovelCoroutine;
    IEnumerator ShovelRoutine()
    {
        // Effects
        vfx.gameObject.SetActive(true);
        audioSource.Play();
        yield return new WaitForSeconds(effectTime);
        vfx.gameObject.SetActive(false);

        // Destroy itself if smooth
        if (isSmooth)
        {
            Destroy(gameObject);
        }
    }
}
