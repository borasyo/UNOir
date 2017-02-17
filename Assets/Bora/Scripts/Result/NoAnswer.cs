using UnityEngine;
using System.Collections;

public class NoAnswer : ResultBase {

	/// <summary>
	/// 概要 : 
	/// Author : 大洞祥太
	/// </summary>

	public GameOver gameOver = null;
	public Result result = null;

	// Update is called once per frame
	//public override void Update () {
	//	base.Update ();
	//}

	public override void Yes() {
		result.bOn = true;
		bOn = false;
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_ONWINDOW);
	}

	public override void No() {
		gameOver.bOn = true;
		bOn = false;
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_ONWINDOW);
	}
}
