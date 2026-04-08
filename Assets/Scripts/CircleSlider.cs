using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class CircleSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Transform _knobVisual;
    [SerializeField] private float _minAngle = -135f;
    [SerializeField] private float _maxAngle = 135f;
    [SerializeField] private float _dragSensitivity = 0.35f;
    [SerializeField, Range(0f, 1f)] private float _normalizedValue = 0.5f;
    [SerializeField] private UnityEvent<float> _onValueChanged;

    private Camera _mainCamera;
    private bool _isDragging;
    private Plane _dragPlane;
    private Vector3 _startHitDirectionWorld;
    private float _startAngle;
    private Vector3 _localAxis;
    private Vector2 _mousePosition;
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

        ApplyVisualRotation();
    }
    private void Start()
    {
        _localAxis = _knobVisual.InverseTransformDirection(transform.forward);
        InputManager.Instance.InputActions.UI.Point.performed += OnDrag;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;

        // Plane perpendicular to knob axis, passing through knob center.
        Vector3 axisWorld = transform.TransformDirection(_localAxis.normalized);
        _dragPlane = new Plane(axisWorld, transform.position);

        Ray ray = _mainCamera.ScreenPointToRay(_mousePosition);
        if (_dragPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            _startHitDirectionWorld = Vector3.ProjectOnPlane(hitPoint - transform.position, axisWorld).normalized;
        }
        else
        {
            _startHitDirectionWorld = Vector3.zero;
        }

        _startAngle = GetCurrentAngle();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            _isDragging = false;
    }

    private void OnDrag(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
        if (!_isDragging) return;

        Vector3 axisWorld = transform.TransformDirection(_localAxis.normalized);
        Ray ray = _mainCamera.ScreenPointToRay(_mousePosition);

        if (!_dragPlane.Raycast(ray, out float enter)) return;

        Vector3 hitPoint = ray.GetPoint(enter);
        Vector3 currentDirectionWorld = Vector3.ProjectOnPlane(hitPoint - transform.position, axisWorld).normalized;

        if (_startHitDirectionWorld.sqrMagnitude < 0.0001f || currentDirectionWorld.sqrMagnitude < 0.0001f)
            return;

        float deltaAngle = Vector3.SignedAngle(_startHitDirectionWorld, currentDirectionWorld, axisWorld);
        float targetAngle = _startAngle + deltaAngle * _dragSensitivity;

        float newValue = Mathf.InverseLerp(_minAngle, _maxAngle, targetAngle);
        if (!Mathf.Approximately(newValue, _normalizedValue))
        {
            _normalizedValue = newValue;
            ApplyVisualRotation();
            _onValueChanged?.Invoke(_normalizedValue);
        }
    }

    private void ApplyVisualRotation()
    {
        float angle = GetCurrentAngle();
        _knobVisual.localRotation = Quaternion.AngleAxis(angle, _localAxis.normalized);
    }
    private float GetCurrentAngle()
    {
        return Mathf.Lerp(_minAngle, _maxAngle, _normalizedValue);
    }
}
