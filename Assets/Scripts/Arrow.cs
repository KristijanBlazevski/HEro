using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float timeToLive = 1f;
    [SerializeField] private float damage = 10f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }

        timeToLive -= Time.deltaTime;
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            RangedEnemy rangeEnemy = collision.gameObject.GetComponent<RangedEnemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (rangeEnemy != null)
            {
                rangeEnemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}