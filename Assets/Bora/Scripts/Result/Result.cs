using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;

public class Result : ResultBase {

	/// <summary>
	/// 概要 : 勝ち処理
	/// Author : 大洞祥太
	/// </summary>

	// Update is called once per frame
	//public override void Update () {
	//	base.Update ();
	//}

	public override void Yes() {
		// 終了
		SceneChanger.Instance.ChangeScene ("MainMenu", 1.0f, true);
		//SceneManager.LoadScene ("Title");
		//ResultManager.Instance.EndResult();
	}
}
