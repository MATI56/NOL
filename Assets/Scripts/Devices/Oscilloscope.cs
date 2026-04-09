using UnityEngine;

public class Oscilloscope : BaseDevice<OscilloscopeState>
{
    public override bool IsStateCorrect(OscilloscopeState state)
    {
        if(CurrentState.LeftKnobValue != state.LeftKnobValue || CurrentState.RightKnobValue != state.RightKnobValue)
        {
            return false;
        }
        return true;
    }
}

[System.Serializable]
public class OscilloscopeState : BaseDeviceState
{
    public float LeftKnobValue;
    public float RightKnobValue;
    public OscilloscopeState()
    {
        LeftKnobValue = 0f;
        RightKnobValue = 0f;
    }
}
