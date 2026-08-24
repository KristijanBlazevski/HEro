using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private float moveSpeed = 1f;

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

    // Update is called once per frame
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
        // rb.linearVelocity = movement * moveSpeed;
        rb.AddForce(movement * moveSpeed, ForceMode2D.Impulse);
        // transform.LookAt(transform.position + movement);
        lookat();
    }

    private void lookat()
    {
        if(movement == Vector3.zero)
        {
            return;
        }
        Vector3 tmp = transform.InverseTransformPoint(transform.position + movement);
        float angle = Mathf.Atan2(tmp.y, tmp.x) * Mathf.Rad2Deg - 90;

        transform.Rotate(0, 0, angle);
    }
}
