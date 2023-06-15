using UnityEngine;

public class MoveTargetDirection : MoveDirection
{
    [HideInInspector] public Vector3 target;
    public override Vector3 CalculateDirection()
    {
        return target;
    }
}
