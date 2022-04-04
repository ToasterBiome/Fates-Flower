using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] CanvasGroup fade;
    [SerializeField] CanvasGroup deathText;
    void OnEnable()
    {
        PlayerController.OnHealthChanged += OnHealthChanged;
        PlayerController.OnDeath += OnDeath;
    }

    void OnDisable()
    {
        PlayerController.OnHealthChanged -= OnHealthChanged;
        PlayerController.OnDeath -= OnDeath;
    }

    void OnHealthChanged(float health)
    {
        TimeSpan ts = TimeSpan.FromSeconds(health);
        healthText.SetText(string.Format("{0:00}:{1:00}", (int)ts.TotalMinutes, (int)ts.Seconds));
    }

    void OnDeath()
    {
        LeanTween.alphaCanvas(fade, 1f, 4f).setDelay(4f).setOnComplete(() =>
        {
            LeanTween.alphaCanvas(deathText, 1f, 1f).setDelay(1f);
        });
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
