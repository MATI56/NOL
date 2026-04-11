using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BoardLine : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GameObject[] _targetPoints;
    [SerializeField] private Vector3 _offset;

    private Vector3 _currentLinePosition;

    private void Start()
    {
        _lineRenderer.positionCount = 2;
        _currentLinePosition = _targetPoints[0].transform.position + _offset;
        _lineRenderer.SetPosition(0, _currentLinePosition);
        _lineRenderer.SetPosition(1, _currentLinePosition);
    }
    public void StartDrawLine()
    {
        Sequence seq = DOTween.Sequence();

        for (int i = 1; i < _targetPoints.Length; i++)
        {
            int index = i;
            Vector3 endPos = _targetPoints[index].transform.position + _offset;
            Vector3 currentPos = Vector3.zero;

            seq.AppendCallback(() =>
            {
                Vector3 prev = _lineRenderer.GetPosition(index - 1);
                _lineRenderer.positionCount = index + 1;
                _lineRenderer.SetPosition(index, prev);
                currentPos = prev;
            });

            seq.Append(
                DOTween.To(
                    () => currentPos,
                    value =>
                    {
                        currentPos = value;
                        _lineRenderer.SetPosition(index, value);
                    },
                    endPos,
                    1f
                ).SetEase(Ease.Linear)
            );
        }
    }
}
