using UnityEngine;
using System.Collections;

public class Thunder : EffectBase
{
    /// <summary>
    /// 概要 : キャラスキル磁場の進行管理
    /// Author : 大洞祥太
    /// </summary>

    CharaSkillBase m_SkillBase = null;
    ParticleSystem m_StartParticle = null;
    ParticleSystem m_NextParticle = null;
    BlackOut m_BlackOut = null;

    [SerializeField] float m_fFadeTime_Sec = 1.0f;
    [SerializeField] float m_fMaxBlack = 0.5f;

    // Use this for initialization
    void Awake ()
    {
        m_StartParticle = transform.FindChild ("ThunderEffect").GetComponent<ParticleSystem> ();
        m_NextParticle = transform.FindChild ("ThunderParticle").GetComponent<ParticleSystem> ();
        m_BlackOut = GetComponentInChildren<BlackOut> ();

        if (!NullCheck ())
            return;
        
        m_NextParticle.transform.parent = null;
        m_BlackOut.transform.parent = null;
        m_NextParticle.Stop ();

        StartCoroutine (InitEffect());
    }

    bool NullCheck() {

        if (!m_StartParticle) {
            Debug.LogError (m_StartParticle.ToString() + "がありません！");
            return false;
        }

        if (!m_NextParticle) {
            Debug.LogError (m_NextParticle.ToString() + "がありません！");
            return false;
        }

        if (!m_BlackOut) {
            Debug.LogError (m_BlackOut.ToString() + "がありません！");
            return false;
        }

        return true;
    }

    // Update is called once per frame
    void Update ()
    {
        m_BlackOut.Alpha -= m_fMaxBlack * (Time.deltaTime / m_fFadeTime_Sec);

        if (m_BlackOut.Alpha > 0.0f)
            return;
        
        Destroy (m_BlackOut.gameObject); 
        Destroy (this.gameObject); 
    }

    IEnumerator InitEffect() {

        StartCoroutine(StartEffect ());
        this.enabled = false;
        yield return new WaitWhile (() => m_StartParticle.isPlaying);
        this.enabled = true;
        NextEffectRun ();
    }

    IEnumerator StartEffect()
    {
        while(m_StartParticle.isPlaying && m_BlackOut.Alpha < m_fMaxBlack) {

            m_BlackOut.Alpha += m_fMaxBlack * (Time.deltaTime / m_fFadeTime_Sec);

            yield return null;
        }
            
        SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_THUNDER);
    }

    void NextEffectRun()
    {
        Destroy (m_StartParticle.gameObject);
        m_StartParticle = null;
        m_NextParticle.Play ();
        m_SkillBase.Run ();
        SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_THUNDERNOW);
    }

    public override void Set (CharaSkillBase skillData)
    {
        m_SkillBase = skillData;
    }
}
