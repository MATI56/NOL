using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent OnLeftClickEvent;
    [SerializeField] private UnityEvent OnRightClickEvent;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            OnLeftClickEvent?.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            OnRightClickEvent?.Invoke();
    }
}
