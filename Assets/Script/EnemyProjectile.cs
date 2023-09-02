using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public BaseScript BaseObject;
    [SerializeField] public float Damage = 10;
    public GameObject Enemy_Projectile;
    public ProjectileMovement Projectile;
    public EnemyMovement enemyMovement;

    private GameObject projectileSpawnPoint;
    private GameObject AllyTower;
    // Update is called once per frame

    void Update()
    {
        Destroy(Enemy_Projectile, 3);
        projectileSpawnPoint = GameObject.Find("Player Projectile Spawn Point");
        AllyTower = GameObject.Find("Ally Tower");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Ally Tower")
        {
            collision.GetComponent<AllyTowerMechanics>().AllyTowerHealth -= Damage;
            collision.GetComponent<AllyTowerMechanics>().Health_Bar(collision.GetComponent<AllyTowerMechanics>().AllyTowerHealth);

            if (collision.GetComponent<AllyTowerMechanics>().AllyTowerHealth <= 0)
            {
                Destroy(collision.gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }
}