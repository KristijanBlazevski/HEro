using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 40f;
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Transform player;

    private bool isAttacking;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        LookAtPlayer();
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        if (isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction =
            (player.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }

    private void LookAtPlayer()
    {
        Vector2 direction =
            player.position - transform.position;

        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.rotation =
            Quaternion.Euler(0f, 0f, angle);
    }

    public void StartAttack()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;
    }

    public void StopAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        Debug.Log("Melee Enemy HP: " + health);

        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}