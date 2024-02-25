using UnityEngine;

public class KillPlayerOnTouch : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Player>()) Destroy(collision.gameObject);
    }
}