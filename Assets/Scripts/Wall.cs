using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IInteractor
{
    [SerializeField] bool isSmooth;

    public void Interaction()
    {
        if (isSmooth)
        {
            Destroy(gameObject);
        }
    }
}
