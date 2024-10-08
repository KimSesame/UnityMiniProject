using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour, IBeatListener, IInteractor, IComparable<Monster>
{
    public enum SFXName
    {
        Attack, Hit, Die, SIZE
    }

    [SerializeField] protected GameObject target;
    [SerializeField] protected AudioClip[] sfxs;

    [Header("Attributes")]
    [SerializeField] protected int hp;
    [SerializeField] protected int dmg;
    [SerializeField] protected int period;
    [SerializeField] protected int priority;

    [Header("Coin")]
    [SerializeField] protected Coin coinPrefab;
    [SerializeField] protected int minCoin;
    [SerializeField] protected int maxCoin;

    [Header("Movement Pattern")]
    [SerializeField] protected int[] dx;
    [SerializeField] protected int[] dy;
    [SerializeField] protected int d_i;

    protected MonsterContatiner[] contatiner;
    protected float cellSize;
    protected int cnt;

    protected AudioSource audioSource;
    protected SpriteRenderer vfx;
    protected float effectTime;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        vfx = transform.GetChild(0).GetComponent<SpriteRenderer>();

        effectTime = 0.1f;
    }

    private void Start()
    {
        contatiner = GameManager.Instance.monsters;
        contatiner[GameManager.Instance.bufferIdx].Insert(this);

        target = GameObject.FindGameObjectWithTag("Player");
        cellSize = GameManager.Instance.map.cellSize.x;
        cnt = 0;
    }

    public abstract void OnBeat();

    public void Interaction()
    {
        takeDamageCoroutine = StartCoroutine(TakeDamageRoutine());
    }

    public int CompareTo(Monster other)
    {
        if (other == null) return 1;
        return this.priority.CompareTo(other.priority);
    }

    protected void Move()
    {
        Vector3 movement = cellSize * new Vector3(dx[d_i], dy[d_i]);
        Vector3 targetPos = transform.position + movement;
        Collider2D collider = Physics2D.OverlapCircle(targetPos, 0.3f);

        // Tile occupied
        if (collider != null)
        {
            // Attack if player
            if (collider.gameObject.Equals(target))
                Attack();
            return;
        }

        // Empty tile
        transform.position = targetPos;
        d_i = (d_i + 1) % dx.Length;
    }

    protected void Attack()
    {
        PlayerController player = target.GetComponent<PlayerController>();
        if (player != null)
        {
            audioSource.clip = sfxs[(int)SFXName.Attack];
            audioSource.Play();
            player.TakeDamage(dmg);
        }
    }

    public void TakeDamage()
    {
        hp -= target.GetComponent<PlayerController>().Damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    protected Coroutine takeDamageCoroutine;
    IEnumerator TakeDamageRoutine()
    {
        // Effects
        vfx.gameObject.SetActive(true);
        audioSource.clip = sfxs[(int)SFXName.Hit];
        audioSource.Play();
        yield return new WaitForSeconds(effectTime);
        vfx.gameObject.SetActive(false);

        target.GetComponent<PlayerController>().Attack(this);
    }

    private void Die()
    {
        dieCoroutine = StartCoroutine(DieRoutine());

        // Drop coins
        Coin newCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        newCoin.Amount = UnityEngine.Random.Range(minCoin, maxCoin + 1);
    }

    Coroutine dieCoroutine;
    IEnumerator DieRoutine()
    {
        // Effects
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        audioSource.clip = sfxs[(int)SFXName.Die];
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
