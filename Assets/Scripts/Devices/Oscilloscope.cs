using TMPro;
using UnityEngine;

public class Oscilloscope : BaseDevice<OscilloscopeState>
{
    [SerializeField] private TextMeshProUGUI _leftKnobValueText;
    [SerializeField] private TextMeshProUGUI _rightKnobValueText;
    public void Start()
    {
        CurrentState = new OscilloscopeState();
    }
    public void UpdateLeftKnob(float value)
    {
        CurrentState.LeftKnobValue = (int)Mathf.Lerp(0, 25, value);
            _leftKnobValueText.SetText(CurrentState.LeftKnobValue.ToString());
    }
    public void UpdateRightKnob(float value)
    {
        CurrentState.RightKnobValue = (int)Mathf.Lerp(0, 25, value);
        _rightKnobValueText.SetText(CurrentState.RightKnobValue.ToString());
    }
    public override bool IsStateCorrect(OscilloscopeState state)
    {
        Debug.Log($"Checking state: LeftKnobValue={state.LeftKnobValue}, RightKnobValue={state.RightKnobValue}");
        if (CurrentState.LeftKnobValue != state.LeftKnobValue || CurrentState.RightKnobValue != state.RightKnobValue)
        {
            return false;
        }
        return true;
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
