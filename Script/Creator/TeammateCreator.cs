using System.Collections;
using UnityEngine;
using TMPro;

public class TeammateCreator : MonoBehaviour
{
    enum Operation { Sum, Multiplication };
    [SerializeField] Operation operation;
    [SerializeField] float value;
    [SerializeField] TMP_Text text;
    private TeammateCreatorController teammateCreatorController;
    private float count;
    private void Start()
    {
        teammateCreatorController = transform.parent.GetComponent<TeammateCreatorController>();
        if (operation == Operation.Sum)
        {
            text.text = "+";
            text.text += value.ToString();
        }
        else if (operation == Operation.Multiplication)
        {
            text.text = "x";
            text.text += (value + 1).ToString();
        }
        //text.text +=  value .ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        float randomPositionRange = 0.5f;
        if (!teammateCreatorController.GetIsTaken())
        {
            Transform parent = other.transform.parent;
            if (operation == Operation.Sum)
            {
                count = value;
            }
            else if (operation == Operation.Multiplication)
            {
                count = parent.GetComponent<TeamLeader>().teammateCount * value;
            }
            for (int i = 0; i < count; i++)
            {
                Vector3 position = new Vector3(Random.Range(parent.position.x - randomPositionRange, parent.position.x + randomPositionRange),
                                                parent.position.y,
                                                Random.Range(parent.position.z - randomPositionRange, parent.position.z + randomPositionRange));

                GameObject human = ObjectGrouper.Instance.GetGroupedObject(Constants.STICKMAN_TAG);
                human.transform.position = position;
                human.transform.SetParent(parent);
                human.GetComponent<Teammate>().JoinTeam();
                human.SetActive(true);
            }
            teammateCreatorController.Take();
        }
    }
}
