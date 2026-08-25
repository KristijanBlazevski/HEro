using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float health = 40f;

    private Rigidbody2D rb;
    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if(playerObject != null)
        {
             player = playerObject.transform;
        }
        
    }

    private void FixedUpdate()
    {
        if (rb == null || player == null)
            return;

        Vector2 direction = (player.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        // Debug.Log("Enemy HP: " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}