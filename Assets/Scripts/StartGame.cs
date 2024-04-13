using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Button gameStartButton;

    public Button gameIntroduceButton;

    public GameObject gameIntroducePanel;

    private Button closeButton;

    public Button gameExitButton;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        gameIntroducePanel.SetActive(false);

        gameStartButton.onClick.AddListener(OnGameStartButtonClick);
        gameIntroduceButton.onClick.AddListener(OnGameIntroduceButtonClick);
        gameExitButton.onClick.AddListener(OnGameExitButtonClick);

        closeButton = gameIntroducePanel.transform.GetChild(0).GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            gameIntroducePanel.SetActive(false);
        });
    }

    private void OnGameStartButtonClick()
    {
        anim.SetTrigger("End");
        Invoke("ChangeScene", 1);
    }

    private void OnGameIntroduceButtonClick()
    {
        gameIntroducePanel.SetActive(true);
    }

    private void OnGameExitButtonClick()
    {
#if UNITY_EDITOR  //ÍË³ö±àÒëÆ÷
        UnityEditor.EditorApplication.isPlaying = false;
#else  //ÍË³öEXE
	Application.Quit();
#endif
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Main");
    }
}
