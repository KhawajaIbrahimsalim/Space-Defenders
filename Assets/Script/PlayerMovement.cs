using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public BaseScript BaseObject;
    public float Player_UpDamage_Cost = 100f;
    public AudioSource UpgradeSound;
    public Button PlayerUpgradeDamage_Button;
    public GameObject Projectile;
    public Rigidbody rb;
    public Camera cam;
    public float Speed = 30f;
    public float MouseSensitivity = 2f;
    public CharacterController Controller;
    public int PlayerHealth = 100;
    public Button P_Damage_InsufficientReactors_Button;
    public Canvas PlayerUpgradePanel;
    public TextMeshProUGUI ReactorAmount_Text;
    public bool CursorONnOFF = true;
    public bool CursorBool = true;
    public Image UpgradeSignalArrow;
    public MainTowerProjectile AllyTower_Projectile;
    public AllyTowerMechanics AllyTower;
    public ProjectileSpawnPoint projectileSpawnPoint;

    float HorizontalDirection;
    float VerticalDirection;
    bool IsFullyUpgraded;
    Vector2 Rotate;
    // Start is called before the first frame update
    void Start()
    {
        Projectile.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;

        BaseObject.ReactorAmount = 0f;

        // Player-Reset values
        Projectile.GetComponent<ProjectileMovement>().PerKillAmount = 7f;
        Projectile.GetComponent<ProjectileMovement>().Damage = 25f;
        projectileSpawnPoint.GetComponent<ProjectileSpawnPoint>().FireRate_2 = 1f;

        // AllyTower-Reset values
        AllyTower_Projectile.PerKillAmount = 7f;
        AllyTower_Projectile.Damage = 25f;
        AllyTower.GetComponent<AllyTowerMechanics>().MaxHealth = 800f;
        AllyTower.GetComponent<AllyTowerMechanics>().AllyTowerHealth = 800f;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        HorizontalDirection = Input.GetAxis("Horizontal");
        VerticalDirection = Input.GetAxis("Vertical");

        Rotate.x += Input.GetAxis("Mouse X") * MouseSensitivity;
        Rotate.y += Input.GetAxis("Mouse Y") * MouseSensitivity;

        Movement(HorizontalDirection, VerticalDirection);
        MouseRotate(Rotate);
        cursorEnable();

        // Damage
        if (BaseObject.ReactorAmount < Player_UpDamage_Cost)
        {
            P_Damage_InsufficientReactors_Button.gameObject.SetActive(true);
            PlayerUpgradeDamage_Button.gameObject.SetActive(false);
        }

        if (BaseObject.ReactorAmount >= Player_UpDamage_Cost)
        {
            PlayerUpgradeDamage_Button.gameObject.SetActive(true);
            P_Damage_InsufficientReactors_Button.gameObject.SetActive(false);
        }
    }
    
    void cursorEnable()
    {
        if (CursorONnOFF == true)
        {
            if (Input.GetKeyDown("left shift") && CursorBool == true)
            {
                Cursor.lockState = CursorLockMode.Confined;
                CursorBool = false;
            }

            else if (Input.GetKeyDown("left shift") && CursorBool == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                CursorBool = true;
            }
        }
    }

    void Movement(float x, float z)
    {
        Vector3 move = transform.right * x + transform.forward * z;

        Controller.Move(move * Speed * Time.deltaTime); // approved(_/)
    }

    void MouseRotate(Vector2 Rotate)
    {
        Rotate.y = Mathf.Clamp(Rotate.y, -90, 90);
        cam.transform.localRotation = Quaternion.Euler(-Rotate.y, Rotate.x, 0f);
    }

    public void UpgradePlayerDamage()
    {
        if (BaseObject.ReactorAmount >= Player_UpDamage_Cost)
        {
            UpgradeSound.Play();
            BaseObject.ReactorAmount -= Player_UpDamage_Cost;
            ReactorAmount_Text.text = BaseObject.ReactorAmount.ToString("0"); // Remaining reactors _Show
            Projectile.GetComponent<ProjectileMovement>().Damage += 5;
        }
    }
}