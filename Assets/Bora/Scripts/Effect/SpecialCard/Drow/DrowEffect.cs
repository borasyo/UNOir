using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrowEffect : MonoBehaviour
{
    /// <summary>
    /// 概要 : 攻撃アップエフェクト単体
    /// Author : 大洞祥太
    /// </summary>

    UnoData m_UnoData = null;
    SpriteRenderer m_SpriteRender = null;

    static List<Sprite> m_SpriteList = new List<Sprite> ();

    void Awake ()
    {
        if (m_SpriteList.Count > 0)
            return;

        Sprite[] all = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_DROWEFFECT);

        foreach (Sprite sprite in all) {
            m_SpriteList.Add (sprite);
        }
    }

    void Start ()
    {
        m_SpriteRender = GetComponent<SpriteRenderer> ();
        m_SpriteRender.sortingOrder = -2;
        m_UnoData = GetComponentInParent<UnoData> ();
        transform.localScale = new Vector3 (1.1f, 1.1f, 1.0f);
        transform.position = new Vector3 (transform.position.x, transform.position.y, 0.0f);
    }

    void Update ()
    {
        // sprite更新
        m_SpriteRender.sprite = m_SpriteList [(int)m_UnoData.CardData.m_Color];

        if (DrowEffectManager.Instance.GetUse (m_UnoData.CardData.m_Color)) {
            m_SpriteRender.color = new Color (1, 1, 1, DrowAlpha.Instance.Alpha);
        } else {
            m_SpriteRender.color = new Color (1, 1, 1, 0);
        }
    }
}
