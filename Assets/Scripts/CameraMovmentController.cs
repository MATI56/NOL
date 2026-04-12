using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class CameraMovmentController : MonoBehaviour
{
    public static CameraMovmentController Instance { get; private set; }

    public GameObject CurrentCameraPosition;
    public UnityEvent OnCameraMove;

    [SerializeField] private float _moveDuration = 3f;
    [SerializeField] private GameObject _defaultCameraPosition;

    private bool IsActive = true;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        InputManager.Instance.InputActions.UI.RightClick.performed += MoveCameraToDefaultPosition;
    }
    public void MoveCameraTo(GameObject target)
    {
        if (!IsActive) return;

        CurrentCameraPosition = target;
        transform.DOMove(target.transform.position, _moveDuration);
        transform.DORotate(target.transform.rotation.eulerAngles, _moveDuration);
        OnCameraMove?.Invoke();
    }
    public void SetCursor(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }
    private void MoveCameraToDefaultPosition(InputAction.CallbackContext context)
    {
        MoveCameraTo(_defaultCameraPosition);
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

}

