using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteReceiver : MonoBehaviour, IBeatListener
{
    public enum HeartSprite
    {
        Idle, Beat, SIZE
    }

    [SerializeField] Sprite[] heartSprites;

    private Image heartImage;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    private void Start()
    {
        heartImage.sprite = heartSprites[(int)HeartSprite.Idle];
    }

    private void OnDestroy()
    {
        StopCoroutine(heartBeatCoroutine);
    }

    public void OnBeat() => heartBeatCoroutine = StartCoroutine(HeartBeatRoutine());

    Coroutine heartBeatCoroutine;
    IEnumerator HeartBeatRoutine()
    {
        heartImage.sprite = heartSprites[(int)HeartSprite.Beat];
        yield return new WaitForSeconds(0.1f);
        heartImage.sprite = heartSprites[(int)HeartSprite.Idle];
    }

}
