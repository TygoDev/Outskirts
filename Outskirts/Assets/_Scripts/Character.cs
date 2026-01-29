using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Stats")]
    public CharacterStats stats;

    [Header("Movement")]
    public Vector2 moveDirection = Vector2.left;

    public float currentHealth;
    protected Rigidbody2D rb;

    // Damage cooldown
    [SerializeField] protected float damageCooldown = 0.5f;
    protected float lastDamageTime = 0f;

    // Knockback
    protected Vector2 knockbackVelocity = Vector2.zero;
    [SerializeField] protected float knockbackDecay = 5f;

    // Track damaged targets per tick
    protected HashSet<Character> damagedThisTick = new HashSet<Character>();

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        Initialize(stats);
    }

    public virtual void Initialize(CharacterStats pStats)
    {
        stats = pStats;
        currentHealth = stats.maxHealth;
    }

    protected virtual void FixedUpdate()
    {
        if (currentHealth <= 0) return;

        damagedThisTick.Clear();

        Vector2 direction = moveDirection.normalized;

        // Slow near x = 0
        float distanceToZero = Mathf.Abs(transform.position.x);
        float stopThreshold = 0.2f;
        if (distanceToZero < stopThreshold)
        {
            direction.x = Mathf.Lerp(direction.x, 0f, 1f - (distanceToZero / stopThreshold));
        }

        Vector2 velocity =
            new Vector2(direction.x * stats.speed, direction.y * stats.speed) +
            knockbackVelocity;

        rb.linearVelocity = velocity;

        knockbackVelocity = Vector2.Lerp(
            knockbackVelocity,
            Vector2.zero,
            knockbackDecay * Time.fixedDeltaTime
        );
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(gameObject.tag)) return;

        Character other = collision.gameObject.GetComponent<Character>();
        if (other == null) return;
        if (Time.time - lastDamageTime < damageCooldown) return;
        if (damagedThisTick.Contains(other)) return;

        // Deal damage
        TakeDamage(other.stats.damage);

        // Knockback
        Vector2 knockDir = (other.transform.position - transform.position).normalized;
        knockbackVelocity -= knockDir * stats.knockbackForce;
        other.knockbackVelocity += knockDir * other.stats.knockbackForce;
        // trigger animation for knockback

        damagedThisTick.Add(other);
        lastDamageTime = Time.time;
    }

    protected virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
