using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    public int vitality = 100;
    public int health;

    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool isHurting = false;

    private Animator animator;

    public delegate void TakeHealth(int amount);
    public TakeHealth HealthEvent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = vitality;
    }

    public void TakeDamage(int amount)
    {
        if (isDead || health <= 0 || amount <= 0) return;

        health = Mathf.Max(health - amount, 0);
        
        if (health == 0)
        {
            isDead = true;
            if (gameObject.CompareTag("Player"))
            {
                SoundManager.Instance.PlayLoseSound();
                StartCoroutine(RestartLevelAfterDelay(3f));
            }
            Death();
        }
        else
        {
            if (gameObject.CompareTag("Player"))
                SoundManager.Instance.PlayHurtSound();
            
            Hurt(true);
        }

        LaunchHealthEvent();
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || health >= vitality) return;
        
        health = Mathf.Min(health + amount, vitality);
        LaunchHealthEvent();
    }

    public void Hurt(bool isHurting)
    {
        this.isHurting = isHurting;
        if (isHurting) animator.Play("Hurt");
        animator.SetBool("IsHurting", this.isHurting);
    }

    public void Death()
    {
        isDead = true;
        animator.SetBool("IsDead", isDead);
        GetComponent<Collider2D>().enabled = false;
    }

    private void LaunchHealthEvent()
    {
        HealthEvent?.Invoke(health);
    }

    public void StopHurt() => Hurt(false);
    
    private IEnumerator RestartLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
