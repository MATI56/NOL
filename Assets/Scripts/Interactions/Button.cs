using UnityEngine;

public class Button : Interactable
{
    [SerializeField] private GameObject _blockCameraPosition;

    private void Start()
    {
        CameraMovmentController.Instance.OnCameraMove.AddListener(CheckBlockCameraPosition);
    }
    override public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (!_isInteractable) return;
        base.OnPointerClick(eventData);
    }
    private void CheckBlockCameraPosition()
    {
        if (_blockCameraPosition != null)
        {
            if (CameraMovmentController.Instance.CurrentCameraPosition == _blockCameraPosition)
            {
                _isInteractable = false;
            }
            else
            {
                _isInteractable = true;
            }
        }
    }
}
