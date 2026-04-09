using TMPro;
using UnityEngine;

public class Radio : BaseDevice<RadioState>
{
    [SerializeField] private RadioAudio[] radioAudios;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TextMeshProUGUI frequencyDisplay;
    [SerializeField] private Vector2 frequencyRange = new(70f, 120f);

    public void Start()
    {
        CurrentState = new RadioState();
        frequencyDisplay.SetText($"{CurrentState.Frequency:F1} MHz");
    }
    public void SetFrequency(float knobValue)
    {
        CurrentState.Frequency = Mathf.Lerp(frequencyRange.y, frequencyRange.x, knobValue);
        frequencyDisplay.SetText($"{CurrentState.Frequency:F1} MHz");
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
        if(CurrentState.Frequency != state.Frequency)
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
        Frequency = 70f;
    }
}