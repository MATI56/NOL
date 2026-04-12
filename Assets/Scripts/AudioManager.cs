using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _sfxSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void SetMasterVolume(float level)
    {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
    }

    public void PlaySound(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }
    public void PlaySoundRandomPitch(AudioClip clip)
    {
        _sfxSource.pitch = Random.Range(0.85f, 1.0f);
        PlaySound(clip);
    }
}
