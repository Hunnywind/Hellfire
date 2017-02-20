using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private int m_stageInitHuman = 10;
    private float m_regenTime;
    public int m_killCount = 0;

    public float m_sunAtk;
    public float m_sunRStart;
    public float m_sunREnd;
    public float m_earthSpeed;

    private GameObject m_earth;

    public int m_stageNumber;
    private float m_startTimer;
    private float m_nextTime;
    private float m_reTime;
    private float m_obstacleTime;
    public float m_helperTime;
    public int m_comboCount;

    private ObjectPoolerScript m_humanPool;
    private ObjectPoolerScript m_obstaclePool;
    private ObjectPoolerScript m_helperPool;


    public int humanCount;
    private static int deadCount = 0;
    GameObject gameoverGB;

    public static bool isGameover = false;
    public GameObject exitButton;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
        
    }

    void Start()
    {
        m_comboCount = 0;
        m_humanPool = GameObject.Find("HumanPool").GetComponent<ObjectPoolerScript>();
        m_obstaclePool = GameObject.Find("ObstaclePool").GetComponent<ObjectPoolerScript>();
        m_helperPool = GameObject.Find("HelperPool").GetComponent<ObjectPoolerScript>();
        m_earth = GameObject.Find("Earth");
        m_earth.GetComponent<Rigidbody>().angularDrag = GameDatabase.Instance().GetInitData("RotationAcceleStop");
        m_sunAtk = GameDatabase.Instance().GetInitData("SunAtk");
        m_sunRStart = GameDatabase.Instance().GetInitData("SunRangeStart");
        m_sunREnd = GameDatabase.Instance().GetInitData("SunRangeEnd");
        m_earthSpeed = 1f;
        m_stageNumber = 1;
        humanCount = 0;
        StageStart();
        
    }
    void Update()
    {
        if (m_stageNumber == 0) return;
        if (GameManager.isGameover) return;

        m_startTimer += Time.deltaTime;
        m_regenTime += Time.deltaTime;
        m_obstacleTime += Time.deltaTime;
        m_helperTime += Time.deltaTime;
        SetStat();

        if (humanCount < 3)
        {
            m_regenTime += 1;
        }
        if (m_regenTime > m_reTime)
        {
            m_regenTime = 0;
            AddHuman(1);
        }
        if(m_startTimer > GameDatabase.Instance().GetTimeData(m_stageNumber).limitTime)
        {
            if (m_stageNumber < 10)
            {
                m_stageNumber++;
            }
            StageStart();
            m_startTimer = 0;
        }
        if(m_obstacleTime > GameDatabase.Instance().GetTimeData(m_stageNumber).obstacleCreateTime)
        {
            AddObstacle();
            m_obstacleTime = 0;
        }
        if(m_helperTime > GameDatabase.Instance().GetTimeData(m_stageNumber).helperCreateTime)
        {
            AddHelper();
            m_helperTime = 0;
        }
    }
    void StageStart()
    {
        m_startTimer = 0;
        m_nextTime = GameDatabase.Instance().GetTimeData(m_stageNumber).limitTime;
        m_reTime = GameDatabase.Instance().GetTimeData(m_stageNumber).humanCreateTime;
        AddHuman(10);
        //AddObstacle();
        //AddHelper();
    }
    void SetStat()
    {
        // x = atk, y = speed
        float power = UICtrl.currentTime / UICtrl.maxTime;
        m_sunAtk = 9f * (1f - power);
        m_earthSpeed = 0.01f + 1.5f * (1f - power);
        //Vector3 stat = Vector3.Lerp(new Vector3(1f,2f),new Vector3(8f,0.01f) ,power);
        //m_sunAtk = stat.x;
        //m_earthSpeed = stat.y;
        //Debug.Log("power:" + power);
        //Debug.Log(stat);
    }
    void AddHuman(int num)
    {
        for (int i = 0; i < num; i++)
        {
            if (humanCount < 21)
            {
                GameObject human = m_humanPool.GetPooledObject();
                human.GetComponent<Human>().Init();
                humanCount++;
            }
        }
    }
    void AddObstacle()
    {
        GameObject obstacle = m_obstaclePool.GetPooledObject();
        obstacle.GetComponent<Obstacle>().Init();
    }
    void AddHelper()
    {
        GameObject obstacle = m_helperPool.GetPooledObject();
        obstacle.GetComponent<Helper>().Init();
    }
    public void GameOver()
    {
        if (!isGameover)
        {
            GameObject.Find("ScoreText").GetComponent<Text>().text = "Your Score : " + m_killCount;
            isGameover = true;
            deadCount++;
            //        m_stageNumber = 0;
            m_killCount = 0;
            gameoverGB = GameObject.Find("GameOverText");
            gameoverGB.GetComponent<Animator>().SetBool("Gameover", true);
        }
    }

    public void Restart()
    {
        gameoverGB = GameObject.Find("GameOverText");
        gameoverGB.GetComponent<Animator>().SetBool("Gameover", false);
        if (deadCount >= 4)
        {
            //GameObject.Find("AdsManager").GetComponent<UnityAdsManager>().ShowRewardedAd();
            //deadCount = 0;
        }
        m_stageNumber = 1;
        isGameover = false;
        SceneManager.LoadScene("PlayTest");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
