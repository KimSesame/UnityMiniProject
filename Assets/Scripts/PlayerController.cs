using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int maxHp = 10;
    [SerializeField] int curHp;
    [SerializeField] int damage;

    private AudioSource audioSource;
    private GameObject gfx;
    private GameObject body;
    private GameObject face;
    private SpriteRenderer bodySpRenderer;
    private SpriteRenderer faceSpRenderer;
    private float cellSize;

    public int Damage { get { return damage; } }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        curHp = maxHp;
    }

    private void Start()
    {
        gfx = transform.GetChild(0).gameObject;
        body = gfx.transform.GetChild(0).gameObject;
        face = gfx.transform.GetChild(1).gameObject;
        bodySpRenderer = body.GetComponent<SpriteRenderer>();
        faceSpRenderer = face.GetComponent<SpriteRenderer>();
        cellSize = GameManager.Instance.map.cellSize.x;
    }

    private void Update()
    {
        Move();
    }

    private void OnDestroy()
    {
        if (jumpCoroutine != null)
            StopCoroutine(jumpCoroutine);
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
            bodySpRenderer.flipX = true;
            faceSpRenderer.flipX = true;

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement = new Vector3(+cellSize, 0, 0);
            bodySpRenderer.flipX = false;
            faceSpRenderer.flipX = false;
        }
        else
            return;

        InputManager.Instance.IsInput = true;

        // Occupied tile
        Collider2D collider = Physics2D.OverlapCircle(transform.position + movement, 0.2f);
        if (collider != null)
        {
            GameObject target = collider.gameObject;

            // Interaction
            target.GetComponent<IInteractor>().Interaction();
        }
        // Empty tile
        else
        {
            // Jump to move
            StartCoroutine(JumpRoutine(movement));
        }

        // Ignore next inputs in same valid timing
        if (InputManager.Instance.IsValid != false)
            InputManager.Instance.IsValid = false;
    }

    private void Jump(Vector3 targetPos, float jumpElapsed, float jumpDuration)
    {
        float jumpProgress = jumpElapsed / jumpDuration;
        float jumpHeight = Mathf.Sin(jumpProgress * Mathf.PI) / 2;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime / jumpDuration);
        gfx.transform.localPosition = new Vector3(0, jumpHeight);
    }

    Coroutine jumpCoroutine;
    IEnumerator JumpRoutine(Vector3 movement)
    {
        Vector3 targetPos = transform.position + movement;
        float animationRate = GameManager.Instance.beatInterval - 2 * GameManager.Instance.beatSlack;
        float jumpDuration = 60 / GameManager.Instance.bpm * animationRate;
        float jumpElapsed = 0f;

        while (jumpElapsed < jumpDuration)
        {
            jumpElapsed += Time.deltaTime;

            Jump(targetPos, jumpElapsed, jumpDuration);
            yield return null;
        }

        transform.position = targetPos;
    }

    public void Attack(Monster target)
    {
        audioSource.Play();

        target.TakeDamage();
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        if (curHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
