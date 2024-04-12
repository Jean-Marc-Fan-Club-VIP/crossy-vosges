using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public bool isLog;
    public float speed = 2;
    public float leftBound;
    public float rightBound;

    private bool isObjectVisible() {
        var cam = Camera.main;
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        var objCollider =  GetComponent<Collider>();

        return GeometryUtility.TestPlanesAABB(planes, objCollider.bounds);
    }

    private void Update()
    {
        int speedMultiplier = 1;
        if (!isObjectVisible())
        {
            speedMultiplier = 10;
        }

        transform.Translate(Vector3.forward * (speed * speedMultiplier * Time.deltaTime));
        var position = transform.position;
        if (position.z < leftBound || position.z > rightBound || position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}