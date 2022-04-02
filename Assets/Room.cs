using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject virtualCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Triggered");
        if (col.tag == "Player")
        {
            if (WorldManager.instance.currentCamera != null)
            {
                WorldManager.instance.currentCamera.SetActive(false);
            }
            WorldManager.instance.currentCamera = virtualCamera;
            virtualCamera.SetActive(true);
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
