using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float smoothTime = 0.3f;

    public float walkSpeed = 2f;

    private float currentVelocity;

    private Transform cameraTransfrom;

    private Animator anim;

    [HideInInspector]
    public int currentGetCoins = 0;

    public CreateCoins createCoins;

    public Text getCoinText;

    public AudioSource soundEffect; // 音效

    public Text timeText;

    private float countdownTime = 60; // 倒计时时间（秒）

    private float remainingTime; // 剩余时间

    public GameObject winningPanel;

    public Button backHomepageButton;

    public GameObject winning;

    public GameObject fail;

    public Button restartBtn;

    public Button nextBtn;

    [HideInInspector]
    public bool gameOver;

    public Button menuBtn;

    public GameObject menuPanel;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransfrom = Camera.main.transform;

        anim = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;  //将鼠标锁定在屏幕中心位置，鼠标的移动不再对游戏视图产生影响

        remainingTime = countdownTime;

        winningPanel.SetActive(false);

        backHomepageButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });

        menuBtn.onClick.AddListener(() =>
        {
            gameOver = true;
            menuPanel.SetActive(true);
        });

        restartBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("One");
        });

        nextBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Two");
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            Movement();
            CountdDownTimer();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            currentGetCoins++;
            getCoinText.text = "Count：" + currentGetCoins + "/" + createCoins.createCoinsNum;
            soundEffect.Play();
            Destroy(other.gameObject);

            IsWinning();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            anim.SetBool("IsRunning", false);
            winningPanel.SetActive(true);
            restartBtn.gameObject.SetActive(true);
            nextBtn.gameObject.SetActive(false);
            winning.SetActive(false);
            fail.SetActive(true);
            gameOver = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    /// <summary>
    /// 移动
    /// </summary>
    private void Movement()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector2 inputDir = input.normalized;

        float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransfrom.eulerAngles.y;

        //判断键盘是否按下
        if (inputDir != Vector2.zero)
        {
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref currentVelocity, smoothTime);
            transform.Translate(transform.forward * walkSpeed * Time.deltaTime, Space.World);
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }

    /// <summary>
    /// 倒计时
    /// </summary>
    private void CountdDownTimer()
    {
        remainingTime -= Time.deltaTime; // 剩余时间递减
        if (remainingTime <= 0) // 如果剩余时间小于等于0
        {
            remainingTime = 0; // 剩余时间设置为0
            //Time.timeScale = 0;
            anim.SetBool("IsRunning", false);
            winningPanel.SetActive(true);
            winning.SetActive(false);
            fail.SetActive(true);
            restartBtn.gameObject.SetActive(true);
            nextBtn.gameObject.SetActive(false);
            gameOver = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        int min = (int)(remainingTime / 60); // 计算分钟数
        int sec = (int)(remainingTime - min * 60); // 计算秒数
        timeText.text =
            min.ToString("D2") + ":" + sec.ToString("D2"); // 格式化时间并显示到 Text 组件中
    }

    private void IsWinning()
    {
        if (currentGetCoins == createCoins.createCoinsNum)
        {
            //Time.timeScale = 0;
            anim.SetBool("IsRunning", false);
            winningPanel.SetActive(true);
            winning.SetActive(true);
            fail.SetActive(false);
            restartBtn.gameObject.SetActive(false);
            nextBtn.gameObject.SetActive(true);
            gameOver = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
