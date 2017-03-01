using UnityEngine;
using System.Collections;

public class WhirlWind : EffectBase
{
    /// <summary>
    /// 概要 : 
    /// Author : 大洞祥太
    /// </summary>

    SpriteRenderer m_SpriteRender = null;
    Vector3 m_Distance = Vector3.zero;

    [SerializeField] float m_fTime_Sec = 0.75f;
    public float Time_Sec { get { return m_fTime_Sec; } set { m_fTime_Sec = value; } }
    [SerializeField] Vector3 m_TargetPos = Vector3.zero;
	
    // Use this for initialization
    void Awake ()
    {
        m_SpriteRender = GetComponent<SpriteRenderer> ();
        m_Distance = m_TargetPos - transform.position;

        this.enabled = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if (bEnd) {
            this.enabled = false;
            return;
        }

        m_SpriteRender.color -= new Color (0, 0, 0, 1.0f * (Time.deltaTime / m_fTime_Sec));
        transform.position += m_Distance * (Time.deltaTime / m_fTime_Sec);

        if (m_SpriteRender.color.a <= 0.0f)
            bEnd = true;
    }

    public void Run ()
    {
        this.enabled = true;
    }
}
