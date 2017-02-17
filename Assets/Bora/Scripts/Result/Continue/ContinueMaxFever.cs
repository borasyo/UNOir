using UnityEngine;
using System.Collections;

public class ContinueMaxFever : MonoBehaviour {

	/// <summary>
	/// 概要 : コンティニュー時のフィーバーマックス処理
	/// Author : 大洞祥太
	/// </summary>

	FeverGauge gauge = null;

	void Start () {

		// ゲージをMAXに
		GameObject fever = GameObject.FindWithTag ("FeverGauge");	
		gauge = fever.GetComponentInChildren<FeverGauge> ();
	}

	void Update() {

		if (!BattleManager.Instance.GetIsInBattle ())
			return;

		gauge.SetPoint (gauge.feverPointMax);
		Destroy (this.GetComponent<ContinueMaxFever>());
	}
}
