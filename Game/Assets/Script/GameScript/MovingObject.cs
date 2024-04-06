using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public bool isLog;
    public float speed = 2;
    public float leftBound;
    public float rightBound;

    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        var position = transform.position;
        if (position.z < leftBound || position.z > rightBound || position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}