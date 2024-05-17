using Script.GameScript;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinController : MonoBehaviour
{
    private Transform coinChildElement;
    private GameStatsController gameStatsController;
    public AudioClip sound;
    private AudioSource audioSource;
    private Renderer coinRenderer;
    private Color originalColor;

    public static int Coins { get; private set; }
    
    private void Awake()
    {
        gameStatsController = new GameStatsController();
    }
    
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                boxCollider.enabled = false;
            }
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = false;
            }
            StartCoroutine(PlaySoundAndDestroyCoin());
        }
    }

    private IEnumerator PlaySoundAndDestroyCoin()
    {
        if (sound && audioSource)
        {
            audioSource.volume = OptionsMenu.volumeSound;
            audioSource.PlayOneShot(sound);
            
            yield return new WaitForSeconds(sound.length);
        }
        Destroy(gameObject);
    }
}