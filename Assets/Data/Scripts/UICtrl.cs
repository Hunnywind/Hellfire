using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour {

    private Image musicImage;
    //private Text timerText;
    private Text killCountText;
    public GameObject quitDialog;

    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    private bool musicOff = false;

    public static float startTime = 20f;
    public static float maxTime = 25f;
    public static float currentTime = 0f;
    private int killCount;

    public Image timerGuage;

    void Awake()
    {
        musicImage = GameObject.Find("Music").GetComponent<Image>();
        //timerText = GameObject.Find("Timer").GetComponent<Text>();
        killCountText = GameObject.Find("KillCount").GetComponent<Text>();
    }

    void Start()
    {
        currentTime = startTime;
        timerGuage.fillAmount = (float)currentTime / (float)maxTime;
        InvokeRepeating("ResetText", 0f, 0.3f);
    }

    void FixedUpdate()
    {
        TimerCtrl();
    }
    void TimerCtrl()
    {
        currentTime -= Time.deltaTime;
        timerGuage.fillAmount = currentTime / maxTime;
        if (currentTime <= 0)
        {
            currentTime = 0;
            GameManager.instance.GameOver();
        }
    }
    public void MusicCtrl()
    {
        if (musicOff)
        {
            AudioListener.volume = 1.0f;
            musicImage.sprite = musicOnSprite;
            musicOff = !musicOff;
        }
        else
        {
            AudioListener.volume = 0.0f;
            musicImage.sprite = musicOffSprite;
            musicOff = !musicOff;
        }
    }

    void ResetText()
    {
        //timerText.text = "Left Time : " + currentTime;
        killCountText.text = GameManager.instance.m_killCount.ToString();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            quitDialog.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Quit(bool quit)
    {
        if (quit)
            Application.Quit();
        else {
            quitDialog.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
