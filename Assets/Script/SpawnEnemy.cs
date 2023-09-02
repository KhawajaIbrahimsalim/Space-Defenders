using UnityEngine;
using TMPro;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject Enemy;
    public Transform Spawn_Enemy;
    public int SpawnNumber = 1;
    public int SpawnMorePerWave = 1;
    public float WaveCountdown = 3f;
    public float PostWaveCountdown = 40f;
    public float EnemySpread;
    public Canvas TurtorialScreen;
    public TextMeshProUGUI CountDown_Text;

    // Update is called once per frame
    [System.Obsolete]
    void Update() // we make a time limit before the new wave will start
    {
        if (WaveCountdown <= 0 && TurtorialScreen.gameObject.active == false)
        {
            NoOfEnemy();
            SpawnNumber += SpawnMorePerWave;
            WaveCountdown = PostWaveCountdown;
        }

        WaveCountdown -= Time.deltaTime;
        CountDown_Text.text = "Next Wave: " + WaveCountdown.ToString("0");
    }

    void NoOfEnemy()
    {
        for (int i = 0; i < SpawnNumber; i++)
        {
            EnemySpawn();
        }
    }

    void EnemySpawn()
    {
        Instantiate(Enemy, Spawn_Enemy.position, Spawn_Enemy.rotation);
    }
}
