using UnityEngine;
using System.Collections;

public class ThunderParticle : MonoBehaviour
{
    /// <summary>
    /// 概要 : 敵の行動遅延中のエフェクト
    /// Author : 大洞祥太
    /// </summary>

    SkillDelay m_Delay = null;
    ParticleSystem m_Particle = null;
    bool m_IsRun = false;

    static bool m_IsUse = false;    //  エフェクトをゲーム中で1つにするため

    // Use this for initialization
    void Start ()
    {
        m_Delay = SkillList.Instance.GetComponentInChildren<SkillDelay> ();
        m_Particle = GetComponent<ParticleSystem> ();
    }
	
    // Update is called once per frame
    void Update ()
    {
        RunCheck ();
      
        if (!m_IsRun || m_Delay.m_IsRun)
            return;

        m_IsUse = false;
        SoundManager.Instance.StopBGM (SoundManager.eBgmValue.BGM_THUNDERNOW);
        Destroy (this.gameObject);
    }

    void RunCheck() {

        if (m_IsRun || !m_Particle.isPlaying)
            return;
       
        m_IsRun = m_Particle.isPlaying;

        // 使用中にする
        if (m_IsUse) {
            Destroy (this.gameObject);
        } else {
            m_IsUse = true;
        }
    }
}
