using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainTowerProjectile : MonoBehaviour
{
    public AudioSource DamageSound;
    public BaseScript BaseObject;
    [SerializeField] private GameObject MainTower_Projectile;
    public float Damage = 25f;
    public float PerKillAmount = 5f;

    private GameObject ReactorAmount_Text;
    private GameObject EHB;
    // Start is called before the first frame update
    void Start()
    {
        EHB = GameObject.Find("EnemyHealthBar"); // Will find the gameobject of a type in a scene.
        ReactorAmount_Text = GameObject.Find("Reactor amount_Text");
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(MainTower_Projectile, 1.5f);
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyMovement>().EnemyHealth -= Damage;
            EHB.GetComponent<EnemyHealthBar>().Enemy_Health_Bar(collision.GetComponent<EnemyMovement>().EnemyHealth);

            if (collision.GetComponent<EnemyMovement>().EnemyHealth <= 0)
            {
                DamageSound.Play();
                BaseObject.GetComponent<BaseScript>().ReactorAmount += PerKillAmount;
                ReactorAmount_Text.GetComponent<TextMeshProUGUI>().text = BaseObject.GetComponent<BaseScript>().ReactorAmount.ToString("0"); // Show
                Destroy(collision.gameObject);
            }
        } 
    }
}