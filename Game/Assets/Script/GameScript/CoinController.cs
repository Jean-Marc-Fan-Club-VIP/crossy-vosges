using Script.GameScript;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private static int coins;
    private Transform coinChildElement;
    private GameStatsController gameStatsController;
    
    
    private void Start()
    {
        coinChildElement = transform.Find("CoinContainer");
    }
    
    private void Update()
    {
        coinChildElement.transform.Rotate(0, Time.deltaTime * 100, 0);
        coinChildElement.transform.position = new Vector3(coinChildElement.transform.position.x,
            coinChildElement.transform.position.y + Mathf.Sin(Time.time * 2) * 0.002f * Time.timeScale,
            coinChildElement.transform.position.z);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<Player>())
        {
            EventManager.UpdateCoins(++coins);
            Destroy(gameObject);
        }
    }
}