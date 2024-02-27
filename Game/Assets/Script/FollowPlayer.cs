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
            transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, smoothness);
        }
    }
}