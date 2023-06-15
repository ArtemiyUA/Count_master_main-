using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Boss : MonoBehaviour
{
    [SerializeField] int _bossHealth = 10;
    public TMP_Text bossHealth;
    private void Awake()
    {
        bossHealth.text = _bossHealth.ToString();
    }

    public float moveSpeed = 5f;
    public Transform target;
    public void bossLife()
    {
        bossHealth.text = _bossHealth.ToString();
        if (_bossHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.STICKMAN_TAG))
        {
            MainTeamMovement.verticleSpeed = 1;
            Vector3 moveDirection = (target.position - transform.position).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            StartCoroutine(Fight(other));
 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.STICKMAN_TAG))
        {
            MainTeamMovement.verticleSpeed = 3;
        }
    }

    IEnumerator Fight(Collider other)
    {
        gameObject.GetComponent<Animator>().SetBool("bossAtack", true);
        yield return new WaitForSeconds(1f);
        other.GetComponent<Teammate>().LeaveTeam();
        _bossHealth--;
        bossLife();


    }
}
