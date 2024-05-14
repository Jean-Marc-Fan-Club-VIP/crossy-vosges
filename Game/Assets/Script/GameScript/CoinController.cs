using Script.GameScript;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private Transform coinChildElement;
    private GameStatsController gameStatsController;
    
    public static int Coins { get; private set; }
    
    private void Awake()
    {
        gameStatsController = new GameStatsController();
    }
    
    
    private void Start()
    {
        coinChildElement = transform.Find("CoinContainer");
        var stats = gameStatsController.GetGameStats();
        if (stats.TryGetValue(OptionsMenu.PlayerName, out var stat))
        {
            Coins = stat.Coins;
            EventManager.UpdateCoins(Coins);
        }
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
            EventManager.UpdateCoins(++Coins);
            Destroy(gameObject);
        }
    }
}