using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDeathSound : MonoBehaviour {

	/// <summary>
	/// 概要 : エネミー死亡時にSEを鳴らす
	/// Author : 大洞祥太
	/// </summary>

	void LateUpdate () {

		List<Enemy> enemyList = GameMainUpperManager.instance.enemyList;
		for (int i = 0; i < enemyList.Count; i++) {
			if (!enemyList [i].GetIsDiedTrigger () || !BattleManager.Instance.GetIsInBattle())
				continue;

			//SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_ENEMYDEAD);
		}
	}
}
