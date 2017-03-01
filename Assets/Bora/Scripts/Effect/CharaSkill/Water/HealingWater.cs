using UnityEngine;
using System.Collections;

public class HealingWater : EffectBase
{
    /// <summary>
    /// 概要 : キャラスキル回復のエフェクト進行管理
    /// Author : 大洞祥太
    /// </summary>

    CharaSkillBase m_SkillBase = null;
    ParticleSystem m_NextParticle = null;
    SpriteRenderer m_SpriteRender = null;

    float m_fNowTime_Sec = 0.0f;
    [SerializeField] float m_fTime_Sec = 0.5f;
    [SerializeField] Vector3 m_TargetPos = new Vector3 (0, 0.5f, 0);

    Vector3 m_Distance = Vector3.zero;
    Vector3 m_InitScale = Vector3.zero;

    [SerializeField] [Range (0.0f, 1.0f)]
    float m_fAttenTiming = 0.75f;       //  減衰を開始させるタイミング

    [SerializeField] [Range (0.0f, 1.0f)]
    float m_fNextEffectTiming = 0.9f;   //  次のエフェクトを再生させるタイミング

    // Use this for initialization
    void Start ()
    {
        m_SpriteRender = GetComponent<SpriteRenderer> ();
        m_NextParticle = GetComponentInChildren<ParticleSystem> ();

        m_Distance = m_TargetPos - transform.position;
        m_InitScale = transform.localScale;
        m_NextParticle.Stop ();
	
        m_fTime_Sec += Random.Range (-0.2f, 0.2f);

        transform.DetachChildren ();
        SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_HEELWATER);
    }

    // Update is called once per frame
    void Update ()
    {
        if (EndCheck ())
            return;

        m_fNowTime_Sec += Time.deltaTime;

        transform.position += m_Distance * (Time.deltaTime / m_fTime_Sec);

        // タイミングになったらスケールを減衰
        if (m_fNowTime_Sec >= m_fTime_Sec * m_fAttenTiming) {
            float fAttenTime = m_fTime_Sec - (m_fTime_Sec * m_fAttenTiming); // 減衰量を徐々に減らす
            transform.localScale -= m_InitScale * (Time.deltaTime / fAttenTime);
        }

        NextParticleCheck ();
    }

    void NextParticleCheck() 
    {
        if (m_NextParticle.isPlaying)
            return;
        
        m_NextParticle.transform.position = transform.position;

        // タイミングをチェック
        if (m_fNowTime_Sec < m_fTime_Sec * m_fNextEffectTiming)
            return;
        
        m_NextParticle.Play ();
    }

    bool EndCheck ()
    {
        if (transform.position.y <= m_TargetPos.y) {
            m_SpriteRender.enabled = false;
            bEnd = true;
        }

        if (!bEnd || m_NextParticle.isPlaying)
            return false;
        
        m_SkillBase.Run ();
        Destroy (m_NextParticle.gameObject);
        Destroy (this.gameObject);
        return true;
    }

    public override void Set (CharaSkillBase skillData)
    {
        m_SkillBase = skillData;
    }
}
