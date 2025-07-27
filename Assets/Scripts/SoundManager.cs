using UnityEngine;

public enum SoundType
{
    PLAYERDEATH,
    HURT,
    BOWSHOT,
    BOWCHARGE,
    JUMP,
    WIN,
    ECOOLISHOOT,
    ECOOLIDEATH,
    TANKSHOOT,
    REDBLOODDEATH,
    BACTERIAPASSIVE,
    BACTERIADEATH,
    TANKPASSIVE,
    TANKDEATH,
    PARASITEPASSIVE,
    PARASITEDEATH,
    BOWFULL,
    ECOOLIIDLE,
    ECOOLIGLASSES
}
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
}
