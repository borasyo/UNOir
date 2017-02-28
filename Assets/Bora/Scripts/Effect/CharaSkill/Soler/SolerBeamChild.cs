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

    bool m_IsUp = true;

    [SerializeField] float m_fShakeTime_Sec = 0.1f;
    float m_fNowShake = 0.0f;       //  移動量
    float m_fNowShakeAmount = 0.0f; //  現在位置から計算した移動量
    [SerializeField] float m_fMaxShake = 0.6f;

    Vector3 m_TargetPos = Vector3.zero;
    Vector3 m_InitScale = Vector3.zero;
    float m_fInitHeight = 0.0f;

    // Use this for initialization
    void Start ()
    {
        m_TargetPos = transform.position;
        m_TargetPos.x *= -1;

        m_Distance = m_TargetPos - transform.position;

        m_fInitHeight = transform.position.y;
        m_InitScale = transform.localScale;

        m_fNowShakeAmount = m_fNowShake = Random.Range (0.0f, m_fMaxShake);
        transform.parent = null;
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
            transform.position += new Vector3 (0, m_fNowShakeAmount * (Time.deltaTime / m_fShakeTime_Sec), 0);

            if (m_fInitHeight + m_fNowShake <= transform.position.y) {
                m_IsUp = false;
                m_fNowShake = -Random.Range (0.0f, m_fMaxShake);
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
}
