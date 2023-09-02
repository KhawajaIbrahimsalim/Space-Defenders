using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image EnemyHealth_Bar;
    [SerializeField] private float EnemyMaxHealth = 200f;

    public void Enemy_Health_Bar(float Enemy_Health)
    {
        EnemyHealth_Bar.fillAmount = Enemy_Health / EnemyMaxHealth;
    }
}
