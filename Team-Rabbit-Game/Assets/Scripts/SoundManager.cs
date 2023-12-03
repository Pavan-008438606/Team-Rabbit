using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public enum SoundType
    {
        Background, TryAgain, Carrot, MonsterDeath, Win
    }

    [SerializeField] private AudioClip background, tryAgain, carrot, monsterDeath, win;
    [SerializeField] private AudioSource backgroundAS;
    [SerializeField] private AudioSource audioSource;




    public bool isMusicOn { get; set; } = true;
    public bool isSoundOn { get; set; } = true;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayBackGroundSound(isSoundOn);
    }


    public void PlayBackGroundSound(bool isOn)
    {
        if (isOn)
        {
            backgroundAS.Play();
        }
        else
        {
            backgroundAS.Stop();
        }
    }


    public void PlaySound(SoundType soundType)
    {
        if (isMusicOn)
        {
            AudioClip audioClip = GetAudioClip(soundType);
            if (this.audioSource.isPlaying)
            {
                AudioSource audioSource = GetAudioSource();
                audioSource.clip = audioClip;
                audioSource.gameObject.AddComponent<DestroyAudioSource>();
                audioSource.Play();
            }
            else
            {
                this.audioSource.Stop();
                this.audioSource.clip = audioClip;
                this.audioSource.Play();
            }
        }
    }



    AudioSource GetAudioSource()
    {
        GameObject audioSourceGameObject = new GameObject();
        AudioSource audioSource = audioSourceGameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        return audioSource;
    }


    AudioClip GetAudioClip(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.Background:
                return background;
            case SoundType.TryAgain:
                return tryAgain;
            case SoundType.Carrot:
                return carrot;
            case SoundType.MonsterDeath :
                return monsterDeath;
            case SoundType.Win:
                return win;
            default:
                return null;
        }
    }



}