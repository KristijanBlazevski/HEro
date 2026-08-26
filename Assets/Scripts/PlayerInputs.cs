using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float health = 100f;
    [SerializeField] private float attSpeed = 1f;
    [SerializeField] private float attRange = 5f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;
    private Rigidbody2D rb;
    private Vector3 movement;
    private float attTimer;
    private Transform target;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        attTimer = 0f;
    }

    private void Update()
    {
        movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movement.y += 1;

        if (Input.GetKey(KeyCode.S))
            movement.y -= 1;

        if (Input.GetKey(KeyCode.A))
            movement.x -= 1;

        if (Input.GetKey(KeyCode.D))
            movement.x += 1;

        movement = movement.normalized;

        FindClosestEnemy();

        attTimer -= Time.deltaTime;

        if (movement == Vector3.zero)
        {
            if (target != null && attTimer <= 0f)
            {
                Attack();
                attTimer = 1f / attSpeed;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(
        movement * moveSpeed,
        ForceMode2D.Impulse
        );

        if (movement != Vector3.zero)
        {
            float angle =
                Mathf.Atan2(movement.y, movement.x)
                * Mathf.Rad2Deg - 90f;

            transform.rotation =
                Quaternion.Euler(0f, 0f, angle);
        }
    
        else if (target != null)
        {
            LookAtEnemy();
        }
    }

    private void FindClosestEnemy()
    {
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag("Enemy");

        float closestDistance = attRange;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance =
                Vector2.Distance(
                    transform.position,
                    enemy.transform.position
                );

            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        target = closestEnemy;
    }

    private void LookAtEnemy()
    {
        Vector2 direction =
            target.position - transform.position;

        float angle =
            Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg - 90f;

        transform.rotation =
            Quaternion.Euler(0f, 0f, angle);
    }

    private void Attack()
    {
        if (target == null)
            return;

        LookAtEnemy();

        Instantiate(
            arrowPrefab,
            firePoint.position,
            transform.rotation
        );
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