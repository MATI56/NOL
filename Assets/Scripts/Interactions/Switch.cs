using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Switch : Interactable
{
    [SerializeField] private bool _initialState = false;
    [SerializeField] private Transform _switchVisual;
    [SerializeField] private Vector3 _onRotation;
    [SerializeField] private Vector3 _offRotation;
    [SerializeField] private float _toggleDuration = 0.5f;

    [SerializeField] private UnityEvent _onTurnOn;
    [SerializeField] private UnityEvent _onTurnOff;

    [SerializeField] private UnityEvent _onForceTurnOn;
    [SerializeField] private UnityEvent _onForceTurnOff;

    private bool _isOn;

    private void Start()
    {
        _isOn = _initialState;
        _switchVisual.localRotation = Quaternion.Euler(_isOn ? _onRotation : _offRotation);
    }
    override public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isInteractable) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;

        base.OnPointerClick(eventData);
        ToggleSwitch();
    }
    public void ForceSetState(bool isOn)
    {
        if (!_isInteractable) return;
        if (_isOn == isOn) return;
        _isOn = isOn;
        
        if (_isOn)
        {
            _onForceTurnOn?.Invoke();
        }
        else
        {
            _onForceTurnOff?.Invoke();
        }
        _switchVisual.DOLocalRotate(_isOn ? _onRotation : _offRotation, _toggleDuration);
    }
    public void SetState(bool isOn)
    {
        if (!_isInteractable) return;
        if (_isOn == isOn) return;
        _isOn = isOn;
        if (_isOn)
        {
            _onTurnOn?.Invoke();
        }
        else
        {
            _onTurnOff?.Invoke();
        }
        _switchVisual.DOLocalRotate(_isOn ? _onRotation : _offRotation, _toggleDuration);
    }
    private void ToggleSwitch()
    {
        SetState(!_isOn);
    }
}
