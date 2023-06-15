using UnityEngine;

public abstract class MoveDirection : MonoBehaviour
{
    private Vector3 direction;
    private Movement movement;
    TeamLeader teamLeader;
    private void Awake()
    {
        teamLeader = FindObjectOfType<TeamLeader>();
        movement = GetComponent<Movement>();
    }
    private void Update()
    {
        direction = CalculateDirection();
    }
    void FixedUpdate()
    {
        movement.Move(direction);
    }
    public void StartMoveDirection()
    {
        enabled = true;

    }
    public void StopMoveDirection()
    {
        enabled = false;
    }
    public abstract Vector3 CalculateDirection();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            // Получить все объекты с Movement и HumanMovement в TeamLeader
            Movement[] movements = teamLeader.GetComponentsInChildren<Movement>();

            foreach (Movement movement in movements)
            {
                movement.SetSurrounded(true);
            }
        }
    }
}
