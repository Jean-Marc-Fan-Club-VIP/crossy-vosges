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
        var positionZ = transform.position.z;
        if (positionZ < leftBound || positionZ > rightBound)
        {
            Destroy(gameObject);
        }
    }
}