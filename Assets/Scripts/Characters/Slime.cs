using UnityEngine;

public class Slime : MonoBehaviour
{
    private HealthController health;
    private AttackController attack;
    private Movement movement;
    private Animator animator;
    private bool isWalking = false;
    private float speed = 0f;

    private void Awake()
    {
        health = GetComponent<HealthController>();
        attack = GetComponent<AttackController>();
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();

        speed = movement.speed;
    }

    private void FixedUpdate()
    {
        movement.speed = isWalking ? speed : 0;
        animator.SetBool("IsWalking", isWalking);

        if (health.isDead || health.isHurting || movement == null)
        {
            isWalking = false;
            return;
        }

        if (attack.isAttacking)
        {
            isWalking = false;
        }
        else
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attack.attackPoint.position, attack.range, attack.whatIsEnemy);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i].tag == "Player")
                {
                    if (!enemiesToDamage[i].GetComponent<HealthController>().isDead)
                        attack.Attack(true);
                }
            }

            isWalking = true;
        }
    }
}
