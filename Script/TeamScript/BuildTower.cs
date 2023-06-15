using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    [SerializeField] int perRowMaxTeammateCount;
    [SerializeField] float distanceBetweenTeammate;
    private List<int> towerCountList;
    private List<GameObject> towerList;
    private CameraHolder cameraAnimation;
    private bool move;

    private void Start()
    {
        towerCountList = new List<int>();
        towerList = new List<GameObject>();
        cameraAnimation = Camera.main.transform.parent.GetComponent<CameraHolder>();
    }
    void FixedUpdate()
    {
        if (move)
        {
            transform.GetComponent<Movement>().Move(Vector3.forward);
        }
    }
    public void Build()
    {
        GetComponent<TeamLeader>().TextActiveFalse();
        GetComponent<MoveDirection>().StopMoveDirection();
        GetComponent<Movement>().StopMovement();

        FillTowerList();
        StartCoroutine(BuildTowerCoroutine());
        cameraAnimation.EndGameAnimation(towerList[0].transform);
    }
    void FillTowerList()
    {
        int humanCount = GetComponent<TeamLeader>().teammateCount;

        for (int i = 1; i <= perRowMaxTeammateCount; i++)
        {
            if (humanCount < i)
            {
                break;
            }
            humanCount -= i;
            towerCountList.Add(i);
        }
        for (int i = perRowMaxTeammateCount; i > 0; i--)
        {
            if (humanCount >= i)
            {
                humanCount -= i;
                towerCountList.Add(i);
                i++;
            }
        }
        towerCountList.Sort();
    }
    IEnumerator BuildTowerCoroutine()
    {
        int towerId = 0;
        Vector3 sum;
        GameObject tower;
        float tempTowerHumanCount;
        transform.position = new Vector3(0, transform.position.y, transform.position.z);

        foreach (int towerHumanCount in towerCountList)
        {
            foreach (GameObject child in towerList)
            {
                child.transform.localPosition += Vector3.up;
            }
            tower = new GameObject("Tower" + towerId);
            tower.transform.parent = transform;
            tower.transform.localPosition = new Vector3(0, 0, 0);
            towerList.Add(tower);
            sum = Vector3.zero;
            tempTowerHumanCount = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.CompareTag(Constants.STICKMAN_TAG))
                {
                    child.GetComponent<Collider>().isTrigger = true;
                    child.GetComponent<MoveDirection>().StopMoveDirection();
                    child.GetComponent<Movement>().StopMovement();

                    child.transform.parent = tower.transform;
                    child.transform.localPosition = new Vector3(tempTowerHumanCount * distanceBetweenTeammate, 0, 0);
                    sum += child.transform.position;
                    tempTowerHumanCount++;
                    i--;
                    if (tempTowerHumanCount >= towerHumanCount)
                    {
                        break;
                    }
                }
            }
            tower.transform.position = new Vector3(-sum.x / towerHumanCount, tower.transform.position.y, tower.transform.position.z);
            sum = Vector3.zero;
            towerId++;
            yield return new WaitForSeconds(0.1f);
        }
        GetComponent<Movement>().StartMovement();
        move = true;
    }
}
