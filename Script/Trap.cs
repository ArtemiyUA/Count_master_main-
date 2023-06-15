using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Teammate>().LeaveTeam();
    }
}
