using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStats stats;

    [Header("Movement Settings")]
    [Tooltip("Direction the character moves in if no target is set.")]
    public Vector2 moveDirection = Vector2.left; // default left

    public float currentHealth;
    private Rigidbody2D rb;

    // Damage cooldown
    [SerializeField] private float damageCooldown = 0.5f;
    private float lastDamageTime = 0f;

    // Knockback
    private Vector2 knockbackVelocity = Vector2.zero;
    [SerializeField] private float knockbackDecay = 5f; // how fast knockback fades

    // Track damaged targets per tick
    private HashSet<Character> damagedThisTick = new HashSet<Character>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // ensure no gravity
        Initialize(stats);
    }

    public void Initialize(CharacterStats pStats)
    {
        stats = pStats;
        currentHealth = stats.maxHealth;
    }

    private void FixedUpdate()
    {
        if (currentHealth <= 0) return;

        damagedThisTick.Clear(); // reset every physics frame

        Vector2 direction = moveDirection.normalized;

        // Smoothly reduce horizontal movement near x = 0
        float distanceToZero = Mathf.Abs(transform.position.x);
        float stopThreshold = 0.2f; // start slowing down when within 0.5 units
        if (distanceToZero < stopThreshold)
        {
            // Lerp horizontal input toward 0
            direction.x = Mathf.Lerp(direction.x, 0f, 1f - (distanceToZero / stopThreshold));
        }

        // Movement + knockback combined
        Vector2 velocity = new Vector2(direction.x * stats.speed, direction.y * stats.speed) + knockbackVelocity;
        rb.linearVelocity = velocity;

        // Gradually reduce knockback
        knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.zero, knockbackDecay * Time.fixedDeltaTime);
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(gameObject.tag))
        {
            return;
        }    
 
        Character other = collision.gameObject.GetComponent<Character>();
        if (other == null) return;
        if (Time.time - lastDamageTime < damageCooldown) return;
        if (damagedThisTick.Contains(other)) return; // already damaged this tick

        // Deal damage
        TakeDamage(other.stats.damage);

        // Knockback
        Vector2 knockDir = (other.transform.position - transform.position).normalized;
        knockbackVelocity -= knockDir * stats.knockbackForce;
        other.knockbackVelocity += knockDir * other.stats.knockbackForce;

        damagedThisTick.Add(other);
        lastDamageTime = Time.time;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        rb.linearVelocity = Vector2.zero;
        Destroy(gameObject);

        if (stats.characterName == "Player")
            CombatManager.Instance.PlayerDied();
        else
            ChooseLoot();
    }

    private void ChooseLoot()
    {
        foreach (var item in stats.lootTable)
        {
            float roll = UnityEngine.Random.Range(0f, 100f);

            if(roll <= item.dropChance)
                CombatManager.Instance.AddLoot(item);
        }

        CombatManager.Instance.AddLoot(null,UnityEngine.Random.Range(stats.moneyDropValue/2,stats.moneyDropValue*2));
    }
}
