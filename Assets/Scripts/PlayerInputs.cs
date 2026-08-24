using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class PlayerInputs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float attDamage = 10f;
    [SerializeField] private float attSpeed = 1f;
    [SerializeField] private float attRange = 5f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;
    private float attTimer;
    private Rigidbody2D rb;
    private Vector2 movement;

     private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.zero;

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

        attTimer -= Time.deltaTime;

        if(movement == Vector2.zero && attTimer <= 0f)
        {
            Attack();
            attTimer=attSpeed;
        }
    }   

    private Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        Transform nearestEnemy = null;
        float nearestDistance = attRange;
        
        foreach(GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if(distance< nearestDistance)
            {
                nearestEnemy=enemy.transform;
            }
        }

        return nearestEnemy;
    }   

    private void Attack()
    {
        Transform target = FindNearestEnemy();

        if(target == null)
        {
            return;
        }

        lookAtEnemy(target.position);

        GameObject arrow = Instantiate(
            arrowPrefab,
            firePoint.position,
            transform.rotation
        );

    }
    private void FixedUpdate()
    {
        
        rb.AddForce(movement * moveSpeed, ForceMode2D.Impulse);
        lookAtDirection();
    }

    private void lookAtDirection()
    {
        //If player does not move return
        if(movement == Vector2.zero)
        {
            return;
        }

        Vector2 lookAt = transform.InverseTransformPoint(new Vector2(transform.position.x, transform.position.y) + movement);
        float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg - 90;

        transform.Rotate(0, 0, angle);
    }

    private void lookAtEnemy(Vector3 targetPos)
    {
        Vector2 lookAt = transform.InverseTransformPoint(new Vector2(targetPos.x, targetPos.y) + movement);
        float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg - 90;

        transform.Rotate(0, 0, angle);
    }
}
