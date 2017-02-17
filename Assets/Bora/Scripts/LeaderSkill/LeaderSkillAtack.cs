using UnityEngine;
using System.Collections;

public class LeaderSkillAtack : LeaderSkillBase {

	/// <summary>
	/// 概要 : 割合ダメージリーダースキル
	/// Author : 大洞祥太
	/// </summary>

	[Range(0.0f,1.0f)] public float fPercentage;

	// Use this for initialization
	void Start () {
		TurnData.Instance.leaderSkillAtack = this.GetComponent<LeaderSkillAtack> ();
	}

	public float fGetPercentage() {
		return fPercentage + 1.0f;
	}
}
