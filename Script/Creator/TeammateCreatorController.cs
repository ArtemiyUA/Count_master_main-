using UnityEngine;

public class TeammateCreatorController : MonoBehaviour
{
    bool isTaken;
    public void Take()
    {
        isTaken = true;
        Destroy(gameObject);
    }
    public bool GetIsTaken()
    {
        return isTaken;
    }
}
