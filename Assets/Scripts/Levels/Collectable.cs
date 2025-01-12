using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] int coin_value = 1;
    [SerializeField] int health_value = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (CompareTag("Coin"))
                LevelManager.Instance.AddCoins(coin_value);
            else if (CompareTag("Key"))
                LevelManager.Instance.AddKey();
            else if (CompareTag("Health"))
            {
                HealthController playerHealth = collision.GetComponent<HealthController>();
                if (playerHealth != null)
                    playerHealth.Heal(health_value);
            }
            else if (CompareTag("Jumper"))
            {
                Adventurer adventurer = collision.GetComponent<Adventurer>();
                if (adventurer != null)
                    adventurer.EnableJumperPowerUp();
            }
            Destroy(gameObject);
        }
    }
}
