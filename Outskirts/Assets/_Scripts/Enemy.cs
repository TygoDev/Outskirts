using UnityEngine;

public class Enemy : Character
{
    bool playerDied = false;

    protected override void Die()
    {
        ChooseLoot();
        base.Die();
        Destroy(gameObject);
    }

    protected override void FixedUpdate()
    {
        if (playerDied)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        base.FixedUpdate();
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (collision.gameObject.GetComponent<Character>().currentHealth - stats.damage <= 0)
            {
                playerDied = true;
            }

            base.OnCollisionStay2D(collision);
        }
    }

    private void ChooseLoot()
    {
        foreach (var item in stats.lootTable)
        {
            float roll = Random.Range(0f, 100f);
            if (roll <= item.dropChance)
            {
                CombatManager.Instance.AddLoot(item);
            }
        }

        CombatManager.Instance.AddLoot(
            null,
            Random.Range(stats.moneyDropValue / 2, stats.moneyDropValue * 2)
        );
    }
}
