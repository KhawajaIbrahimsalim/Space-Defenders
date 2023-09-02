using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //[SerializeField] private NavMeshAgent Enemy;
    Transform Player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float ProjectileSpeed;
    [SerializeField] private GameObject EnemyProjectile;
    [SerializeField] private Transform EnemyProjectileSpawnPoint;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private Transform Enemy_Transform;
    [SerializeField] private float Range = 10f;
    [SerializeField] float Speed = 5f;
    public float EnemyHealth = 200f;
    public float TimeBeforeShoot = 1f;
    public float TimeBeforeShoot_2 = 1f; // This value will remain constants

    private GameObject E_SpawnPoint;
    Transform MainTower;
    AudioSource Audio_EnemyProjectile;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        MainTower = GameObject.Find("AllyTower Position").transform;
        Audio_EnemyProjectile = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeBeforeShoot -= Time.deltaTime;
        
        Vector3 Distance = transform.position - MainTower.position;
        
        if (Distance.sqrMagnitude > Range)
        {
            rb.transform.position = Vector3.MoveTowards(rb.transform.position,
                MainTower.position, 
                Speed * Time.deltaTime);
        }
        else if (TimeBeforeShoot <= 0)
        {
            GameObject CurrentProjectile = Instantiate(EnemyProjectile, EnemyProjectileSpawnPoint.position, Enemy_Transform.transform.rotation);
            Audio_EnemyProjectile.Play();
            CurrentProjectile.GetComponent<Rigidbody>().AddForce(EnemyProjectileSpawnPoint.forward * ProjectileSpeed * Time.deltaTime, ForceMode.Impulse);
            TimeBeforeShoot = TimeBeforeShoot_2;
        }
        
        rb.transform.forward = -(rb.transform.position - MainTower.position); // Rotate the enemy towards the Player.
    }
}