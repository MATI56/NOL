using TMPro;
using UnityEngine;

public class Radio : BaseDevice<RadioState>
{
    [SerializeField] private RadioAudio[] radioAudios;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TextMeshProUGUI frequencyDisplay;
    [SerializeField] private Vector2 frequencyRange = new(70f, 120f);
    [SerializeField] private AudioClip _knobSound;

    public void Start()
    {
        CurrentState = new RadioState();
        frequencyDisplay.SetText($"{CurrentState.Frequency:F1} MHz");
        TryPlay();
    }
    public void SetFrequency(float knobValue)
    {
        if (CurrentState.Frequency == Mathf.Round(Mathf.Lerp(frequencyRange.y, frequencyRange.x, knobValue))) return;
        CurrentState.Frequency = Mathf.Round(Mathf.Lerp(frequencyRange.y, frequencyRange.x, knobValue));
        frequencyDisplay.SetText($"{CurrentState.Frequency:F1} MHz");
        AudioManager.Instance.PlaySoundRandomPitch(_knobSound);
    }
    public void TryPlay()
    {
        foreach (RadioAudio radioAudio in radioAudios)
        {
            if (Mathf.Approximately(radioAudio.Frequency, CurrentState.Frequency))
            {
                _audioSource.clip = radioAudio.Clip;
                _audioSource.Play();
                return;
            }
        }
        _audioSource.Stop();
    }
    public override bool IsStateCorrect(RadioState state)
    {
        Debug.Log($"Checking Radio State: Current Frequency = {CurrentState.Frequency}, Target Frequency = {state.Frequency}");
        if (CurrentState.Frequency != state.Frequency)
        {
            return false;
        }
        return true;
    }

    [System.Serializable]
    private class RadioAudio
    {
        public AudioClip Clip;
        [Range(70, 120)]
        public float Frequency = 70f;
    }
}

[System.Serializable]
public class RadioState : BaseDeviceState
{
    public float Frequency;
    public RadioState()
    {
        Frequency = 102f;
    }
}