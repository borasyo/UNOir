using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    /// <summary>
    /// 概要 : 攻撃アップエフェクト
    /// Author : 大洞祥太
    /// </summary>

    UnoData m_UnoData = null;

    SpriteRenderer m_SpriteRender = null;
    Vector3 m_InitPos = Vector3.zero;

    Sprite[] TempSprite = new Sprite[4];

    // Use this for initialization
    void Start ()
    {
        m_InitPos = transform.localPosition;
        m_SpriteRender = GetComponent<SpriteRenderer> ();
        m_UnoData = GetComponentInParent<UnoData> ();
        TempSprite = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_POWERUP);
    }

    // Update is called once per frame
    void Update ()
    {
        bool bNowUse = PowerUpManager.Instance.GetUse (m_UnoData.CardData.m_Color);
        if (!bNowUse || !BattleManager.Instance.GetIsInBattle ()) {
            m_SpriteRender.color = new Color (1, 1, 1, 0);
            return;
        }

        m_SpriteRender.sprite = TempSprite [(int)m_UnoData.CardData.m_Color];

        m_SpriteRender.color = PowerUpAlpha.Instance.NowColor;
        transform.localPosition = m_InitPos - PowerUpAlpha.Instance.NowPos;
    }
}
