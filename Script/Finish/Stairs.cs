using UnityEngine;

public class Stairs : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
        other.GetComponent<Teammate>().leader.IncreaseFinishedHumanCount();
    }
}
