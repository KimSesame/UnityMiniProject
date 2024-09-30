using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSender : MonoBehaviour
{
    [SerializeField] GameObject notePrefab;

    [SerializeField] bool isRight;
    [SerializeField] float delay;

    private void Start()
    {
        // Start note sending coroutine
        sendCoroutine = StartCoroutine(SendRoutine());
    }

    private void OnDestroy()
    {
        // Stop note sending coroutine
        StopCoroutine(sendCoroutine);
    }

    private void Send()
    {
        Note newNote = Instantiate(notePrefab, transform).GetComponent<Note>();
        newNote.IsRight = isRight;
    }

    Coroutine sendCoroutine;
    IEnumerator SendRoutine()
    {
        while (true)
        {
            Send();
            yield return new WaitForSeconds(GameManager.Instance.beatInterval);
        }
    }
}
