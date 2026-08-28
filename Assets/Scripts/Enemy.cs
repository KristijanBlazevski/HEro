using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 40f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Slider healthBar;
    private float currentHealth;
    private Rigidbody2D rb;
    private Transform player;
    private EnemySpawner enemySpawner;

    private bool isAttacking;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemySpawner = GameObject.Find("MeleeSpawner").GetComponent<EnemySpawner>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
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
        healthBar.transform.rotation = Quaternion.identity;
        healthBar.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.7f);
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
        currentHealth -= damageAmount;
        healthBar.value = currentHealth;

        if (currentHealth <= 0f)
        {
            // enemySpawner.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}