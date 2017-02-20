using UnityEngine;
using System.Collections;
using HumanStates;
using System;

public class Human : MonoBehaviour {

    private StateMachine<Human> m_stateMachine = new StateMachine<Human>();

    [SerializeField]
    private Transform earthCenter;
    [SerializeField]
    private float distance;

    public float humanAngleValue;
    public float earthAngleValue;
    public float angleValue;

    private float m_hp;
    private float m_maxHp;
    private float m_regenHp;
    private Animator m_anim;

    public bool isDamaged = false;
    private bool isDie = false;

    private Rigidbody earthRigid;
    private SkinnedMeshRenderer m_mesh;
    public GameObject m_directionObject;

    public GameObject m_plusObject;
    public GameObject m_minusObject;
    public GameObject m_dieEffect;


    public void ChangeState(State<Human> state)
    {
        m_stateMachine.ChangeState(state);
    }

    void Awake()
    {

    }
    void Start()
    {
    }
    public void Init()
    {
        m_anim = GetComponentInChildren<Animator>();
        m_mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        earthRigid = GameObject.Find("Earth").GetComponent<Rigidbody>();
        earthCenter = GameObject.Find("Earth").transform;
        distance = Vector3.Distance(earthCenter.position, transform.position);
        var n = new Normal();
        m_stateMachine.Init(this, n);
        m_maxHp = GameDatabase.Instance().GetTimeData(GameManager.instance.m_stageNumber).humanHp;
        m_regenHp = GameDatabase.Instance().GetTimeData(GameManager.instance.m_stageNumber).humanRegen;
        m_hp = m_maxHp;
        humanAngleValue = UnityEngine.Random.Range(0f, 360f);
        gameObject.SetActive(true);
        SetPosition();
    }
    void Update()
    {
        if (GameManager.isGameover)
            return;
        m_stateMachine.Update();
        m_mesh.material.color = Color.Lerp(Color.white, Color.red, 1 - m_hp / m_maxHp);
    }
    void SetPosition()
    {
        earthAngleValue += earthRigid.angularVelocity.z;
        angleValue = earthAngleValue + humanAngleValue;
        transform.position = new Vector3((float)Math.Cos(angleValue * Math.PI / 180) * distance, (float)Math.Sin(angleValue * Math.PI / 180) * distance, 0f) + earthCenter.position;
        transform.rotation = Quaternion.AngleAxis(angleValue - 90, new Vector3(0, 0, 1));
    }
    public void SetAnimation(string param, bool value)
    {
        m_anim.SetBool(param, value);
    }
    void FixedUpdate()
    {
        if (GameManager.isGameover)
            return;
        SetPosition();

        if (isDie) return;
        CheckDamaged();

        if(isDamaged)
        {
            m_hp -= Time.deltaTime * GameManager.instance.m_sunAtk;
            if (m_hp < 0 )
            {
                isDie = true;
                StartCoroutine(Die());
            }
        }
        else
        {
            m_hp += Time.deltaTime * GameDatabase.Instance().GetTimeData(GameManager.instance.m_stageNumber).humanRegen;
            if (m_hp > m_maxHp)
            {
                m_hp = m_maxHp;
            }
        }
    }
    void CheckDamaged()
    {
        if (angleValue > 360)
        {
            angleValue -= 360;
        }
        else if (angleValue < 0)
        {
            angleValue += 360;
        }
        if (humanAngleValue > 360)
        {
            humanAngleValue -= 360;
        }
        else if (humanAngleValue < 0)
        {
            humanAngleValue += 360;
        }
        if (earthAngleValue > 360)
        {
            earthAngleValue -= 360;
        }
        else if (earthAngleValue < 0)
        {
            earthAngleValue += 360;
        }

        if (angleValue  >= GameManager.instance.m_sunRStart && angleValue <= GameManager.instance.m_sunREnd)
        {
            isDamaged = true;
        }
        else
        {
            isDamaged = false;
        }
    }
    IEnumerator Die()
    {
        ChangeState(new Normal());
        GameManager.instance.humanCount--;
        SoundManager.Instance.ScreamSound();
        GameManager.instance.m_killCount++;
        m_mesh.material.color = Color.white;
        SetAnimation("isDie", true);
        UICtrl.currentTime += 0.5f;
        yield return new WaitForSeconds(1f);
        isDie = false;
        gameObject.SetActive(false);
        humanAngleValue = 0;
        earthAngleValue = 0;
        angleValue = 0;
        m_hp = m_maxHp;
        SetAnimation("isDie", false);
        SetAnimation("isEmer", false);
        
        m_dieEffect.SetActive(false);
    }
}
