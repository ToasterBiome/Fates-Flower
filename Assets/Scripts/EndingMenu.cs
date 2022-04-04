using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingMenu : MonoBehaviour
{
    [SerializeField] CanvasGroup fade;
    [SerializeField] CanvasGroup one; //we do a bit of shitcoding
    [SerializeField] CanvasGroup two;
    [SerializeField] CanvasGroup three;
    [SerializeField] CanvasGroup four;
    [SerializeField] CanvasGroup five;

    [SerializeField] CanvasGroup six;
    [SerializeField] Button returnButton;

    void OnEnable()
    {
        returnButton.onClick.AddListener(() =>
        {
            LeanTween.alphaCanvas(fade, 1f, 1f).setOnComplete(() =>
            {
                SceneManager.LoadScene("MainMenu");
            });
        });
    }


    // Start is called before the first frame update
    void Start()
    {
        fade.alpha = 1f;
        one.alpha = 1f;
        LeanTween.alphaCanvas(fade, 0f, 1f).setDelay(1f).setOnComplete(() =>
        {
            LeanTween.alphaCanvas(one, 0f, 2f);
            LeanTween.alphaCanvas(two, 1f, 2f).setDelay(1f).setOnComplete(() =>
            {
                LeanTween.alphaCanvas(two, 0f, 2f);
                LeanTween.alphaCanvas(three, 1f, 2f).setDelay(1f).setOnComplete(() =>
                {
                    LeanTween.alphaCanvas(three, 0f, 2f);
                    LeanTween.alphaCanvas(four, 1f, 2f).setDelay(1f).setOnComplete(() =>
                {
                    LeanTween.alphaCanvas(four, 0f, 2f);
                    LeanTween.alphaCanvas(five, 1f, 2f).setDelay(1f).setOnComplete(() =>
                {
                    LeanTween.alphaCanvas(five, 0f, 2f);
                    LeanTween.alphaCanvas(six, 1f, 4f).setDelay(1f).setOnComplete(() =>
                {
                    returnButton.gameObject.SetActive(true);
                });
                });
                });
                });
            });
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
