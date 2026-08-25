using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float timeToLive = 3f;
    [SerializeField] private float damage = 20f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.linearVelocity = transform.up * speed;
    }

    private void Update()
    {
        timeToLive -= Time.deltaTime;

        if (timeToLive <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInputs player =
                collision.gameObject.GetComponent<PlayerInputs>();

            if (player != null)
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}