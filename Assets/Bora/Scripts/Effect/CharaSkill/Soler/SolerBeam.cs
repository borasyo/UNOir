using UnityEngine;
using System.Collections;

public class SolerBeam : EffectBase
{
    /// <summary>
    /// 概要 : 
    /// Author : 大洞祥太
    /// </summary>

    CharaSkillBase m_SkillBase = null;

    [SerializeField] float m_fTime_Sec = 1.0f;
    Vector3 m_Distance = Vector3.zero;

    bool m_IsUp = true;

    float m_fNowShake = 0.0f;
    float m_fNowShakeAmount = 0.0f;
    [SerializeField] float m_fShakeTime_Sec = 0.1f;
    [SerializeField] float m_fMaxShake = 0.6f;

    [SerializeField] Vector3 m_TargetPos = Vector3.zero;
    float m_fInitScale  = 0.0f;
    float m_fInitHeight = 0.0f;

    // Use this for initialization
    void Start ()
    {
        transform.position += new Vector3 (0, Random.Range (-0.5f, 0.5f), 0);

        m_Distance = m_TargetPos - transform.position;
        m_fInitHeight = transform.position.y;
        m_IsUp = true;
        m_fNowShakeAmount = m_fNowShake = Random.Range (0.0f, m_fMaxShake);
        m_fInitScale = transform.localScale.y;

        SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SOLERBEAM);
    }

    // Update is called once per frame
    void Update ()
    {
        transform.position += m_Distance * (Time.deltaTime / m_fTime_Sec);
        transform.localScale -= new Vector3 (0, m_fInitScale, 0) * (Time.deltaTime / m_fTime_Sec);

        if (m_TargetPos.x >= transform.position.x) {
            bEnd = true;
            m_SkillBase.Run ();
            Destroy (this.gameObject);
        }

        if (m_IsUp) {
            transform.position += new Vector3 (0, m_fNowShakeAmount * (Time.deltaTime / m_fShakeTime_Sec), 0);

            if (m_fInitHeight + m_fNowShake <= transform.position.y) {
                m_IsUp = false;
                m_fNowShake = Random.Range (-m_fMaxShake, 0.0f);
                m_fNowShakeAmount = transform.position.y - (m_fInitHeight + m_fNowShake);
            }
        } else {
            transform.position -= new Vector3 (0, m_fNowShakeAmount * (Time.deltaTime / m_fShakeTime_Sec), 0);

            if (m_fInitHeight + m_fNowShake >= transform.position.y) {
                m_IsUp = true;
                m_fNowShake = Random.Range (0.0f, m_fMaxShake);
                m_fNowShakeAmount = (m_fInitHeight + m_fNowShake) - transform.position.y;
            }
        }
    }

    public override void Set (CharaSkillBase skillData)
    {
        m_SkillBase = skillData;
    }
}
