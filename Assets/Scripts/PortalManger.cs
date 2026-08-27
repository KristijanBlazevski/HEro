using UnityEngine;

public class PortalManger : MonoBehaviour
{
    [SerializeField] Transform connectedTo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && connectedTo != null)
        {
            collision.transform.position = connectedTo.position;
            connectedTo.parent.gameObject.SetActive(true);
            Vector3 roomPos = connectedTo.parent.position;
            Camera.main.transform.position = new Vector3(roomPos.x, roomPos.y, Camera.main.transform.position.z);
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
