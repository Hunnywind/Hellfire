using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputCtrl : MonoBehaviour
{

    public bool isLeft;
    public static float rotateSpeed;
    public static float force;

    private Rigidbody earthRigid;
    private Image m_leftGuide;
    private Image m_rightGuide;


    void Awake()
    {
        earthRigid = GameObject.Find("Earth").GetComponent<Rigidbody>();
        m_leftGuide = GameObject.Find("LeftButtonGuide").GetComponent<Image>();
        m_rightGuide = GameObject.Find("RightButtonGuide").GetComponent<Image>();
    }
    void Start()
    {
        rotateSpeed = GameDatabase.Instance().GetInitData("RotationAcceleMove");
        //rotateSpeed = 2f;
    }

    void OnMouseDrag()
    {
        if (GameManager.isGameover)
            return;

        if (isLeft)
        {
            earthRigid.AddTorque(0f, 0f, GameManager.instance.m_earthSpeed);
            m_leftGuide.color = Color.yellow;
            m_rightGuide.color = Color.white;
        }
        else
        {
            earthRigid.AddTorque(0f, 0f, -GameManager.instance.m_earthSpeed);
            m_leftGuide.color = Color.white;
            m_rightGuide.color = Color.yellow;
        }
    }

    public void OnMouseUp()
    {
        m_leftGuide.color = Color.white;
        m_rightGuide.color = Color.white;
    }
}
