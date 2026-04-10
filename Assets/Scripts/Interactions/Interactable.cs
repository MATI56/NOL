using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class Interactable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected UnityEvent OnLeftClickEvent;
    [SerializeField] protected UnityEvent OnRightClickEvent;
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClickEvent?.Invoke();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
            OnRightClickEvent?.Invoke();
    }
}
