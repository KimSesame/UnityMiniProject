using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static UnityAction OnTurnEnd;    // Player timing end, Monsters can behave

    public static InputManager Instance { get; private set; }

    [SerializeField] bool isValid;

    public bool IsValid { get { return isValid; } set { isValid = value; if (value) OnTurnEnd?.Invoke(); } }
    
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
    }
}
