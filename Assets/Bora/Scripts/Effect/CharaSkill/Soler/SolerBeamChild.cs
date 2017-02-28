using UnityEngine;
using System.Collections;

public class SolerBeamChild : MonoBehaviour
{

    /// <summary>
    /// 概要 : 
    /// Author : 大洞祥太
    /// </summary>

    public float m_fTime_Sec = 1.0f;
    Vector3 m_Distance = Vector3.zero;

    float m_fInitHeight = 0.0f;
    public bool m_IsUp = true;
    public float m_fShakeTime = 0.1f;
    float m_fNowShake = 0.0f;
    float m_fNowShakeAmount = 0.0f;
    public float m_fShake = 0.6f;
    Vector3 m_TargetPos = Vector3.zero;
    Vector3 m_InitScale = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        m_TargetPos = transform.position;
        m_TargetPos.x *= -1;
        m_Distance = m_TargetPos - transform.position;
        m_fInitHeight = transform.position.y;
        m_IsUp = true;
        m_fNowShakeAmount = m_fNowShake = Random.Range (0.0f, m_fShake);
        transform.parent = null;
        m_InitScale = transform.localScale;
    }

    // Update is called once per frame
    void Update ()
    {
        transform.position += m_Distance * (Time.deltaTime / m_fTime_Sec);
        transform.localScale -= m_InitScale * (Time.deltaTime / m_fTime_Sec);

        if (m_TargetPos.x >= transform.position.x) {
            Destroy (this.gameObject);
        }

        if (m_IsUp) {
            transform.position += new Vector3 (0, m_fNowShakeAmount * (Time.deltaTime / m_fShakeTime), 0);

            if (m_fInitHeight + m_fNowShake <= transform.position.y) {
                m_IsUp = false;
                m_fNowShake = -Random.Range (0.0f, m_fShake);
                m_fNowShakeAmount = transform.position.y - (m_fInitHeight + m_fNowShake);
            }
        } else {
            transform.position -= new Vector3 (0, m_fNowShakeAmount * (Time.deltaTime / m_fShakeTime), 0);

            if (m_fInitHeight + m_fNowShake >= transform.position.y) {
                m_IsUp = true;
                m_fNowShake = Random.Range (0.0f, m_fShake);
                m_fNowShakeAmount = (m_fInitHeight + m_fNowShake) - transform.position.y;
            }
        }
    }
}
