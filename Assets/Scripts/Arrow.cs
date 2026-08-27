using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 10f;
    [SerializeField] private float timeToLive = 1f;
    [SerializeField] private float damage = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    void Start()
    {
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {

        if(timeToLive <= 0)
        {
            Destroy(gameObject);
        }

        timeToLive -= Time.deltaTime;
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
    }
}
