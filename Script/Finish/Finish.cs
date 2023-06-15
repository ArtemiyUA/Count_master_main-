using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.GetComponent<BuildTower>().Build();
        GetComponent<Collider>().enabled = false;
    }
}
