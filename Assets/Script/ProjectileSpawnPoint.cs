using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectileSpawnPoint : MonoBehaviour
{
    public BaseScript BaseObject;
    public GameObject Projectile;
    public Camera fpsCam;
    public Transform ProjectileSpawn_point;
    public GameObject Player;
    public float Projectile_Speed;
    public float Spread;
    public float FireRate = 1f;
    public float Player_FireRate_Cost = 500f;
    public AudioSource UpgradeSound;
    public Button PlayerUpgradeFireRate_Button;
    public Button FireRate_InsufficientReactors_Button;
    public bool IsFiring = true;
    public float FireRate_2 = 1f;
    public Button P_FireRate_Button_NoUpgradeAvailable;
    public TextMeshProUGUI ReactorAmount_Text;
    public Image UpgradeSignalArrow;

    AudioSource Audio_Projectile;

    // Start is called before the first frame update
    void Start()
    {
        Audio_Projectile = GetComponent<AudioSource>();
        //ReactorTotalAmount = GameObject.Find("Reactor amount_Text");
    }

    // Update is called once per frame
    void Update()
    {
        ButtonChangeWithAmount();
        NoUpgradeAvailable();
        SpawnProjectile();
    }

    private void SpawnProjectile()
    {
        if (Input.GetKey("mouse 0") && IsFiring == true)
        {
            if (FireRate <= 0)
            {
                float x = Random.Range(-Spread, Spread);
                float y = Random.Range(-Spread, Spread);

                GameObject CurrentProjectile =  Instantiate(Projectile, ProjectileSpawn_point.position, fpsCam.transform.rotation);

                Audio_Projectile.Play();

                CurrentProjectile.GetComponent<Rigidbody>().AddForce(new Vector3(x, y, 0f) + fpsCam.transform.forward * Projectile_Speed * Time.deltaTime, ForceMode.Impulse);

                FireRate = FireRate_2;
            }

            FireRate -= Time.deltaTime;
        }     
    }

    private void NoUpgradeAvailable()
    {
        if (FireRate_2 <= 0.3f)
        {
            P_FireRate_Button_NoUpgradeAvailable.gameObject.SetActive(true);

            PlayerUpgradeFireRate_Button.gameObject.SetActive(false);
            FireRate_InsufficientReactors_Button.gameObject.SetActive(false);
        }
    }

    private void ButtonChangeWithAmount()
    {
        // Fire Rate
        if (BaseObject.GetComponent<BaseScript>().ReactorAmount < Player_FireRate_Cost && FireRate_2 > 0.3f)
        {
            FireRate_InsufficientReactors_Button.gameObject.SetActive(true);
            PlayerUpgradeFireRate_Button.gameObject.SetActive(false);
        }

        if (BaseObject.GetComponent<BaseScript>().ReactorAmount >= Player_FireRate_Cost && FireRate_2 > 0.3f)
        {
            PlayerUpgradeFireRate_Button.gameObject.SetActive(true);
            FireRate_InsufficientReactors_Button.gameObject.SetActive(false);
        }
    }

    public void UpgradeFireRate()
    {
        if (BaseObject.ReactorAmount >= Player_FireRate_Cost && FireRate_2 > 0.3f)
        {
            UpgradeSound.Play();
            BaseObject.ReactorAmount -= Player_FireRate_Cost;
            ReactorAmount_Text.text = BaseObject.ReactorAmount.ToString("0"); // Remaining reactors _Show
            FireRate_2 -= 0.1f;
        }
    }
} // If Player wants to change to First Person or Third Person
            //Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            //RaycastHit Hit;

            //Vector3 TragetPoint;

            //if (Physics.Raycast(ray, out Hit))
            //{
            //    TragetPoint = Hit.point;
            //}
            //else
            //{
            //    TragetPoint = ray.GetPoint(75);
            //}

            //Vector3 DirectionWithoutSpread = TragetPoint - ProjectileSpawn_point.position;

            //float x = Random.Range(-Spread, Spread);
            //float y = Random.Range(-Spread, Spread);

            //Vector3 DirectionWithSpread = DirectionWithoutSpread + new Vector3(x, y, 0f);

            //CurrentProjectile.transform.forward = DirectionWithSpread.normalized; // Rotate the Projectile to Shoot direction