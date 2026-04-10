using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class CameraMovmentController : MonoBehaviour
{
    [SerializeField] private float _moveDuration = 3f;
    [SerializeField] private GameObject _defaultCameraPosition;

    private void Start()
    {
        InputManager.Instance.InputActions.UI.RightClick.performed += MoveCameraToDefaultPosition;
    }

    private void MoveCameraToDefaultPosition(InputAction.CallbackContext context)
    {
        transform.DOMove(_defaultCameraPosition.transform.position, _moveDuration);
        transform.DORotate(_defaultCameraPosition.transform.rotation.eulerAngles, _moveDuration);
    }

    public void MoveCameraTo(GameObject target)
    {
        transform.DOMove(target.transform.position, _moveDuration);
        transform.DORotate(target.transform.rotation.eulerAngles, _moveDuration);
    }
}
