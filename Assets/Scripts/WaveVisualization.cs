using Unity.Mathematics;
using UnityEngine;

public class WaveVisualization : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _numPoints = 100;
    [SerializeField] private Vector2 _xLimits = new Vector2(0f, 2 * Mathf.PI);


    private float _amplitude = 1f;
    private float _frequency = 1f;
    private void Update()
    {
        Draw();
    }
    public void SetAmplitude(float amplitude)
    {
        _amplitude = amplitude;
    }
    public void SetFrequency(float frequency)
    {
        _frequency = frequency;
    }
    private void Draw()
    {
        float xStart = _xLimits.x;
        float Tau = 2 * Mathf.PI;
        float xEnd = _xLimits.y;

        _lineRenderer.positionCount = _numPoints;
        for(int currentPoint = 0; currentPoint < _numPoints; currentPoint++)
        {
            float t = (float)currentPoint / (_numPoints - 1);
            float x = Mathf.Lerp(xStart, xEnd, t);
            float y = _amplitude * Mathf.Sin((Tau * _frequency * x) + Time.timeSinceLevelLoad);
            _lineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0f));
        }
    }
}
