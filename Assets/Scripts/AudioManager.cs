using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Статичний екземпляр для доступу з інших скриптів

    private AudioSource audioSource;

    [Header("Sounds")]
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip playerDeathSound;
    [SerializeField] private AudioClip enemyDeathSound;
    [SerializeField] private AudioClip gameStartSound;
    

    private void Awake()
    {
        //Реалізація Singleton для AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Зберігаємо AudioManager при зміні сцени
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void PlayAttackSound()
    {
        float volume = 0.4f;
        PlaySound(attackSound, volume);
    }

    public void PlayPlayerDeathSound()
    {
        float volume = 0.4f;
        PlaySound(playerDeathSound, volume);
    }

    public void PlayEnemyDeathSound()
    {
        float volume = 0.4f;
        PlaySound(enemyDeathSound, volume);
    }

    public void PlayGameStartSound()
    {
        float volume = 0.6f;
        PlaySound(gameStartSound, volume);
    }

    private void PlaySound(AudioClip audioClip, float volume)
    {
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
    }
}
