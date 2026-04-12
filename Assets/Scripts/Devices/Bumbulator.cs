using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bumbulator : BaseDevice<BumbulatorState>
{
    [SerializeField] private Sprite[] _symbolSprites;
    [SerializeField] private Image[] _buttonImages;

    [SerializeField] private TextMeshProUGUI _switchValueText;

    private int _switchValue = 0;

    private int[] _symbolValues = new int[] {0,0,0,0};
    
    public void Start()
    {
        CurrentState = new BumbulatorState();
        _switchValueText.SetText(_switchValue.ToString());
    }
    public override bool IsStateCorrect(BumbulatorState state)
    {
        Debug.Log($"Comparing states: Current - {CurrentState.FirstSymbol}, {CurrentState.SecondSymbol}, {CurrentState.ThirdSymbol}, {CurrentState.FourthSymbol}, {CurrentState.SwitchValue} | Target - {state.FirstSymbol}, {state.SecondSymbol}, {state.ThirdSymbol}, {state.FourthSymbol}, {state.SwitchValue}");
        if (CurrentState.FirstSymbol != state.FirstSymbol || 
           CurrentState.SecondSymbol != state.SecondSymbol || 
           CurrentState.ThirdSymbol != state.ThirdSymbol || 
           CurrentState.FourthSymbol != state.FourthSymbol || 
           CurrentState.SwitchValue != state.SwitchValue)
        {
            return false;
        }
        return true;
    }
    public void AddToSwitchValue(int value)
    {
        _switchValue += value;
        UpdateState();
    }
    public void SubtractFromSwitchValue(int value)
    {
        _switchValue -= value;
        UpdateState();
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

        CurrentState.SwitchValue = _switchValue;
        _switchValueText.SetText(_switchValue.ToString());
    }
}

[System.Serializable]
public class BumbulatorState : BaseDeviceState
{
    [Range(0, 12)]
    public int FirstSymbol;
    [Range(0, 12)]
    public int SecondSymbol;
    [Range(0, 12)]
    public int ThirdSymbol;
    [Range(0, 12)]
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