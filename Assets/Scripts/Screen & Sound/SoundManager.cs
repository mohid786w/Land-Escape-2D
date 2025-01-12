using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource effectsSource;

    [Header("Sound Clips")]
    public AudioClip attackSound;
    public AudioClip loseSound;
    public AudioClip jumpSound;
    public AudioClip levelCompleteSound;
    public AudioClip hurtSound;
    public AudioClip gameCompleteSound;
    public AudioClip coinCollectSound;
    public AudioClip doorUnlockSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip)
            effectsSource.PlayOneShot(clip);
    }

    public void PlayAttackSound() => PlaySound(attackSound);
    public void PlayLoseSound() => PlaySound(loseSound);
    public void PlayJumpSound() => PlaySound(jumpSound);
    public void PlayLevelCompleteSound() => PlaySound(levelCompleteSound);
    public void PlayHurtSound() => PlaySound(hurtSound);
    public void PlayGameCompleteSound() => PlaySound(gameCompleteSound);
    public void PlayCoinCollectSound() => PlaySound(coinCollectSound);
    public void PlayDoorUnlockSound() => PlaySound(doorUnlockSound);
}