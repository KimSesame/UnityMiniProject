using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

public class Note : MonoBehaviour
{
    [Header("Target Layer")]
    [SerializeField] int noteLayer;

    [Header("Attributes")]
    [SerializeField] bool isRight;
    [SerializeField] float speed;

    public bool IsRight { set { isRight = value; } }

    private void Awake()
    {
        noteLayer = LayerMask.NameToLayer("Note");
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Handle only right notes
        if (!isRight) return;

        // End of valid input range
        if (collision.gameObject.layer == noteLayer)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        int moveDir = (isRight) ? -1 : +1;   // left: -1, right: +1
        transform.Translate(moveDir * speed * Time.deltaTime, 0, 0);
    }
}
