using UnityEngine;

public class MainTeamMovement : Movement
{
    [SerializeField] float tempHorizontalSpeed;
    [SerializeField] float tempVerticleSpeed;
    private float horizontalSpeed;
    public static float verticleSpeed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void Move(Vector3 direction)
    {
        rb.velocity = new Vector3(direction.x * horizontalSpeed, 0, verticleSpeed);
    }

    public override void StartMovement()
    {
        horizontalSpeed = tempHorizontalSpeed;
        verticleSpeed = tempVerticleSpeed;
    }
    public override void StopMovement()
    {
        horizontalSpeed = 0;
        verticleSpeed = 0;
        rb.velocity = Vector3.zero;
    }
    
}
