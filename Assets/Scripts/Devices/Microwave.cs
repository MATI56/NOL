using DG.Tweening;
using UnityEngine;

public class Microwave : BaseDevice<MicrowaveState>
{
    [SerializeField] private Light _light;

    public void Start()
    {
        CurrentState = new MicrowaveState();
        _light.gameObject.SetActive(false);
        _light.intensity = 0f;
    }
    public void StartHeating()
    {
        if(_light.gameObject.activeSelf) return;

        CurrentState.IsHeating = true;
        _light.gameObject.SetActive(true);
        _light.color = Color.red;
        _light.DOIntensity(0.09f, 1f);
    }
    public void StartFreezing()
    {
        if (_light.gameObject.activeSelf) return;

        CurrentState.IsHeating = false;
        _light.gameObject.SetActive(true);
        _light.color = Color.cyan;
        _light.DOIntensity(0.09f, 1f);
    }
    public void SetTime(float timeValue)
    {
        CurrentState.TimeValue = timeValue;
    }
    public override bool IsStateCorrect(MicrowaveState state)
    {
        if(CurrentState.IsHeating != state.IsHeating || CurrentState.TimeValue != state.TimeValue)
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
    public float TimeValue;
    public MicrowaveState()
    {
        IsHeating = false;
        TimeValue = 0f;
    }
}
