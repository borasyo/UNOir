using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CharaSkillEffect : MonoBehaviour
{

    /// <summary>
    /// 概要 : 
    /// キャラカードが盤面にあって、出せる時に
    /// カードを光らせるエフェクト
    /// 
    /// Author : 大洞祥太
    /// </summary>

    UnoData m_UnoData = null;
    SpriteRenderer m_SpriteRenderer = null;

    void Start ()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer> ();
        m_UnoData = GetComponentInParent<UnoData> ();

        UpdateEnable ();
    }

    void Update ()
    {
        SetColor (FieldCard.Instance.Judge (m_UnoData.CardData) && !m_UnoData.OnClick && !GameController.Instance.bStop);
    }

    // このカードは今キャラカードなのかをパーティ情報からチェック
    // 関数名が分かりにくいので修正する
    public void UpdateEnable ()
    {
        PlayerCharactor playerChara = CharaCardCheck ();
		
        this.enabled = playerChara;
        SetColor (this.enabled);
        SetSprite (playerChara);
    }

    PlayerCharactor CharaCardCheck ()
    {
        UnoStruct.tCard myCardData = m_UnoData.CardData;
        List<PlayerCharactor> playerList = GameMainUpperManager.instance.charactorAndFriend;

        // カラーと番号の合う物を抽出(1つあればいいので、先頭のみ)
        PlayerCharactor playerChara = playerList.Where (player => 
            myCardData.m_Color  == player.GetTCard ().m_Color &&
            myCardData.m_Number == player.GetTCard ().m_Number).FirstOrDefault ();

        return playerChara; // 存在すればTrueを返す
    }

    void SetColor (bool IsOn)
    {
        if (IsOn) {
            m_SpriteRenderer.color = CharaSkillEffectData.Instance.m_NowColor;
        } else {
            m_SpriteRenderer.color = new Color (1, 1, 1, 1.0f - CharaSkillEffectData.Instance.m_fAddAlphaAmount);
        }
    }

    void SetSprite (PlayerCharactor playerChara)
    {
        if (playerChara == null)
            return;

        int index = GameMainUpperManager.instance.charactorAndFriend.IndexOf (playerChara);

        m_SpriteRenderer.sprite = CharaSkillEffectData.Instance.GetSprite (index);
    }
}
