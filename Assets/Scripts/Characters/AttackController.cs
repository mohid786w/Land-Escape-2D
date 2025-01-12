using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int damage = 10;
    public float speed = 1f;
    public float range = 1f; 
    public Transform attackPoint;
    public LayerMask whatIsEnemy;

    [HideInInspector] public bool isAttacking = false;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack(bool a)
    {
        isAttacking = a;
        animator.SetFloat("AttackSpeed", speed);
        animator.SetBool("IsAttacking", isAttacking);
    }

    public void Hit()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, range, whatIsEnemy);

        HealthController lastEnemy = null;

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            HealthController enemyHealth = enemiesToDamage[i].GetComponent<HealthController>();

            if (lastEnemy != null)
            {
                if (enemyHealth == lastEnemy)
                    continue;
            }

            if (enemyHealth != null)
                enemyHealth.TakeDamage(damage);

            lastEnemy = enemyHealth;
        }
    }

    public void StopAttack() => Attack(false);

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
