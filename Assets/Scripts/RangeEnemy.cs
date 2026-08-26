using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private float health = 25f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackRange = 6f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float repositionTime = 3f;
    [SerializeField] private float repositionDistance = 2f;
    private Rigidbody2D rb;
    private Transform player;

    private float attackTimer;
    private float repositionTimer;

    private Vector2 targetPosition;
    private bool isRepositioning;

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

        attackTimer = attackCooldown;
        repositionTimer = repositionTime;
    }

    private void Update()
    {
        if (player == null)
            return;

        attackTimer -= Time.deltaTime;
        repositionTimer -= Time.deltaTime;

        float distance =
            Vector2.Distance(transform.position, player.position);

        // If we are close enough to attack
        if (distance <= attackRange && !isRepositioning)
        {
            if (attackTimer <= 0f)
            {
                Attack();
                attackTimer = attackCooldown;
            }

            if (repositionTimer <= 0f)
            {
                ChooseNewPosition();
                repositionTimer = repositionTime;
            }
        }
        lookAtPlayer();
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        float distance =
            Vector2.Distance(transform.position, player.position);

        // Move toward player until attack range
        if (!isRepositioning && distance > attackRange)
        {
            Vector2 direction =
                (player.position - transform.position).normalized;

            rb.linearVelocity = direction * moveSpeed;
        }
        // Move toward new position
        else if (isRepositioning)
        {
            Vector2 direction =
                (targetPosition - (Vector2)transform.position).normalized;

            rb.linearVelocity = direction * moveSpeed;

            float distanceToTarget =
                Vector2.Distance(transform.position, targetPosition);

            if (distanceToTarget < 0.2f)
            {
                rb.linearVelocity = Vector2.zero;
                isRepositioning = false;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void ChooseNewPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        targetPosition =
            (Vector2)transform.position +
            randomDirection * repositionDistance;

        isRepositioning = true;
        repositionTimer = repositionTime;
    }

    private void Attack()
    {
        if (player == null)
            return;

        Vector2 direction =
            (player.position - transform.position).normalized;

        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            transform.rotation
        );
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        Debug.Log("Ranged Enemy HP: " + health);

        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
    private void lookAtPlayer()
    {
        Vector2 direction =
            player.position - transform.position;

        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.rotation =
            Quaternion.Euler(0f, 0f, angle);
    }
}