using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public PlayerMovement Player;
    public GameObject Projectile;
    public ProjectileSpawnPoint projectileSpawnPoint;
    public Canvas PlayerUpgradePanel;
    public Canvas AllyTowerUpgradePanel;
    public Canvas PlayerAndTowerButton;
    public Button UpgradeButton;
    public Button CloseButton;

    public void ShowUpgrades()
    {
        PlayerUpgradePanel.gameObject.SetActive(true); // appear
        CloseButton.gameObject.SetActive(true); // appear
        UpgradeButton.gameObject.SetActive(false); // disappear
        PlayerAndTowerButton.gameObject.SetActive(true); // appear
        Projectile.SetActive(false);
        projectileSpawnPoint.IsFiring = false;
        Cursor.lockState = CursorLockMode.Confined;
        Player.CursorONnOFF = false;
    }

    public void CloseUpgrades()
    {
        PlayerUpgradePanel.gameObject.SetActive(false); // disappear
        AllyTowerUpgradePanel.gameObject.SetActive(false); // disappear
        CloseButton.gameObject.SetActive(false); // disappear
        UpgradeButton.gameObject.SetActive(true); // appear
        PlayerAndTowerButton.gameObject.SetActive(false); // disappear
        Projectile.SetActive(true);
        projectileSpawnPoint.IsFiring = true;
        Cursor.lockState = CursorLockMode.Locked;
        Player.CursorONnOFF = true;
        Player.CursorBool = true;
    }

    public void ShowPlayerUpgrade()
    {
        AllyTowerUpgradePanel.gameObject.SetActive(false);
        PlayerUpgradePanel.gameObject.SetActive(true);
    }

    public void ShowAllyTowerUpgrade()
    {
        PlayerUpgradePanel.gameObject.SetActive(false);
        AllyTowerUpgradePanel.gameObject.SetActive(true);
    }
}
