using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 50f;
    [SerializeField] private float moveSpeed = 2f;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes

=======
    [SerializeField] private float health = 40f;
>>>>>>> Stashed changes
    private Rigidbody2D rb;
    private Transform player;

    private bool isAttacking;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
<<<<<<< Updated upstream

    
=======
>>>>>>> Stashed changes
    }

    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

<<<<<<< Updated upstream
        

        player = playerObject.transform;

        
=======
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
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======

    private void LookAtPlayer()
    {
        Vector2 direction =
            player.position - transform.position;

<<<<<<< Updated upstream
        Debug.Log("Enemy HP: " + health);
=======
        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
>>>>>>> Stashed changes

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
>>>>>>> Stashed changes
}