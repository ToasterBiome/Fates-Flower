using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;
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

    }


}
