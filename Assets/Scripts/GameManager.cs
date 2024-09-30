using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Grid map;
    public MonsterContatiner[] monsters = { new(), new() };
    public int bufferIdx;
    public float bpm;
    public float beatSlack;
    public float beatInterval;

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

        Init();
    }

    private void Update()
    {
        AlarmBeat();
    }

    private void OnDestroy()
    {
        InputManager.OnTurnEnd -= BehaveMonster;
    }

    private void Init()
    {
        InputManager.OnTurnEnd += BehaveMonster;

        bufferIdx = 0;
        bpm = 60f;
        beatSlack = 0.2f;

        beatInterval = 60f / bpm;
        timer = beatInterval;
    }

    private void AlarmBeat()
    {
        timer -= Time.deltaTime;

        // Start point of player's valid input range
        if (timer <= beatSlack)
        {
            if (!InputManager.Instance.IsInput)
                InputManager.Instance.IsValid = true;
        }
        // End point of player's valid input range
        else if (timer <= beatInterval - beatSlack)
        {
            if (InputManager.Instance.IsValid != false)
                InputManager.Instance.IsValid = false;
            InputManager.Instance.IsInput = false;
        }

        // Beat
        if (timer <= 0f)
        {
            timer += beatInterval;
        }
    }

    private void BehaveMonster()
    {
        // Monster with high priority behaves faster
        while (monsters[bufferIdx].Count > 0)
        {
            Monster curMonster = monsters[bufferIdx].ExtractMax();
            // Behave
            curMonster.OnBeat();
            // Move to the other buffer
            monsters[bufferIdx ^ 1].Insert(curMonster);
        }
        bufferIdx ^= 1; // toggle buffer
    }
}
