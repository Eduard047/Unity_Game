using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    private float currentHealth;
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("UI")]
    [SerializeField] private Image[] hearts; // Reference to heart images for UI
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return Mathf.CeilToInt(startingHealth);
    }

    public Image GetHeartImage(int index)
    {
        if (index >= 0 && index < hearts.Length)
        {
            return hearts[index];
        }
        return null;
    }

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if (invulnerable) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            HandleDamage();
        }
        else
        {
            HandleDeath();
        }
    }

    private void HandleDamage()
    {
        anim.SetTrigger("hurt");
        StartCoroutine(Invulnerability());
        UpdateUI();
    }

    private void HandleDeath()
    {
        if (!dead)
        {
            // Deactivate all attached component classes
            foreach (Behaviour component in components)
                component.enabled = false;

            anim.SetBool("grounded", true);
            anim.SetTrigger("die");

            dead = true;
        }
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
        UpdateUI();
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Mathf.CeilToInt(currentHealth))
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    // Respawn
    public void Respawn()
    {
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invulnerability());
        dead = false;

        // Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;

        UpdateUI();
    }
}
