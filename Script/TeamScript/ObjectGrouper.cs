using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectGrouper : MonoBehaviour
{
    public static ObjectGrouper Instance { get; private set; }

    [SerializeField] List<ObjectGroupItem> teammateToGroup;
    private Dictionary<string, Queue<GameObject>> groupedObjects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        groupedObjects = new Dictionary<string, Queue<GameObject>>();

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        foreach (ObjectGroupItem stickman in teammateToGroup)
        {
            if (!groupedObjects.ContainsKey(stickman.objectToGroup.tag))
            {
                groupedObjects[stickman.objectToGroup.tag] = new Queue<GameObject>();
            }

            for (int i = 0; i < stickman.amountToGroup; i++)
            {
                GameObject teammate = Instantiate(stickman.objectToGroup);
                teammate.SetActive(false);
                groupedObjects[stickman.objectToGroup.tag].Enqueue(teammate);
            }
        }
    }

    public GameObject GetGroupedObject(string tag)
    {
        if (groupedObjects.ContainsKey(tag) && groupedObjects[tag].Count > 0)
        {
            GameObject groupedObject = groupedObjects[tag].Dequeue();
            groupedObject.SetActive(true);
            return groupedObject;
        }

        foreach (ObjectGroupItem item in teammateToGroup)
        {
            if (item.objectToGroup.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToGroup);
                    obj.SetActive(false);
                    return obj;
                }
            }
        }

        return null;
    }
}
