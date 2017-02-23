using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharaSkillEffect : MonoBehaviour {

	/// <summary>
	/// /* 概要 */ 
    /// キャラカードが盤面にあって、出せる時に
    /// カードを光らせるエフェクト
	/// 
    /// Author : 大洞祥太
	/// </summary>

	bool bCharaCard = false;
//	UnoStruct.tCard tempCard;

	UnoData unoData = null;
	SpriteRenderer spriteRenderer = null;

	void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
		unoData = GetComponentInParent<UnoData> ();
	}

	void Update () {

        if (!bCharaCard)
			return;

        if (!FieldCard.Instance.Judge(unoData.CardData) || unoData.OnClick || GameController.Instance.bStop)
        {
			spriteRenderer.color = new Color (1, 1, 1, 1.0f - CharaSkillEffectData.Instance.fAddAlpha);
			transform.localScale = CharaSkillEffectData.Instance.InitScale;
			return;
		}

		//transform.localScale = CharaSkillEffectData.Instance.nowScale;
		spriteRenderer.color = CharaSkillEffectData.Instance.nowColor;

		/*if (FieldCard.Instance.GetTempFlg () && !FieldCard.Instance.Judge(tempCard)) {
			spriteRenderer.color = new Color (fBlack,fBlack,fBlack,spriteRenderer.color.a);
		}*/
	}

    // このカードは今キャラカードなのかをパーティ情報からチェック
    // 関数名が分かりにくいので修正する
    public void RunCheck() {

        bCharaCard = CharaCardCheck();

        if (bCharaCard) {
            //tempCard = card;
        } else {
			spriteRenderer.color = new Color (1,1,1, 1.0f - CharaSkillEffectData.Instance.fAddAlpha);
			transform.localScale = CharaSkillEffectData.Instance.InitScale;
		}
	}

    bool CharaCardCheck() {

        UnoStruct.tCard myCardData = unoData.CardData;
		List<PlayerCharactor> player = GameMainUpperManager.instance.charactorAndFriend;
        
        for (int i = 0; i < player.Count; i++) {
            if (myCardData.m_Color != player[i].GetTCard().m_Color ||
                myCardData.m_Number != player[i].GetTCard().m_Number) {
                continue;
            }

            // TODO : 違うタスクなので別に移動したい
            //tempCard = card;
            spriteRenderer.sprite = CharaSkillEffectData.Instance.GetSprite(i);

            return true;
        }

        return false;
    }
}
