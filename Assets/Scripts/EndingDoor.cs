using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDoor : MonoBehaviour
{
    bool transitioning = false;
    public static Action OnGameComplete;

    [SerializeField] GameObject keyRequiredText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && WorldManager.instance.key && !transitioning)
        {
            keyRequiredText.SetActive(false); //just to make sure
            transitioning = true;
            OnGameComplete?.Invoke();
        }
        if (collider.tag == "Player" && !WorldManager.instance.key && !transitioning)
        {
            keyRequiredText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            keyRequiredText.SetActive(false);
        }
    }
}
