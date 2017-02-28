using UnityEngine;
using System.Collections;

public class SunProtect : EffectBase
{
    /// <summary>
    /// 概要 : ハレのスキルェフェクトを管理
    /// Author : 大洞祥太
    /// </summary>

    static bool m_IsUse = false;    //  2つ以上エフェクトを生成させないため

    SkillRise m_SkillBase = null;
    SpriteRenderer m_SpriteRender = null;

    [SerializeField] float m_fStartTime_Sec = 1.0f;

    [SerializeField] float m_fRotTime_Sec = 30.0f;      //  回転スピード
    [SerializeField] float m_fAtten_Sec = 0.25f;        //  減衰スピード
    [SerializeField] float m_fRivisionAlpha = 0.25f;    //  

    void Start ()
    {
        m_SpriteRender = GetComponent<SpriteRenderer> ();
        ColorInit ();
        m_SpriteRender.color = new Color (1, 1, 1, 1);
    }

    IEnumerator ColorInit() {

        transform.eulerAngles += new Vector3 (0, 0, 360 * (Time.deltaTime / m_fRotTime_Sec));
        m_SpriteRender.color += new Color (0, 0, 0, 1.0f * (Time.deltaTime / m_fStartTime_Sec));
        yield return m_SpriteRender.color.a >= 1.0f;
    }

    void Update ()
    {
        Vector3 RotAmount = new Vector3 (0, 0, 360 * (Time.deltaTime / m_fRotTime_Sec));
        transform.eulerAngles += RotAmount;

        float fNowAlpha = ((1.0f - m_fRivisionAlpha) * (float)SkillRise.GetStock / (float)m_SkillBase.GetMaxNum) + m_fRivisionAlpha;
        if (m_SpriteRender.color.a > fNowAlpha) {
            m_SpriteRender.color -= new Color (0, 0, 0, 1.0f * (Time.deltaTime / m_fAtten_Sec));

            if (m_SpriteRender.color.a < fNowAlpha) {
                m_SpriteRender.color = new Color (1, 1, 1, fNowAlpha);
            }
        } else if (m_SpriteRender.color.a < fNowAlpha) {
            m_SpriteRender.color += new Color (0, 0, 0, 1.0f * (Time.deltaTime / m_fAtten_Sec));

            if (m_SpriteRender.color.a > fNowAlpha) {
                m_SpriteRender.color = new Color (1, 1, 1, fNowAlpha);
            }
        }

        DestroyCheck ();
    }

    // 終了判定
    void DestroyCheck() {

        if (SkillRise.GetStock <= 0) {
            m_SpriteRender.color -= new Color (0, 0, 0, m_fRivisionAlpha * (Time.deltaTime / m_fAtten_Sec)); 
            if (m_SpriteRender.color.a <= 0.0f) {
                m_IsUse = false;
                Destroy (this.gameObject);
            }
        }

        // 勝ちなら強制終了
        if (ResultManager.Instance.bWin) {
            m_IsUse = false;
            Destroy (this.gameObject);
        }
    }

    public override void Set (CharaSkillBase skillData)
    {
        m_SkillBase = (SkillRise)skillData;
        m_SkillBase.Run ();

        // 2つ以上はオブジェクトは生成しない
        if (m_IsUse) {
            Destroy (this.gameObject);
            return;
        } 

        m_IsUse = true;
        SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SUN);
    }
}
