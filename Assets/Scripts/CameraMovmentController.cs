using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class CameraMovmentController : MonoBehaviour
{
    [SerializeField] private float moveDuration = 3f;
    public void MoveCameraTo(GameObject target)
    {
        transform.DOMove(target.transform.position, moveDuration);
        transform.DORotate(target.transform.rotation.eulerAngles, moveDuration);
    }

}
