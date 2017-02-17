using UnityEngine;
using System.Collections;

public class EffectBase : MonoBehaviour {

	/// <summary>
	/// 概要 : キャラ、エネミーのエフェクトの親クラス
	/// Author : 大洞祥太
	/// </summary>

	public bool bEnd = false;
	public bool bAtack = false;
	public bool bReverse = false; 

	public virtual void Set(UnoStruct.eColor color, Vector3 TargetPos) {

	}

	public virtual void Set(Vector3 TargetPos) {

	}

	public virtual void Set(Vector3 TargetPos, bool bFlg) {

	}

	public virtual void Set(bool bFlg) {
		
	}

	public virtual void Set(CharaSkillBase skillData) {

	}
}
