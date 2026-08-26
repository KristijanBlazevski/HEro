using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private float health = 100f;

    private Rigidbody2D rb;
    private Vector3 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Debug.Log("HERO INIT");
    }

    void Update()
    {
        movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement.y += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movement.y -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement.x += 1;
        }

        movement = movement.normalized;
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement * moveSpeed, ForceMode2D.Impulse);

        lookat();
    }

    private void lookat()
    {
        if (movement == Vector3.zero)
        {
            return;
        }

        Vector3 tmp =
            transform.InverseTransformPoint(
                transform.position + movement
            );

        float angle =
            Mathf.Atan2(tmp.y, tmp.x) * Mathf.Rad2Deg - 90;

        transform.Rotate(0, 0, angle);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        Debug.Log("Player HP: " + health);

        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}