using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float attackDelay = 0.2f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private GameObject attackPrefab;
    private bool playerInside;
    private bool attacking;

    private Enemy enemy;
    private PlayerInputs player;

    private float cooldownTimer;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (playerInside && !attacking && cooldownTimer <= 0f)
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        attacking = true;

        enemy.StartAttack();

        GameObject attack =
            Instantiate(
                attackPrefab,
                transform.position,
                transform.rotation
            );

    Destroy(attack, attackDelay);
        Invoke(nameof(DealDamage), attackDelay);
    }

    private void DealDamage()
    {
        if (playerInside && player != null)
        {
            player.TakeDamage(damage);
        }

        attacking = false;
        cooldownTimer = attackCooldown;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;

            player =
                collision.GetComponent<PlayerInputs>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
            player = null;

            enemy.StopAttack();
        }
    }
}