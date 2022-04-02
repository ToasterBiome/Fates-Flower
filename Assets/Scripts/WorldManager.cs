using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;

    [SerializeField]
    Transform player;
    public GameObject currentCamera;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }

    void Start()
    {

    }

    void Update()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Mathf.Clamp(player.transform.position.y, -3, 3), Camera.main.transform.position.z);
    }


}
