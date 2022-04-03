using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> roomTemplates;
    void Awake()
    {
        if (roomTemplates.Count == 0)
        {
            Debug.LogError("No available Room Templates to spawn.");
            Destroy(gameObject);
            return;
        }
        GameObject chosenTemplate = roomTemplates[Random.Range(0, roomTemplates.Count)];
        GameObject roomTemplate = Instantiate(chosenTemplate, transform.parent);
        Destroy(gameObject);
    }
}
