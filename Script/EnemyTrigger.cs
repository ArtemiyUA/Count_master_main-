using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.STICKMAN_TAG))
        {

            other.GetComponent<Teammate>().LeaveTeam();
            //Destroy(gameObject);
            EnemyTeam.enemyCounter--;
            gameObject.SetActive(false);
        }
    }
}
