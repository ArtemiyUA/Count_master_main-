using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float _speed;
    [HideInInspector] public Vector3 lookAt;
    private Vector3 compensation;
    private Transform _camera;
    private Animation anim;
    private bool isLook;
    private void Start()
    {
        _camera = transform.GetChild(0);
        anim = GetComponent<Animation>();
        compensation = transform.position - target.position;
        lookAt = Vector3.up;
    }
    void Update()
    {
        transform.position = target.position + compensation;
        if (isLook)
        {
            _camera.rotation = Quaternion.RotateTowards(_camera.rotation,
                                                    Quaternion.LookRotation(target.position - _camera.position + lookAt),
                                                    Time.deltaTime * _speed);
        }
    }
    public void EndGameAnimation(Transform target)
    {
       // anim.Play(Constants.END_GAME_CAMERA_ANIM);
        this.target = target;
        compensation = transform.position - target.position;
        isLook = true;
        lookAt = Vector3.zero;
    }
}
