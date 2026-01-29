using UnityEngine;

public class Player : Character
{
    private Animator animator;
    private bool isAttacking;
    private bool isDead;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void TakeDamage(float amount)
    {
        if (isDead) return;

        base.TakeDamage(amount);

        if (!isAttacking)
        {
            animator.SetTrigger("Attack");
            isAttacking = true;
        }
    }

    protected override void FixedUpdate()
    {
        if (isDead) return;

        base.FixedUpdate();
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
    }

    protected override void Die()
    {
        if (isDead) return;
        isDead = true;

        base.Die(); // stops movement

        animator.ResetTrigger("Attack");
        animator.SetTrigger("Die");

        // Optional: disable collider so nothing else hits you
        GetComponent<Collider2D>().enabled = false;
    }

    // Animation Event at END of death animation
    public void OnDeathAnimationFinished()
    {
        CombatManager.Instance.PlayerDied();
        Destroy(gameObject);
    }

    // Attack animation event
    public void OnAttackAnimationFinished()
    {
        isAttacking = false;
    }
}
