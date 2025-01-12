using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] int damage = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponent<HealthController>().TakeDamage(damage);
    }
}
