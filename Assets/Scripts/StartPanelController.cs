using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanelController : MonoBehaviour
{
    public GameObject startPanel;
    public Button startBtn;

    public Button otherScenesBtn;
    public GameObject otherScenesPanel;
    public Button otherScenesBackBtn;
    public Button oneSceneBtn;
    public Button twoSceneBtn;
    public Button threeSceneBtn;

    public Button helpBtn;
    public GameObject helpPanel;
    public Button helpBackBtn;

    public Button creditsBtn;
    public GameObject creditsPanel;
    public Button creditsBackBtn;

    public Animator anim;

    private void Start()
    {
        startBtn.onClick.AddListener(() =>
        {
            anim.SetTrigger("End");
            StartCoroutine(ChangeScene("One"));
        });

        otherScenesBtn.onClick.AddListener(() =>
        {
            startPanel.SetActive(false);
            otherScenesPanel.SetActive(true);
        });
        otherScenesBackBtn.onClick.AddListener(() =>
        {
            startPanel.SetActive(true);
            otherScenesPanel.SetActive(false);
        });

        oneSceneBtn.onClick.AddListener(() =>
        {
            anim.SetTrigger("End");
            StartCoroutine(ChangeScene("One"));
        });

        twoSceneBtn.onClick.AddListener(() =>
        {
            anim.SetTrigger("End");
            StartCoroutine(ChangeScene("Two"));
        });

        threeSceneBtn.onClick.AddListener(() =>
        {
            anim.SetTrigger("End");
            StartCoroutine(ChangeScene("Three"));
        });


        helpBtn.onClick.AddListener(() =>
        {
            startPanel.SetActive(false);
            helpPanel.SetActive(true);
        });
        helpBackBtn.onClick.AddListener(() =>
        {
            startPanel.SetActive(true);
            helpPanel.SetActive(false);
        });

        creditsBtn.onClick.AddListener(() =>
        {
            startPanel.SetActive(false);
            creditsPanel.SetActive(true);
        });
        creditsBackBtn.onClick.AddListener(() =>
        {
            startPanel.SetActive(true);
            creditsPanel.SetActive(false);
        });

    }

    private void OnGameStartButtonClick()
    {
        anim.SetTrigger("End");
        StartCoroutine(ChangeScene("Main"));
    }

    private IEnumerator ChangeScene(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
