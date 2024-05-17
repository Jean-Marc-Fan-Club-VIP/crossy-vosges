using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothness;


    private void Update()
    {
        if (player)
        {
            Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z / 1.5f);
            transform.position = Vector3.Lerp(transform.position, playerPosition + offset, smoothness);
        }
    }
}