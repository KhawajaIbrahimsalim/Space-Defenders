using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI ScoreTime;
    public TextMeshProUGUI HighestSurvivalTime;
    public PlayerMovement Player;
    public ProjectileMovement Projectile;
    public Text Player_UpDamageCost;
    public Text AllyTower_HealingCost;
    public Text AllyTower_MaxHealthCost;
    public AllyTowerMechanics AllyTower;
    public MainTowerProjectile AllyTower_Projectile;
    public Text AllyTower_UpDamageCost;
    public float PerKillReactorAmount = 7f;
    public float CostMultiply;
    public ProjectileSpawnPoint Projectile_SpawnPoint;
    public Text Player_UpFireRateCost;
    public EnemyProjectile EnemyProjectile;
    public EnemyMovement EnemyMovement;

    float StartTime;
    int DummyTimer = 30;
    int minute;
    int second;
    int HighestSurvivalTime_M = 0;
    int HighestSurvivalTime_S = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        ScoreTime.color = Color.yellow;
        HighestSurvivalTime.color = Color.red;
        HighestSurvivalTime_M = PlayerPrefs.GetInt("HighScore_M", 0);
        HighestSurvivalTime_S = PlayerPrefs.GetInt("HighScore_S", 0);
        //HighestSurvivalTime.text = "Highest Survival Time: " + HighestSurvivalTime_M.ToString() + ":" + HighestSurvivalTime_S.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - StartTime;

        minute = ((int)t / 60);
        second = ((int)t % 60);

        if (second / 10 == 0)
        {
            ScoreTime.text = minute.ToString() + ":0" + second.ToString("0");
        }

        if (second / 10 != 0)
        {
            ScoreTime.text = minute.ToString() + ":" + second.ToString("0");
        }


        if (second == 30)
        {
            if (DummyTimer == 30)
            {
                // Player
                // 0. Defualt
                Projectile.PerKillAmount += PerKillReactorAmount;
                // 1. Damage
                Player.Player_UpDamage_Cost *= CostMultiply;
                Player_UpDamageCost.text = "x" + Player.Player_UpDamage_Cost.ToString("0"); // To show cost on HUD
                // 2. Fire rate
                Projectile_SpawnPoint.Player_FireRate_Cost *= CostMultiply;
                Player_UpFireRateCost.text = "x" + Projectile_SpawnPoint.Player_FireRate_Cost.ToString("0");
                // DummyTimer
                DummyTimer = 0;

                // AllyTower
                // 0. Defualt
                AllyTower_Projectile.PerKillAmount += PerKillReactorAmount;
                // 1. Damage
                AllyTower.AllyTower_UpDamage_Cost *= CostMultiply;
                AllyTower_UpDamageCost.text = "x" + AllyTower.AllyTower_UpDamage_Cost.ToString("0");
                // 2. Healing
                AllyTower.AllyTower_Healing_Cost *= CostMultiply;
                AllyTower_HealingCost.text = "x" + AllyTower.AllyTower_Healing_Cost.ToString("0");
                // 3. Max Health
                AllyTower.AllyTower_MaxHealth_Cost *= CostMultiply;
                AllyTower_MaxHealthCost.text = "x" + AllyTower.AllyTower_MaxHealth_Cost.ToString("0");

                // Enemy
                // 1. Damage
                EnemyProjectile.GetComponent<EnemyProjectile>().Damage += 1f;
                EnemyMovement.GetComponent<EnemyMovement>().EnemyHealth += 1.5f;
            }
        }

        if (second == 0)
        {
            if (DummyTimer == 0)
            {
                // Player
                // 0. Defualt
                Projectile.PerKillAmount += PerKillReactorAmount;
                // 1. Damage
                Player.Player_UpDamage_Cost *= CostMultiply;
                Player_UpDamageCost.text = "x" + Player.Player_UpDamage_Cost.ToString("0"); // To show cost on HUD
                // 2. Fire rate
                Projectile_SpawnPoint.Player_FireRate_Cost *= CostMultiply;
                Player_UpFireRateCost.text = "x" + Projectile_SpawnPoint.Player_FireRate_Cost.ToString("0");
                // DummyTimer
                DummyTimer = 30;

                // AllyTower
                // 0. Defualt
                AllyTower_Projectile.PerKillAmount += PerKillReactorAmount;
                // 1. Damage
                AllyTower.AllyTower_UpDamage_Cost *= CostMultiply;
                AllyTower_UpDamageCost.text = "x" + AllyTower.AllyTower_UpDamage_Cost.ToString("0");
                // 2. Healing
                AllyTower.AllyTower_Healing_Cost *= CostMultiply;
                AllyTower_HealingCost.text = "x" + AllyTower.AllyTower_Healing_Cost.ToString("0");
                // 3. Max Health
                AllyTower.AllyTower_MaxHealth_Cost *= CostMultiply;
                AllyTower_MaxHealthCost.text = "x" + AllyTower.AllyTower_MaxHealth_Cost.ToString("0");

                // Enemy
                // 1. Damage
                EnemyProjectile.GetComponent<EnemyProjectile>().Damage += 1f;
                EnemyMovement.GetComponent<EnemyMovement>().EnemyHealth += 1.5f;
            }
        }

        if (minute > HighestSurvivalTime_M)
        {
            HighestSurvivalTime_M = minute;
            PlayerPrefs.SetInt("HighScore_M", HighestSurvivalTime_M);
        }

        if (second > HighestSurvivalTime_S || second == 0f)
        {
            if (minute >= HighestSurvivalTime_M)
            {
                HighestSurvivalTime_S = second;
                PlayerPrefs.SetInt("HighScore_S", HighestSurvivalTime_S);
            }
        }

        if (HighestSurvivalTime_S / 10 == 0)
        {
            HighestSurvivalTime.text = "Highest Time: " + HighestSurvivalTime_M.ToString("0") + ":0" + HighestSurvivalTime_S.ToString("0");
        }

        if (HighestSurvivalTime_S / 10 != 0)
        {
            HighestSurvivalTime.text = "Highest Time: " + HighestSurvivalTime_M.ToString("0") + ":" + HighestSurvivalTime_S.ToString("0");
        }
    }
}
