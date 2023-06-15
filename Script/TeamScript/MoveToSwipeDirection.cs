using UnityEngine;

public class MoveToSwipeDirection : MoveDirection
{
    [SerializeField] float stopTolerance;
    [SerializeField] float endPointSpeed;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private float inputSpeed;
    private Camera _camera;

    void Start()
    {
        endPoint = Vector3.zero;
        _camera = Camera.main;
    }
    public override Vector3 CalculateDirection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = _camera.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.nearClipPlane));
            endPoint = startPoint;
        }
        if (Input.GetMouseButton(0))
        {
            startPoint = _camera.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.nearClipPlane));
            inputSpeed = GetToleranceSpeed(startPoint.x, endPoint.x);
            endPoint = Vector3.Lerp(endPoint, startPoint, endPointSpeed * Time.deltaTime);
            return new Vector3(inputSpeed, 0, 0);
        }
        return Vector3.zero;
    }
    float GetToleranceSpeed(float startPoint, float endPoint)
    {
        float inputSpeed = startPoint - endPoint;
        if (Mathf.Abs(inputSpeed) > stopTolerance)
        {
            return inputSpeed;
        }
        else
        {
            return 0;
        }
    }
}
