using UnityEngine;
using System.Collections;

public class WhirlWindManager : EffectBase
{
    /// <summary>
    /// 概要 : 
    /// Author : 大洞祥太
    /// </summary>

    CharaSkillBase m_SkillBase = null;

    int m_nNowEffect = 0;
    WhirlWind[] m_Child = new WhirlWind[4];

    // Use this for initialization
    void Start ()
    {	
        for (int i = 0; i < transform.childCount; i++) {
            m_Child [i] = transform.GetChild (i).GetComponent<WhirlWind> ();
            m_Child [i].Time_Sec += Random.Range (-0.15f, 0.15f);
        }

        // 一つ目を実行
        m_Child [0].Run ();
        SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_WIND);
    }
	
    // Update is called once per frame
    void Update ()
    {
        if (EndCheck ())
            return;
       
        // 現在のエフェクトが終了したかをチェック
        if (!m_Child [m_nNowEffect].bEnd)
            return;

        NextEffect ();
    }

    void NextEffect ()
    {
        m_nNowEffect++;

        // 二つ同時にエフェクトを動かすかの判定をしている
        if (m_nNowEffect >= m_Child.Length / 2) {
            // 同時実行
            m_Child [m_nNowEffect].Run ();
            m_Child [m_nNowEffect + 1].Run ();

            m_nNowEffect++; // 同時に進めたので、もう1つカウントを増やす
        } else {
            m_Child [m_nNowEffect].Run ();
        }
    }

    bool EndCheck ()
    {
        if (m_nNowEffect < m_Child.Length - 1 || !m_Child [m_nNowEffect].bEnd)
            return false;

        m_SkillBase.Run ();
        Destroy (this.gameObject);
        return true;
    }

    public override void Set (CharaSkillBase skillData)
    {
        m_SkillBase = skillData;
    }
}
