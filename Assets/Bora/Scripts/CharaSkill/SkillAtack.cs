using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillAtack : CharaSkillBase {

	/// <summary>
	/// 概要 : 割合ダメージスキル
	/// Author : 大洞祥太
	/// </summary>

	[Range(0.0f,1.0f)] public float fPercentage;

	// Use this for initialization
	void Start () {
		
		SkillType = eSkillType.SKILL_ATACK;
	}

	public override void Run() {

		if (GameMainUpperManager.instance.player.isDead)
			return;

		List<Enemy> enemyList = GameMainUpperManager.instance.enemyList;
		for (int i = 0; i < enemyList.Count; i++) {
			int hp = enemyList[i].hpMax;
			int damage = (int)(hp*fPercentage);
            enemyList[i].Damaged(damage, (Charactor.Attribute)SkillColor);
			//enemyList [i].FlashAnimation ();
		}
	}
}
