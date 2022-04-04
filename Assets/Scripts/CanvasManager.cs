using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] CanvasGroup fade;
    [SerializeField] CanvasGroup deathText;
    [SerializeField] Button returnButton;
    void OnEnable()
    {
        PlayerController.OnHealthChanged += OnHealthChanged;
        PlayerController.OnDeath += OnDeath;
        PlayerController.OnDamage += OnDamage;
        PlayerController.OnHeal += OnHeal;
        returnButton.onClick.AddListener(() =>
        {
            LeanTween.alphaCanvas(fade, 1f, 1f).setOnComplete(() =>
            {
                SceneManager.LoadScene("MainMenu");
            });
        });
    }

    void OnDisable()
    {
        PlayerController.OnHealthChanged -= OnHealthChanged;
        PlayerController.OnDeath -= OnDeath;
        PlayerController.OnDamage -= OnDamage;
        PlayerController.OnHeal -= OnHeal;
    }

    void OnHealthChanged(float health)
    {
        TimeSpan ts = TimeSpan.FromSeconds(health);
        healthText.SetText(string.Format("{0:00}:{1:00}", (int)ts.TotalMinutes, (int)ts.Seconds));
    }

    void OnDamage()
    {
        //healthText.color = Color.red;
        //LeanTween.textColor(healthText.rectTransform, Color.white, 1f);
    }

    void OnHeal()
    {
        //healthText.color = Color.green;
        //LeanTween.textColor(healthText.rectTransform, Color.white, 1f);
    }

    void OnDeath()
    {
        LeanTween.alphaCanvas(fade, 1f, 4f).setDelay(4f).setOnComplete(() =>
        {
            LeanTween.alphaCanvas(deathText, 1f, 1f).setDelay(1f).setOnComplete(() =>
            {
                returnButton.gameObject.SetActive(true);
            });
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        fade.alpha = 1f;
        LeanTween.alphaCanvas(fade, 0, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
