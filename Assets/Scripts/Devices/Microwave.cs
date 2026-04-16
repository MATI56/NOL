using DG.Tweening;
using TMPro;
using UnityEngine;

public class Microwave : BaseDevice<MicrowaveState>
{
    [SerializeField] private Light _light;
    [SerializeField] private TextMeshProUGUI _timeDisplay;
    [SerializeField] private AudioClip _knobSound;

    public void Start()
    {
        CurrentState = new MicrowaveState();
        _light.gameObject.SetActive(false);
        _light.intensity = 0f;
    }
    public void StartHeating()
    {
        CurrentState.IsHeating = true;
        _light.gameObject.SetActive(true);
        _light.DOColor(Color.red, 1f);
        _light.DOIntensity(0.3f, 1f);
    }
    public void StartFreezing()
    {
        CurrentState.IsHeating = false;
        _light.gameObject.SetActive(true);
        _light.DOColor(Color.cyan, 1f);
        _light.DOIntensity(0.3f, 1f);
    }
    public void SetTime(float timeValue)
    {
        if (CurrentState.TimeValue == (int)Mathf.Lerp(0, 1000, timeValue)) return;
        CurrentState.TimeValue = (int)Mathf.Lerp(0, 1000, timeValue);
        _timeDisplay.SetText($"{CurrentState.TimeValue}V");
        AudioManager.Instance.PlaySoundRandomPitch(_knobSound);
    }
    public override bool IsStateCorrect(MicrowaveState state)
    {
        Debug.Log($"Comparing states: Current - {CurrentState.IsHeating}, {CurrentState.TimeValue} | Target - {state.IsHeating}, {state.TimeValue}");
        if (CurrentState.IsHeating != state.IsHeating || CurrentState.TimeValue != state.TimeValue)
        {
            return false;
        }
        return true;
    }
}

[System.Serializable]
public class MicrowaveState : BaseDeviceState
{
    public bool IsHeating;
    public int TimeValue;
    public MicrowaveState()
    {
        IsHeating = false;
        TimeValue = 0;
    }
}
