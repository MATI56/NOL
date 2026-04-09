using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : Interactable, IDragHandler
{
    [SerializeField] private Vector3 _localAxis;
    [SerializeField] private Vector3 _offSet;

    private Plane _dragPlane;

    private void Start()
    {
        _dragPlane = new Plane(_localAxis, transform.position);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        if (_dragPlane.Raycast(ray, out float enter))
        {
           
            transform.position = ray.GetPoint(enter) + _offSet;
        }  
    }
}
