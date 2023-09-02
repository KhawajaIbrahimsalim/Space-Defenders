using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AllyTowerMechanics : MonoBehaviour
{
    public BaseScript BaseObject;
    public AudioSource UpgradeSound;
    public float AllyTower_UpDamage_Cost = 100f;
    public float AllyTower_Healing_Cost = 25f;
    public float AllyTower_MaxHealth_Cost = 200f;
    public Button AllyTowerUpgradeDamage_Button;
    public Button AllyTowerHealing_Button;
    public Button AllyTowerUpMaxHealth_Button;
    public Image HealthBar;
    public Transform AllyTowerProjectileSpawnPoint;
    public Transform AllyTowerProjectileSpawnPoint_2;
    public GameObject MainTowerProjectile;
    public float ProjectileSpeed;
    public Rigidbody rb;
    public float TimeBeforeShoot = 1f;
    public float TimeBeforeShoot_2 = 1f; // This value will remain constants
    public float AllyTowerHealth = 1000f;
    public float MaxHealth = 1000f;
    public Button A_Damage_InsufficientReactors_Button;
    public Button A_Healing_InsufficientReactors_Button;
    public Button A_MaxHealth_InsufficientReactors_Button;
    public AudioSource Audio_AllyTower;
    public Canvas AllyTowerUpgradePanel;
    public Button A_Healing_Button_ShipHealthIsFull;
    public TextMeshProUGUI ReactorAmount_Text;
    public Image UpgradeSignalArrow;
    public EnemyProjectile EnemyProjectile;
    public EnemyMovement EnemyMovement;

    bool IsDamage_UpgradeAvailable = false;

    //int EnemyCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Audio_AllyTower = GetComponent<AudioSource>();
        ReactorAmount_Text.text = BaseObject.GetComponent<BaseScript>().ReactorAmount.ToString("0");

        // Enemy-Reset values
        EnemyMovement.EnemyHealth = 50f;
        EnemyProjectile.Damage = 10f;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        GameObject closests = FindNearst();
        TimeBeforeShoot -= Time.deltaTime;

        // Damage
        if (IsDamage_UpgradeAvailable == true)
        {
            if (BaseObject.ReactorAmount < AllyTower_UpDamage_Cost)
            {
                UpgradeSignalArrow.gameObject.SetActive(false);
            }

            if (BaseObject.ReactorAmount >= AllyTower_UpDamage_Cost)
            {
                UpgradeSignalArrow.gameObject.SetActive(true);
            }
        }

        if (BaseObject.ReactorAmount < AllyTower_UpDamage_Cost)
        {
            A_Damage_InsufficientReactors_Button.gameObject.SetActive(true);
            AllyTowerUpgradeDamage_Button.gameObject.SetActive(false);
        }

        if (BaseObject.ReactorAmount >= AllyTower_UpDamage_Cost)
        {
            AllyTowerUpgradeDamage_Button.gameObject.SetActive(true);
            A_Damage_InsufficientReactors_Button.gameObject.SetActive(false);
        }

        // Healing
        if (BaseObject.ReactorAmount < AllyTower_Healing_Cost)
        {
            if (IsDamage_UpgradeAvailable == false)
            {
                UpgradeSignalArrow.gameObject.SetActive(false);
            }
            A_Healing_InsufficientReactors_Button.gameObject.SetActive(true);
            AllyTowerHealing_Button.gameObject.SetActive(false);
        }

        if (BaseObject.ReactorAmount >= AllyTower_Healing_Cost)
        {
            if (IsDamage_UpgradeAvailable == false)
            {
                UpgradeSignalArrow.gameObject.SetActive(true);
            }
            AllyTowerHealing_Button.gameObject.SetActive(true);
            A_Healing_InsufficientReactors_Button.gameObject.SetActive(false);
        }

        // Healing: If Ship Health Is Full_Button
        if (AllyTowerHealth == MaxHealth)
        {
            IsDamage_UpgradeAvailable = true;

            A_Healing_Button_ShipHealthIsFull.gameObject.SetActive(true);

            A_Healing_InsufficientReactors_Button.gameObject.SetActive(false);
            AllyTowerHealing_Button.gameObject.SetActive(false);
        }

        if (AllyTowerHealth < MaxHealth)
        {
            IsDamage_UpgradeAvailable = false;

            A_Healing_Button_ShipHealthIsFull.gameObject.SetActive(false);

            AllyTowerHealing_Button.gameObject.SetActive(true);
        }

        // MaxHealth
        if (BaseObject.ReactorAmount < AllyTower_MaxHealth_Cost)
        {
            A_MaxHealth_InsufficientReactors_Button.gameObject.SetActive(true);
            AllyTowerUpMaxHealth_Button.gameObject.SetActive(false);
        }

        if (BaseObject.ReactorAmount >= AllyTower_MaxHealth_Cost)
        {
            AllyTowerUpMaxHealth_Button.gameObject.SetActive(true);
            A_MaxHealth_InsufficientReactors_Button.gameObject.SetActive(false);
        }

        // Projectile Spawn
        if (TimeBeforeShoot <= 0 && closests != null)
        {
            Audio_AllyTower.Play();

            // AllyTowerProjectileSpawnPoint
            GameObject CurrentProjectile = Instantiate(MainTowerProjectile, AllyTowerProjectileSpawnPoint.position, rb.transform.rotation);
            CurrentProjectile.GetComponent<Rigidbody>().AddForce(AllyTowerProjectileSpawnPoint.forward * ProjectileSpeed * Time.deltaTime, ForceMode.Impulse);

            // AllyTowerProjectileSpawnPoint_2
            GameObject CurrentProjectile_2 = Instantiate(MainTowerProjectile, AllyTowerProjectileSpawnPoint_2.position, rb.transform.rotation);
            CurrentProjectile_2.GetComponent<Rigidbody>().AddForce(AllyTowerProjectileSpawnPoint_2.forward * ProjectileSpeed * Time.deltaTime, ForceMode.Impulse);

            TimeBeforeShoot = TimeBeforeShoot_2;
        }

        if (closests != null)
        {
            transform.forward = -(transform.position - closests.transform.position);
        }
    }

    GameObject FindNearst()
    {
        GameObject[] EnemyList;
        EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closests = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position; // the Main Tower "position"

        foreach (GameObject go in EnemyList)
        {
            Vector3 diff = go.transform.position - position;
            float CurDistance = diff.sqrMagnitude;

            if (CurDistance < distance)
            {
                closests = go;
                distance = CurDistance;
            }
        }

        return closests;
    }

    public void Health_Bar(float AllyTowerHealth)
    {
        HealthBar.fillAmount = AllyTowerHealth / MaxHealth;
    }

    public void UpgradeTowerDamage()
    {
        if (BaseObject.ReactorAmount >= AllyTower_UpDamage_Cost)
        {
            UpgradeSound.Play();
            BaseObject.GetComponent<BaseScript>().ReactorAmount -= AllyTower_UpDamage_Cost;
            ReactorAmount_Text.text = BaseObject.ReactorAmount.ToString("0"); // Remaining reactors _Show
            MainTowerProjectile.GetComponent<MainTowerProjectile>().Damage += 5;
        }
    }

    public void Healing()
    {
        if (AllyTowerHealth < MaxHealth)
        {
            UpgradeSound.Play();
            BaseObject.GetComponent<BaseScript>().ReactorAmount -= AllyTower_Healing_Cost;
            ReactorAmount_Text.text = BaseObject.GetComponent<BaseScript>().ReactorAmount.ToString("0"); // Remaining reactors _Show

            if (MaxHealth - AllyTowerHealth < 100f)
            {
                float Heal = AllyTowerHealth - MaxHealth;
                AllyTowerHealth += Heal;
                Health_Bar(AllyTowerHealth);
            }
            else
            {
                AllyTowerHealth += 100f;
                Health_Bar(AllyTowerHealth);
            }
        }
    }

    public void MaximumHealth()
    {
        UpgradeSound.Play();

        if (AllyTowerHealth == MaxHealth)
        {
            BaseObject.GetComponent<BaseScript>().ReactorAmount -= AllyTower_MaxHealth_Cost;
            ReactorAmount_Text.text = BaseObject.GetComponent<BaseScript>().ReactorAmount.ToString("0"); // Remaining reactors _Show
            MaxHealth += 25f;
            AllyTowerHealth += 25f;
        }

        if (AllyTowerHealth != MaxHealth)
        {
            BaseObject.GetComponent<BaseScript>().ReactorAmount -= AllyTower_MaxHealth_Cost;
            ReactorAmount_Text.text = BaseObject.GetComponent<BaseScript>().ReactorAmount.ToString("0"); // Remaining reactors _Show
            MaxHealth += 25f;
        }
    }
}