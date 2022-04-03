using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI healthText;
    void OnEnable()
    {
        PlayerController.OnHealthChanged += OnHealthChanged;
    }

    void OnDisable()
    {
        PlayerController.OnHealthChanged -= OnHealthChanged;
    }

    void OnHealthChanged(float health)
    {
        TimeSpan ts = TimeSpan.FromSeconds(health);
        healthText.SetText(string.Format("{0:00}:{1:00}", (int)ts.TotalMinutes, (int)ts.Seconds));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
