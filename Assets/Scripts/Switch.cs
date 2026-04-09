using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Switch : Interactable
{
    [SerializeField] private Transform _switchVisual;
    [SerializeField] private Vector3 _onRotation;
    [SerializeField] private Vector3 _offRotation;
    [SerializeField] private float _toggleDuration = 0.5f;

    private bool _isOn;
    override public void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        ToggleSwitch();
    }
    private void ToggleSwitch()
    {
        
        _isOn = !_isOn;
        _switchVisual.DOLocalRotate(_isOn ? _onRotation : _offRotation, _toggleDuration);
    }
}
