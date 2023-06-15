using UnityEngine;
using TMPro;
using System.Collections;

public class TeamLeader : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text teammateCountText;
    [SerializeField] private int defaultTeammateCount;

    public int teammateCount;
    private int finishedTeammateCount;

    private void Start()
    {
        CreateHuman();
        teammateCountText.gameObject.SetActive(false);
    }

    private void CreateHuman()
    {
        for (int i = 0; i < defaultTeammateCount; i++)
        {
            GameObject human = ObjectGrouper.Instance.GetGroupedObject(Constants.STICKMAN_TAG);
            human.transform.position = transform.position;
            human.transform.SetParent(transform);
            human.GetComponent<Teammate>().JoinTeam();
            human.SetActive(true);
        }
    }

    public void IncreaseFinishedHumanCount()
    {
        finishedTeammateCount++;

        if (finishedTeammateCount == teammateCount)
        {
            GetComponent<BuildTower>().enabled = false;
            gameManager.Win();
        }
    }

    public void IncreaseHumanCount()
    {
        teammateCount++;
        UpdateText();
    }

    public void DecreaseHumanCount()
    {
        teammateCount--;
        UpdateText();

        if (teammateCount == 0)
        {
            gameManager.Lose();
        }
    }

    private void UpdateText()
    {
        teammateCountText.text = teammateCount.ToString();
    }

    public void TextActiveFalse()
    {
        teammateCountText.gameObject.SetActive(false);
    }

    public void OnTextHolder()
    {
        teammateCountText.gameObject.SetActive(true);
    }

    public void StopTeammateMovement()
    {
        Movement[] movements = GetComponentsInChildren<Movement>();

        foreach (Movement movement in movements)
        {
            movement.SetSurrounded(true);
            StartCoroutine(StartMove());
        }
    }

    private IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Movement>().SetSurrounded(false);
    }
}
