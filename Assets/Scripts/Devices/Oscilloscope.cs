using TMPro;
using UnityEngine;

public class Oscilloscope : BaseDevice<OscilloscopeState>
{
    [SerializeField] private TextMeshProUGUI _leftKnobValueText;
    [SerializeField] private TextMeshProUGUI _rightKnobValueText;
    [SerializeField] private AudioClip _knobSound;
    public void Start()
    {
        CurrentState = new OscilloscopeState();
    }
    public void UpdateLeftKnob(float value)
    {
        if (CurrentState.LeftKnobValue == (int)Mathf.Lerp(0, 25, value)) return;
        CurrentState.LeftKnobValue = (int)Mathf.Lerp(0, 25, value);
            _leftKnobValueText.SetText(CurrentState.LeftKnobValue.ToString());
        AudioManager.Instance.PlaySoundRandomPitch(_knobSound);
    }
    public void UpdateRightKnob(float value)
    {
        if (CurrentState.RightKnobValue == (int)Mathf.Lerp(0, 25, value)) return;
        Debug.Log(CurrentState.RightKnobValue.ToString());
        CurrentState.RightKnobValue = (int)Mathf.Lerp(0, 25, value);
        _rightKnobValueText.SetText(CurrentState.RightKnobValue.ToString());
        AudioManager.Instance.PlaySoundRandomPitch(_knobSound);
    }
    public override bool IsStateCorrect(OscilloscopeState state)
    {
        Debug.Log($"Checking state: LeftKnobValue={state.LeftKnobValue}, RightKnobValue={state.RightKnobValue}");
        if (CurrentState.LeftKnobValue == state.LeftKnobValue || (CurrentState.RightKnobValue >= state.RightKnobValue - 2 && CurrentState.RightKnobValue <= state.RightKnobValue + 2))
        {
            return true;
        }
        return false;
    }
}

[System.Serializable]
public class OscilloscopeState : BaseDeviceState
{
    public int LeftKnobValue;
    public int RightKnobValue;
    public OscilloscopeState()
    {
        LeftKnobValue = 0;
        RightKnobValue = 0;
    }
}
