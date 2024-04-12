using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public bool isLog;
    public float speed = 2;
    public float leftBound;
    public float rightBound;

    private void Update()
    {   
        int currentLevel = LevelSelector.LevelGame();
        if (currentLevel == 1)
        {
            
        }
        else if (currentLevel == 2)
        {
            speed *= 1.003f;
        }
        
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        var position = transform.position;
        if (position.z < leftBound || position.z > rightBound || position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}