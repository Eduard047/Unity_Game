using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth = 3.0f;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("Invulnerability")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;

    private Vector3 initialPosition; // Store the initial position for respawn

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;


                dead = true;
                StartCoroutine(RespawnCoroutine());

                AudioManager.instance.PlayEnemyDeathSound();
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }


    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        AudioManager.instance.PlayPlayerDeathSound();
        yield return new WaitForSeconds(0.5f);
        currentHealth = startingHealth;
        transform.position = initialPosition;

        if (GetComponent<PlayerController>() != null) {
            GetComponent<PlayerController>().enabled = true;
            dead = false;
        }

    // float timer = 0f;
    // while (timer < 2f)
    // {
    //     timer += Time.deltaTime;
    //     yield return null;
    // }

    // currentHealth = startingHealth;
    // transform.position = initialPosition;

    // if (GetComponent<PlayerController>() != null) 
    // {
    //     GetComponent<PlayerController>().enabled = true;
    //     dead = false;
    // }
    }
}