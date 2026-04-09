using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CircleSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Transform _knobVisual;
    [SerializeField] private float _minAngle = -135f;
    [SerializeField] private float _maxAngle = 135f;
    [SerializeField] private float _dragSensitivity = 0.35f;
    [SerializeField] private Vector3 _localAxis = Vector3.forward;
    [SerializeField, Range(0f, 1f)] private float _normalizedValue = 0.5f;
    [SerializeField] private UnityEvent<float> _onValueChanged;

    [SerializeField] private TextMeshProUGUI _outputText;

    private Camera _mainCamera;
    private bool _isDragging;
    private Plane _dragPlane;
    private Vector3 _lastHitDirectionWorld;
    private float _currentAngle;
    public float Value
    {
        get => _normalizedValue;
        set
        {
            float clamped = Mathf.Clamp01(value);
            if (Mathf.Approximately(clamped, _normalizedValue)) return;

            _normalizedValue = clamped;
            ApplyVisualRotation();
            _onValueChanged?.Invoke(_normalizedValue); 
        }
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        _currentAngle = Mathf.Lerp(_minAngle, _maxAngle, _normalizedValue);
        ApplyVisualRotation();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        _isDragging = true;

        Vector3 axisWorld = _knobVisual.TransformDirection(_localAxis.normalized);
        _dragPlane = new Plane(axisWorld, _knobVisual.position);

        Ray ray = _mainCamera.ScreenPointToRay(eventData.position);
        if (_dragPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            _lastHitDirectionWorld = Vector3.ProjectOnPlane(hitPoint - _knobVisual.position, axisWorld).normalized;
        }
        else
        {
            _lastHitDirectionWorld = Vector3.zero;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            _isDragging = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging) return;
        if(eventData.button != PointerEventData.InputButton.Left) return;

        Vector3 axisWorld = transform.TransformDirection(_localAxis.normalized);
        Ray ray = _mainCamera.ScreenPointToRay(eventData.position);

        if (!_dragPlane.Raycast(ray, out float enter)) return;

        Vector3 hitPoint = ray.GetPoint(enter);
        Vector3 currentDirectionWorld = Vector3.ProjectOnPlane(hitPoint - _knobVisual.position, axisWorld).normalized;

        if (_lastHitDirectionWorld.sqrMagnitude < 0.0001f || currentDirectionWorld.sqrMagnitude < 0.0001f)
            return;

        float deltaAngle = Vector3.SignedAngle(_lastHitDirectionWorld, currentDirectionWorld, axisWorld);
        _currentAngle += deltaAngle * _dragSensitivity;
        _currentAngle = Mathf.Clamp(_currentAngle, _minAngle, _maxAngle);

        float newValue = Mathf.InverseLerp(_minAngle, _maxAngle, _currentAngle);

        if (!Mathf.Approximately(newValue, _normalizedValue))
        {
            _normalizedValue = newValue;
            ApplyVisualRotation();
            _onValueChanged?.Invoke(_normalizedValue);
        }

        _lastHitDirectionWorld = currentDirectionWorld;
    }

    private void ApplyVisualRotation()
    {
        _knobVisual.localRotation = Quaternion.AngleAxis(_currentAngle, _localAxis.normalized);
        if (_outputText != null)
        {
            _outputText.text = _normalizedValue.ToString("F2");
        }
    }
}
