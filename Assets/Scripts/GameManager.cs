using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static UnityAction OnBeatAlarm;

    public Grid map;
    public float bpm;
    public float beatSlack;

    private float beatInterval;
    private float timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        bpm = 60f;
        beatSlack = 0.3f;

        beatInterval = 60f / bpm;
        timer = beatInterval;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        // Start point of player's valid input range
        if (timer <= beatSlack)
        {
            InputManager.Instance.IsValid = true;
        }

        // Beat
        if (timer <= 0f)
        {
            OnBeatAlarm?.Invoke();
            timer = beatInterval;
        }

        // End point of player's valid input range
        if (timer <= beatInterval - beatSlack)
        {
            if (InputManager.Instance.IsValid != false)
                InputManager.Instance.IsValid = false;
        }
    }
}
