using UnityEngine;

public class Button : Interactable
{
   override public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        Debug.Log("Button Clicked!");
    }
}
