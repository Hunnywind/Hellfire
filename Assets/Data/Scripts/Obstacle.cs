using UnityEngine;
using System.Collections;
using System;

public class Obstacle : MonoBehaviour {
    public bool isDamaged = false;
    private bool isDie = false;

    [SerializeField]
    private Transform earthCenter;
    private Rigidbody earthRigid;
    private float m_hp;
    private SkinnedMeshRenderer m_mesh;
    public GameObject m_directionObject;

    public GameObject m_plusObject;
    public GameObject m_minusObject;
    public GameObject m_dieEffect;
    private Animator m_anim;

    public float humanAngleValue;
    public float earthAngleValue;
    public float angleValue;
    private float distance;

    private float m_lifecircleTime;

    void Awake()
    {
        m_mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        m_mesh.material.color = Color.blue;
    }
    public void Init()
    {
        m_anim = GetComponentInChildren<Animator>();
        earthRigid = GameObject.Find("Earth").GetComponent<Rigidbody>();
        earthCenter = GameObject.Find("Earth").transform;
        distance = Vector3.Distance(earthCenter.position, transform.position);
        m_hp = 1f;
        m_lifecircleTime = 0f;
        //humanAngleValue = UnityEngine.Random.Range(0f, 360f);
        float a = UnityEngine.Random.Range(0, 2) == 0 ? UnityEngine.Random.Range(0f, 60f) : UnityEngine.Random.Range(120f, 360f);
        a -= earthRigid.angularVelocity.z;
        humanAngleValue = a;
        gameObject.SetActive(true);
        SetPosition();
    }
    void FixedUpdate()
    {
        if (GameManager.isGameover)
            return;
        SetPosition();
        if (isDie) return;
        if (isDamaged)
        {
            m_hp -= Time.deltaTime * 4f;
            if (m_hp < 0)
            {
                isDie = true;
                StartCoroutine(Penalty());
            }
        }
        m_lifecircleTime += Time.deltaTime;
        if(m_lifecircleTime > 10f)
        {
            ObstacleClear();
        }
        CheckDamaged();
    }
    void SetPosition()
    {
        earthAngleValue += earthRigid.angularVelocity.z;
        angleValue = earthAngleValue + humanAngleValue;
        transform.position = new Vector3((float)Math.Cos(angleValue * Math.PI / 180) * distance, (float)Math.Sin(angleValue * Math.PI / 180) * distance, 0f) + earthCenter.position;
        transform.rotation = Quaternion.AngleAxis(angleValue - 90, new Vector3(0, 0, 1));
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

        if (angleValue >= GameManager.instance.m_sunRStart && angleValue <= GameManager.instance.m_sunREnd)
        {
            SetAnimation("isEmer", true);
            isDamaged = true;
        }
        else
        {
            SetAnimation("isEmer", false);
            isDamaged = false;
        }
    }
    void ObstacleClear()
    {
        gameObject.SetActive(false);
        humanAngleValue = 0;
        earthAngleValue = 0;
        angleValue = 0;
    }
    IEnumerator Penalty()
    {
        SoundManager.Instance.ScreamSound();
        SetAnimation("isDie", true);
        UICtrl.currentTime -= 7f;
        GameManager.instance.m_comboCount = 0;
        yield return new WaitForSeconds(1f);
        isDie = false;
        gameObject.SetActive(false);
        humanAngleValue = 0;
        earthAngleValue = 0;
        angleValue = 0;
        m_hp = 1f;
        SetAnimation("isDie", false);
        //SetAnimation("isEmer", false);

        m_dieEffect.SetActive(false);
    }
    public void SetAnimation(string param, bool value)
    {
        m_anim.SetBool(param, value);
    }
}
