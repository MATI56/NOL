using UnityEngine;
using UnityEngine.Playables;

public class EndingsManager : MonoBehaviour
{
    [SerializeField] private EndingData[] endings;
    [SerializeField] private PlayableDirector timelineDirector;

    [SerializeField] private Bumbulator _bumbulator;
    [SerializeField] private Microwave _microwave;
    [SerializeField] private Radio _radio;
    [SerializeField] private Oscilloscope _oscilloscope;

    public void CheckEndings()
    {
        foreach (EndingData ending in endings)
        {
            Debug.Log($"Checking ending: {ending.EndingName}");
            if (ending.BumbulatorState.IsRequired && !_bumbulator.IsStateCorrect(ending.BumbulatorState))
            {
                continue;
            }
            else if (ending.MicrowaveState.IsRequired && !_microwave.IsStateCorrect(ending.MicrowaveState))
            {
                continue;
            }
            else if (ending.RadioState.IsRequired && !_radio.IsStateCorrect(ending.RadioState))
            {
                continue;
            }
            else if (ending.OscilloscopeState.IsRequired && !_oscilloscope.IsStateCorrect(ending.OscilloscopeState))
            {
                continue;
            }
            else
            {
                PlayEnding(ending);
                break;
            }
        }
    }

    private void PlayEnding(EndingData ending)
    {
        timelineDirector.playableAsset = ending.EndingTimeline;
        timelineDirector.Play();
    }
}