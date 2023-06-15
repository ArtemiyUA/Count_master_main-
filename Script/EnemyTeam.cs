using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyTeam : MonoBehaviour
{
    public Transform enemyTransform; 
    public float moveSpeed = 2f; 

    public TMP_Text enemyCountText;
    public static int enemyCounter;

    private List<Transform> enemyTransforms = new List<Transform>(); 

    private Animator animator;
    void Start()
    {

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Enemy"))
            {
                enemyTransforms.Add(child);

            }
        }

        enemyCounter = enemyTransforms.Count;
    }

    void Update()
    {
        if (enemyCounter <= 0)
        {
            enemyCountText.gameObject.SetActive(false);
        }

        enemyCountText.text = enemyCounter.ToString();
        if (enemyTransform != null && GetComponent<Collider>().bounds.Contains(enemyTransform.position))
        {
            foreach (Transform enemy in enemyTransforms)
            {
                enemy.position = Vector3.MoveTowards(enemy.position, enemyTransform.position, moveSpeed * Time.deltaTime);
                enemy.gameObject.GetComponent<Animator>().SetBool("atack", true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.STICKMAN_TAG))
        {
            MainTeamMovement.verticleSpeed = 1;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.STICKMAN_TAG))
        {
            MainTeamMovement.verticleSpeed = 3;

        }
    }
}
