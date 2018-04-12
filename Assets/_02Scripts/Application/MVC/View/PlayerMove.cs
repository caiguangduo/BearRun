using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : View
{
    const float grivaty = 9.8f;
    const float m_moveSpeed = 13;
    const float m_jumpValue = 5;
    const float m_maxSpeed = 40;
    const float m_speedAddDis = 200;
    const float m_speedAddRate = 0.5f;

    public override string M_Name { get { return Consts.V_PlayerMove; } }

    private CharacterController m_cc;
    private CharacterController M_Cc
    {
        get
        {
            if (m_cc == null)
            {
                m_cc = GetComponent<CharacterController>();
            }
            return m_cc;
        }
    }

    private InputDirection m_inputDir = InputDirection.Null;
    private bool m_activeInput = false;
    private Vector3 m_mousePos;

    private float m_targetX = 0;
    private float M_TargetX
    {
        get
        {
            return m_targetX;
        }
        set
        {
            if (value < -2)
            {
                value = -2;
            }
            else if (value > 2)
            {
                value = 2;
            }
            m_targetX = value;
        }
    }

    float xPos = 0;

    private bool m_isSlide = false;
    private float m_slideTime;

    private float m_yDistance;

    private float m_speed = 20;
    public float M_Speed
    {
        get
        {
            return m_speed;
        }
        set
        {
            m_speed = value;
            if (m_speed > m_maxSpeed)
            {
                m_speed = m_maxSpeed;
            }
        }
    }

    private bool m_isInvincible = false;
    private bool m_isHit = false;
    private float m_maskSpeed;
    private float m_addRate = 10;

    private bool m_isGoal = false;
    private int m_nowIndex = 1;

    private int m_DoubleTime = 1;
    private int m_skillTime
    {
        get
        {
            return Game.M_Instance.M_GM.M_SkillTime;
        }
    }

    private IEnumerator m_multiplyCoroutine;
    private IEnumerator m_magnetCoroutine;

    public GameObject m_magnetColliderObj;

    private IEnumerator m_invincibleCoroutine;

    private GameObject m_trail;
    
    private GameObject m_ball;
    private GameObject M_Ball
    {
        get
        {
            if (m_ball == null)
            {
                m_ball = transform.Find("Ball").gameObject;
            }
            return m_ball;
        }
    }
    private IEnumerator m_goalCoroutine;

    public GameObject m_qiuWang;
    public GameObject m_jiaSu;
    IEnumerator m_showHideNet;
    IEnumerator m_showHideJiaSu;

    public SkinnedMeshRenderer M_PlayerSkin;
    public MeshRenderer M_BallSkin;

    public override void RegisterAttentionEvent()
    {
        M_AttentionList.Add(Consts.E_ClickGoalButtonAttention);
    }
    public override void HandleEvent(string name, object data)
    {
        switch (name)
        {
            case Consts.E_ClickGoalButtonAttention:
                OnGoalClick();
                break;
        }
    }
    public void OnGoalClick()
    {
        if (m_goalCoroutine != null)
            StopCoroutine(m_goalCoroutine);
        SendMessage("MessagePlayShoot");
        m_trail.SetActive(true);
        M_Ball.SetActive(false);
        m_goalCoroutine = MoveBall();
        StartCoroutine(m_goalCoroutine);
    }
    IEnumerator MoveBall()
    {
        while (true)
        {
            if (!Game.M_Instance.M_GM.M_IsPause && Game.M_Instance.M_GM.M_IsPlay)
            {
                m_trail.transform.Translate(transform.forward * 40 * Time.deltaTime);
            }
            yield return null;
        }
    }
    public void HitBallDoor()
    {
        StopCoroutine(m_goalCoroutine);
        m_trail.transform.localPosition = new Vector3(0, 1.62f, 6.28f);
        m_trail.SetActive(false);
        M_Ball.SetActive(true);

        m_isGoal = true;

        GameObject go = Game.M_Instance.M_ObjectPool.Spawn(Consts.O_FX_GOAL);
        go.transform.SetParent(m_trail.gameObject.transform.parent);
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Goal);

        MVC.SendEvent(Consts.E_ShootGoalAttention);
    }

    private void Awake()
    {
        m_trail = GameObject.Find("trail").gameObject;
        m_trail.SetActive(false);

        M_PlayerSkin.material = Game.M_Instance.M_StaticData.GetClothInfo(Game.M_Instance.M_GM.M_TakeOnSkin, Game.M_Instance.M_GM.M_TakeOnCloth).M_Material;
        M_BallSkin.material = Game.M_Instance.M_StaticData.GetFootballInfo(Game.M_Instance.M_GM.M_TakeOnFootball).M_Material;
    }
    private void Start()
    {
        StartCoroutine(UpdateAction());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Tag_SmallFence)
        {
            if (m_isInvincible) return;

            other.gameObject.SendMessage("HitPlayer", transform.position);
            Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Hit);
            HitObstacles();
        }
        else if (other.gameObject.tag == Tag.Tag_BigFence)
        {
            if (m_isInvincible) return;
            if (m_isSlide) return;

            other.gameObject.SendMessage("HitPlayer", transform.position);
            Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Hit);
            HitObstacles();
        }
        else if (other.gameObject.tag == Tag.Tag_Block)
        {
            if (m_isInvincible) return;

            Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_End);
            other.gameObject.SendMessage("HitPlayer", transform.position);

            MVC.SendEvent(Consts.E_EndGameController);
        }
        else if (other.gameObject.tag == Tag.Tag_SmallBlock)
        {
            if (m_isInvincible) return;

            Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_End);
            other.transform.parent.parent.SendMessage("HitPlayer", transform.position);

            MVC.SendEvent(Consts.E_EndGameController);
        }
        else if (other.gameObject.tag == Tag.Tag_BeforeTrigger)
        {
            other.transform.parent.SendMessage("HitTrigger", SendMessageOptions.RequireReceiver);
        }
        else if (other.gameObject.tag == Tag.Tag_BeforeGoalTrigger)
        {
            MVC.SendEvent(Consts.E_HitGoalTriggerAttention);
            if (m_showHideJiaSu != null)
            {
                StopCoroutine(m_showHideJiaSu);
            }
            m_showHideJiaSu = ShowHideJiaSu();
            StartCoroutine(m_showHideJiaSu);
        }
        else if (other.gameObject.tag == Tag.Tag_GoalKeeper)
        {
            HitObstacles();
            other.transform.parent.parent.parent.SendMessage("HitGoalKeeper", SendMessageOptions.RequireReceiver);
        }
        else if (other.gameObject.tag == Tag.Tag_BallDoor)
        {
            if (m_isGoal)
            {
                m_isGoal = false;
                return;
            }
            HitObstacles();
            if (m_showHideNet != null)
            {
                StopCoroutine(m_showHideNet);
            }
            m_showHideNet = ShowHideNet();
            StartCoroutine(m_showHideNet);

            other.transform.parent.parent.SendMessage("HitDoor", m_nowIndex);
        }
    }

    IEnumerator ShowHideNet()
    {
        m_qiuWang.SetActive(true);
        yield return new WaitForSeconds(1);
        m_qiuWang.SetActive(false);
    }
    IEnumerator ShowHideJiaSu()
    {
        m_jiaSu.SetActive(true);
        yield return new WaitForSeconds(1);
        m_jiaSu.SetActive(false);
    }

    public void HitObstacles()
    {
        if (m_isHit) return;
        m_isHit = true;
        m_maskSpeed = M_Speed;
        M_Speed = 0;
        StartCoroutine(DecreaseSpeed());
    }
    IEnumerator DecreaseSpeed()
    {
        while (M_Speed <= m_maskSpeed)
        {
            M_Speed += Time.deltaTime * m_addRate;
            yield return 0;
        }
        m_isHit = false;
    }

    #region 移动控制
    IEnumerator UpdateAction()
    {
        while (true)
        {
            if (!Game.M_Instance.M_GM.M_IsPause && Game.M_Instance.M_GM.M_IsPlay)
            {
                UpdateDis();

                UpdateMoveForwardAndJumpState();

                GetInputDirection();
                UpdateTargetState();
                MoveControl();

                UpdateSlide();
                UpdateSpeed();
            }
            yield return 0;
        }
    }
    
    void UpdateDis()
    {
        DistanceArgs e = new DistanceArgs() { M_Distance = (int)transform.position.z };
        MVC.SendEvent(Consts.E_UpdateDisAttention, e);
    }

    void UpdateMoveForwardAndJumpState()
    {
        if (!M_Cc.isGrounded)
            m_yDistance -= grivaty * Time.deltaTime;
        if (Mathf.Abs(m_yDistance) < 0.1f)
            m_yDistance = 0;
        M_Cc.Move((transform.forward * M_Speed + new Vector3(0, m_yDistance, 0)) * Time.deltaTime);
    }

    void GetInputDirection()
    {
        //手势识别
        m_inputDir = InputDirection.Null;
        if (Input.GetMouseButtonDown(0))
        {
            m_activeInput = true;
            m_mousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) && m_activeInput)
        {
            Vector3 dir = Input.mousePosition - m_mousePos;
            if (dir.magnitude > 10)
            {
                if ((Mathf.Abs(dir.x) > Mathf.Abs(dir.y)) && dir.x > 0)
                {
                    m_inputDir = InputDirection.Right;
                }
                else if ((Mathf.Abs(dir.x) > Mathf.Abs(dir.y)) && dir.x < 0)
                {
                    m_inputDir = InputDirection.Left;
                }
                else if ((Mathf.Abs(dir.x) < Mathf.Abs(dir.y)) && dir.y > 0)
                {
                    m_inputDir = InputDirection.Up;
                }
                else if ((Mathf.Abs(dir.x) < Mathf.Abs(dir.y)) && dir.y < 0)
                {
                    m_inputDir = InputDirection.Down;
                }
                m_activeInput = false;
            }
        }

        //键盘识别
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            m_inputDir = InputDirection.Up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            m_inputDir = InputDirection.Down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            m_inputDir = InputDirection.Left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            m_inputDir = InputDirection.Right;
        }
    }

    void UpdateTargetState()
    {
        xPos = transform.position.x;
        switch (m_inputDir)
        {
            case InputDirection.Right:
                if (xPos < 2)
                {
                    M_TargetX += 2;
                    SendMessage("AnimManager", m_inputDir);
                    Game.M_Instance.M_Sound.PlayEffect("Se_UI_Huadong");
                }
                break;
            case InputDirection.Left:
                if (xPos > -2)
                {
                    M_TargetX -= 2;
                    SendMessage("AnimManager", m_inputDir);
                    Game.M_Instance.M_Sound.PlayEffect("Se_UI_Huadong");
                }
                break;
            case InputDirection.Down:
                if (!m_isSlide)
                {
                    m_isSlide = true;
                    m_slideTime = 0.733f;
                    SendMessage("AnimManager", m_inputDir);
                    Game.M_Instance.M_Sound.PlayEffect("Se_UI_Huadong");
                }
                break;
            case InputDirection.Up:
                if (M_Cc.isGrounded)
                {
                    Game.M_Instance.M_Sound.PlayEffect("Se_UI_Jump");
                    m_yDistance = m_jumpValue;
                    SendMessage("AnimManager", m_inputDir);
                }
                break;
        }
    }

    void MoveControl()
    {
        if (xPos != M_TargetX)
        {
            if (xPos > M_TargetX)
                xPos -= m_moveSpeed * Time.deltaTime;
            else
                xPos += m_moveSpeed * Time.deltaTime;
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

            if (Mathf.Abs(xPos - M_TargetX) < 0.1f)
            {
                transform.position = new Vector3(M_TargetX, transform.position.y, transform.position.z);
                if (M_TargetX == -2)
                {
                    m_nowIndex = 0;
                }
                else if (M_TargetX == 0)
                {
                    m_nowIndex = 1;
                }
                else if (M_TargetX == 2)
                {
                    m_nowIndex = 2;
                }
            }
        }
    }
    
    void UpdateSlide()
    {
        if (m_isSlide)
        {
            m_slideTime -= Time.deltaTime;
            if (m_slideTime < 0)
            {
                m_isSlide = false;
                m_slideTime = 0;
            }
        }
    }

    float m_lastZDis = 0;
    void UpdateSpeed()
    {
        if ((transform.position.z-m_lastZDis) >= m_speedAddDis)
        {
            m_lastZDis += m_speedAddDis;

            if (m_isHit)
                m_maskSpeed += m_speedAddRate;
            else
                M_Speed += m_speedAddRate;
        }
    }
    #endregion

    public void HitCoin()
    {
        CoinArgs e = new CoinArgs
        {
            M_Coin = m_DoubleTime
        };
        MVC.SendEvent(Consts.E_UpdateCoinAttention, e);
    }
    public void HitItem(ItemKind item)
    {
        ItemArgs e = new ItemArgs
        {
            M_Kind = item,
            M_HitCount = 0
        };
        MVC.SendEvent(Consts.E_HitItemController, e);
    }

    public void HitMultiply()
    {
        if (m_multiplyCoroutine != null)
            StopCoroutine(m_multiplyCoroutine);
        m_multiplyCoroutine = MultiplyCoroutine();
        StartCoroutine(m_multiplyCoroutine);
    }
    IEnumerator MultiplyCoroutine()
    {
        m_DoubleTime = 2;
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (Game.M_Instance.M_GM.M_IsPlay && !Game.M_Instance.M_GM.M_IsPause)
            {
                timer -= Time.deltaTime;
            }
            yield return null;
        }
        m_DoubleTime = 1;
    }

    public void HitMagnet()
    {
        if (m_magnetCoroutine != null)
        {
            StopCoroutine(m_magnetCoroutine);
        }
        m_magnetCoroutine = MagnetCoroutine();
        StartCoroutine(m_magnetCoroutine);
    }
    IEnumerator MagnetCoroutine()
    {
        m_magnetColliderObj.SetActive(true);
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (Game.M_Instance.M_GM.M_IsPlay && !Game.M_Instance.M_GM.M_IsPause)
            {
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        m_magnetColliderObj.SetActive(false);
    }

    public void HitAddTime()
    {
        MVC.SendEvent(Consts.E_HitAddTimeAttention);
    }

    public void HitInvincible()
    {
        if (m_invincibleCoroutine != null)
        {
            StopCoroutine(m_invincibleCoroutine);
        }
        m_invincibleCoroutine = InvincibleCoroutine();
        StartCoroutine(m_invincibleCoroutine);
    }
    IEnumerator InvincibleCoroutine()
    {
        m_isInvincible = true;
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (Game.M_Instance.M_GM.M_IsPlay && !Game.M_Instance.M_GM.M_IsPause)
            {
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        m_isInvincible = false;
    }
}
