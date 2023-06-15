using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teammate : MonoBehaviour
{
    [HideInInspector] public TeamLeader leader;
    private List<TeamPoints> teammatePoints;

    public void JoinTeam()
    {
        GetComponent<MoveDirection>().StartMoveDirection();
        GetComponent<Movement>().StartMovement();
        GetComponent<Collider>().enabled = true;

        leader = transform.parent.GetComponent<TeamLeader>();
        teammatePoints = transform.parent.GetComponent<CreateTeamPoints>().teammatePoints;
        foreach (TeamPoints teammatePoint in teammatePoints)
        {
            if (!teammatePoint.Go)
            {
                teammatePoint.Go = gameObject;
                GetComponent<MoveTargetDirection>().target = teammatePoint.Point;
                leader.IncreaseHumanCount();
                break;
            }
        }
    }
    public void LeaveTeam()
    {
        StartCoroutine(LeaveTeamCoroutine());
    }
    IEnumerator LeaveTeamCoroutine()
    {
        transform.parent = null;
        GetComponent<Collider>().enabled = false;
        GetComponent<Animator>().SetTrigger(Constants.FALL_ANIM);
        GetComponent<MoveDirection>().StopMoveDirection();
        GetComponent<Movement>().StopMovement();
        leader.DecreaseHumanCount();

        foreach (TeamPoints teammatePoint in teammatePoints)
        {
            if (teammatePoint.Go == gameObject)
            {
                teammatePoint.Go = null;
                break;
            }
        }

        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }
}
