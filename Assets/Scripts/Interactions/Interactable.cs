using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class Interactable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected bool _isInteractable = true;

    [SerializeField] protected UnityEvent OnLeftClickEvent;
    [SerializeField] protected UnityEvent OnRightClickEvent;

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (!_isInteractable) return;
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClickEvent?.Invoke();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
            OnRightClickEvent?.Invoke();
    }
    public void SetInteractable(bool isInteractable)
    {
        _isInteractable = isInteractable;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isInteractable) return;
        Cursor.SetCursor(GameManager.Instance.HoverCursor, Vector2.zero, CursorMode.Auto);
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!_isInteractable) return;
        Cursor.SetCursor(GameManager.Instance.DefaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
