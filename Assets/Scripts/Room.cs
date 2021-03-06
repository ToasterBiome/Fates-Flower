using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject virtualCamera;
    [SerializeField] List<GameObject> doors;
    [SerializeField] List<GameObject> enemies;

    [SerializeField] bool roomCleared = false;

    [SerializeField] GameObject flower;
    [SerializeField] bool rewardFlower = true;
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> trueDoors = new List<GameObject>();
        foreach (GameObject door in doors)
        {
            if (door.activeSelf)
            {
                trueDoors.Add(door);
            }
        }
        doors = trueDoors;
    }

    // Update is called once per frame
    void Update()
    {
        if (WorldManager.instance.currentRoom != this)
        {
            return;
        }
        if (enemies.Count == 0 && !roomCleared)
        {
            OnRoomCleared();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (WorldManager.instance.currentCamera != null)
            {
                WorldManager.instance.currentCamera.SetActive(false);
            }
            WorldManager.instance.currentCamera = virtualCamera;
            virtualCamera.SetActive(true);
            virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = col.gameObject.transform;
            Vector2 difference = (transform.position - col.gameObject.transform.position).normalized;
            if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
            {
                difference.y = 0;
            }
            else
            {
                difference.x = 0;
            }
            OnRoomEntered(col.gameObject, -difference);
        }
        if (col.tag == "Enemy")
        {
            enemies.Add(col.gameObject);
            col.GetComponent<Enemy>().OnDeath += OnDeath;
        }
    }

    void OnDeath(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().OnDeath -= OnDeath;
        enemies.Remove(enemy);
    }

    void OnRoomEntered(GameObject player, Vector2 directionEntered)
    {
        WorldManager.instance.currentRoom = this;
        if (!roomCleared)
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(true);
            }
        }
        player.transform.position = (Vector2)transform.position + directionEntered * 5f;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Spawn();
        }
    }

    void OnRoomCleared()
    {
        roomCleared = true;
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
        if (rewardFlower)
        {
            flower.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            //virtualCamera.SetActive(false);
        }
    }
}
