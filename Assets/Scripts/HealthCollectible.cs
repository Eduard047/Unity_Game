using UnityEngine;
using System.Collections;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private Sprite emptyHeartSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(healthValue);
                
                // Ваша логіка для заміни серця на пусте серце
                ReplaceHeartWithEmpty(playerHealth);

                StartCoroutine(GetHurt(playerHealth));
            }
        }
    }

    private void ReplaceHeartWithEmpty(Health playerHealth)
    {
        if (playerHealth != null)
        {
            // Отримуємо індекс серця, яке потрібно замінити
            int heartIndex = Mathf.CeilToInt(playerHealth.GetCurrentHealth()) - 1;

            // Перевіряємо, чи індекс знаходиться в межах масиву серць
            if (heartIndex >= 0 && heartIndex < playerHealth.GetMaxHealth())
            {
                // Замінюємо спрайт серця на порожній
                playerHealth.GetHeartImage(heartIndex).sprite = emptyHeartSprite;
            }
        }
    }

    private IEnumerator GetHurt(Health playerHealth)
    {
        // Застосовуємо ігнорування колізії
        Physics2D.IgnoreLayerCollision(6, 8, true);

        yield return new WaitForSeconds(3);

        // Зняття ігнорування колізії
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
}
