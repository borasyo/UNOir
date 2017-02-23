using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharaSkillEffect : MonoBehaviour {

	/// <summary>
	/// 概要 : 
    /// キャラカードが盤面にあって、出せる時に
    /// カードを光らせるエフェクト
	/// 
    /// Author : 大洞祥太
	/// </summary>

	UnoData m_UnoData = null;
	SpriteRenderer m_SpriteRenderer = null;

	void Start() {
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		m_UnoData = GetComponentInParent<UnoData> ();

		UpdateEnable ();
	}

	void Update () {
		SetColor (FieldCard.Instance.Judge (m_UnoData.CardData) && !m_UnoData.OnClick && !GameController.Instance.bStop);
	}

    // このカードは今キャラカードなのかをパーティ情報からチェック
    // 関数名が分かりにくいので修正する
    public void UpdateEnable() {
		this.enabled = CharaCardCheck ();
		SetColor (this.enabled);
	}

    bool CharaCardCheck() {

		UnoStruct.tCard myCardData = m_UnoData.CardData;
		List<PlayerCharactor> player = GameMainUpperManager.instance.charactorAndFriend;
        
        for (int i = 0; i < player.Count; i++) {
            if (myCardData.m_Color  != player[i].GetTCard().m_Color ||
                myCardData.m_Number != player[i].GetTCard().m_Number) {
                continue;
            }

            // TODO : 違うタスクなので別に移動したい
			m_SpriteRenderer.sprite = CharaSkillEffectData.Instance.GetSprite(i);

            return true;
        }

        return false;
    }

	void SetColor(bool IsOn) {

		if(IsOn) {
			m_SpriteRenderer.color = CharaSkillEffectData.Instance.nowColor;
		} else {
			m_SpriteRenderer.color = new Color (1, 1, 1, 1.0f - CharaSkillEffectData.Instance.fAddAlpha);
		}
	}
}
