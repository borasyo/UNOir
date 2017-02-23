using UnityEngine;
using System.Collections;

public class CharaSkillBase : MonoBehaviour {

	/// <summary>
	/// 概要 :
    /// キャラスキルの内部処理ベースクラス
	/// Author : 大洞祥太 
	/// </summary>

	public enum eSkillType {
		SKILL_ATACK = 0,	//	割合ダメージ
		SKILL_HEEL,			//	回復
		SKILL_DELAY,		//	遅延	
		SKILL_RISE,			//	確率UP
	
		SKILL_MAX,
	};

	eSkillType m_SkillType = eSkillType.SKILL_MAX; 
	[SerializeField] UnoStruct.eColor m_SkillColor = UnoStruct.eColor.COLOR_MAX;

	public eSkillType SkillType {
		get { return m_SkillType; }
		set { m_SkillType = value; }
	}

	public UnoStruct.eColor SkillColor {
		get { return m_SkillColor; }
		set { m_SkillColor = value; }
	}

    // キャラスキルの内部処理を記述する
    public virtual void ExecutionCharaSkill() {

	}
}
