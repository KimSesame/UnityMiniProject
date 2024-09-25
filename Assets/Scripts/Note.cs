using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public bool isRight;

    [SerializeField] float speed;

    public bool IsRight { set { isRight = value; } }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        int moveDir = (isRight) ? -1 : +1;   // left: -1, right: +1
        transform.Translate(moveDir * speed * Time.deltaTime, 0, 0);
    }
}
