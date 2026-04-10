using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : Interactable, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Vector3 _localAxis;
    [SerializeField] private Vector3 _offSet;

    [SerializeField] private bool _UseDragPosition;

    [SerializeField] private Vector3 _dragPosition;
    [SerializeField] private Vector3 _dragRotation;
    [SerializeField] private Vector3 _dragScale;

    private Vector3 _normalPosition;
    private Vector3 _normalRotation;
    private Vector3 _normalScale;

    private Plane _dragPlane;

    private void Start()
    {
        _normalPosition = transform.position;
        _normalRotation = transform.localEulerAngles;
        _normalScale = transform.localScale;

        if (_UseDragPosition)
        {
            transform.position = _dragPosition;
            transform.localEulerAngles = _dragRotation;
            transform.localScale = _dragScale;
        }
        _dragPlane = new Plane(_localAxis, transform.position);

        transform.position = _normalPosition;
        transform.localEulerAngles = _normalRotation;
        transform.localScale = _normalScale;

    }
    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        if (_dragPlane.Raycast(ray, out float enter))
        {
            transform.DOMove(ray.GetPoint(enter) + _offSet, 0.1f);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_UseDragPosition)
        {
            transform.DOMove(_dragPosition, 0.1f);
            transform.DORotate(_dragRotation, 0.1f);
            transform.DOScale(_dragScale, 0.1f);
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_UseDragPosition)
        {
            transform.DOMove(_normalPosition, 0.1f);
            transform.DORotate(_normalRotation, 0.1f);
            transform.DOScale(_normalScale, 0.1f);
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            if (_dragPlane.Raycast(ray, out float enter))
            {
                transform.DOMove(ray.GetPoint(enter), 0.1f);
            }
        }
    }
}