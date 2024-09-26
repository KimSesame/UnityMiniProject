using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject face;

    private SpriteRenderer spRenderer;
    private SpriteRenderer faceSpRenderer;
    private float cellSize;

    private void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        faceSpRenderer = face.GetComponent<SpriteRenderer>();
        cellSize = GameManager.Instance.map.cellSize.x;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Ignore input during invalid timing
        if (InputManager.Instance.IsValid == false)
            return;

        // Set movement vector
        Vector3 movement;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement = new Vector3(0, +cellSize, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement = new Vector3(0, -cellSize, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement = new Vector3(-cellSize, 0, 0);
            spRenderer.flipX = true;
            faceSpRenderer.flipX = true;

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement = new Vector3(+cellSize, 0, 0);
            spRenderer.flipX = false;
            faceSpRenderer.flipX = false;
        }
        else
            return;

        // Monster exists
        Collider2D collider = Physics2D.OverlapCircle(transform.position + movement, 0.2f);
        Monster monster = collider?.GetComponent<Monster>();
        if (monster != null)
        {
            monster.Attack();

            // Ignore next inputs in same valid timing
            InputManager.Instance.IsValid = false;
            return;
        }

        // Move
        transform.position = transform.position + movement;

        // Ignore next inputs in same valid timing
        InputManager.Instance.IsValid = false;
    }
}
