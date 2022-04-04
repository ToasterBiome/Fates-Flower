using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button creditsButton;
    [SerializeField] Button exitButton;

    [SerializeField] GameObject creditsScreen;

    [SerializeField] CanvasGroup fade;

    void OnEnable()
    {
        startButton.onClick.AddListener(() =>
        {
            LeanTween.alphaCanvas(fade, 1f, 1f).setOnComplete(() =>
            {
                SceneManager.LoadScene("EndingScene");
            });
        });
        creditsButton.onClick.AddListener(() =>
        {
            creditsScreen.SetActive(!creditsScreen.activeSelf);
        });
        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
    // Start is called before the first frame update
    void Start()
    {
        fade.alpha = 1f;
        LeanTween.alphaCanvas(fade, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
