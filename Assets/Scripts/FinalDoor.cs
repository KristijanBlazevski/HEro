using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    [SerializeField] private VictoryScreen victoryScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            victoryScreen.ShowVictory();
        }
    }
}