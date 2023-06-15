using System.Collections.Generic;
using UnityEngine;

public class CreateTeamPoints : MonoBehaviour
{
    [SerializeField] float layerCount;
    [SerializeField] float startAngle;
    [SerializeField] float spliLayerSize;

    [HideInInspector] public List<TeamPoints> teammatePoints;

    void Awake()
    {
        CreateSinCos();
    }
    void CreateSinCos()
    {
        AddList(0, 0, 0);
        for (int layer = 1; layer < layerCount; layer++)
        {
            float angle = startAngle / layer;
            for (float totalAngle = 0; totalAngle < 360; totalAngle += angle)
            {
                float sinX = Mathf.Sin(totalAngle * Mathf.PI / 180);
                float cosX = Mathf.Cos(totalAngle * Mathf.PI / 180);

                AddList(sinX, cosX, layer);
            }
        }
    }
    void AddList(float sinX, float cosX, int layer)
    {
        TeamPoints newTeammatePoint = new TeamPoints();
        newTeammatePoint.Point = new Vector3(sinX, 0, cosX) * layer / spliLayerSize;
        teammatePoints.Add(newTeammatePoint);
    }
}
