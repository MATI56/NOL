using UnityEngine;
using UnityEngine.UI;

public class Bumbulator : BaseDevice<BumbulatorState>
{
    [SerializeField] private Sprite[] _symbolSprites;
    [SerializeField] private Image[] _buttonImages;

    private int[] _symbolValues = new int[] {0,0,0,0};
    
    public void Start()
    {
        CurrentState = new BumbulatorState();
    }
    public override bool IsStateCorrect(BumbulatorState state)
    {
        if(CurrentState.FirstSymbol != state.FirstSymbol || 
           CurrentState.SecondSymbol != state.SecondSymbol || 
           CurrentState.ThirdSymbol != state.ThirdSymbol || 
           CurrentState.FourthSymbol != state.FourthSymbol || 
           CurrentState.SwitchValue != state.SwitchValue)
        {
            return false;
        }
        return true;
    }
    public void IncreaseDigit(int index)
    {
        if (index < 0 || index > 3) return;

        if(_symbolValues[index] < _symbolSprites.Length - 1)
        {
            _symbolValues[index]++;
        }
        else
        {
            _symbolValues[index] = 0;
        }
        _buttonImages[index].sprite = _symbolSprites[_symbolValues[index]];
        UpdateState();

    }
    private void UpdateState()
    {
        CurrentState.FirstSymbol = _symbolValues[0];
        CurrentState.SecondSymbol = _symbolValues[1];
        CurrentState.ThirdSymbol = _symbolValues[2];
        CurrentState.FourthSymbol = _symbolValues[3];
    }
}

[System.Serializable]
public class BumbulatorState : BaseDeviceState
{
    [Range(0, 9)]
    public int FirstSymbol;
    [Range(0, 9)]
    public int SecondSymbol;
    [Range(0, 9)]
    public int ThirdSymbol;
    [Range(0, 9)]
    public int FourthSymbol;

    public int SwitchValue;

    public BumbulatorState()
    {
        FirstSymbol = 0;
        SecondSymbol = 0;
        ThirdSymbol = 0;
        FourthSymbol = 0;
        SwitchValue = 0;
    } 
}