using UnityEngine;
using UnityEngine.Playables;

public class EndingsManager : MonoBehaviour
{
    [SerializeField] private EndingData[] endings;
    [SerializeField] private PlayableDirector timelineDirector;

    [SerializeField] private Bumbulator _bumbulator;
    [SerializeField] private Microwave _microwave;
    [SerializeField] private Radio _radio;

    public void CheckEndings()
    {
        foreach (EndingData ending in endings)
        {
            if (ending.BumbulatorState.IsOn && !_bumbulator.IsStateCorrect(ending.BumbulatorState))
            {
                return;
            }
            else if (ending.MicrowaveState.IsOn && !_microwave.IsStateCorrect(ending.MicrowaveState))
            {
                return;
            }
            else if (ending.RadioState.IsOn && !_radio.IsStateCorrect(ending.RadioState))
            {
                return;
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