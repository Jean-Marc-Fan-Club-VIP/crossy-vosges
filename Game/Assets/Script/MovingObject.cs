using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public bool isLog;
    private float speed;

    public float Speed
    {
        set => speed = value;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        if (transform.position.z is < -25 or > 25) Destroy(gameObject);
    }
}