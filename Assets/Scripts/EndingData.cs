using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "EndingData", menuName = "Scriptable Objects/EndingData")]
public class EndingData : ScriptableObject
{
    public string EndingName;
    public TimelineAsset EndingTimeline;

    public BumbulatorState BumbulatorState;
    public MicrowaveState MicrowaveState;
    public RadioState RadioState;
    public OscilloscopeState OscilloscopeState;
}