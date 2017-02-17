using UnityEngine;
using System.Collections;

public class Retire : MonoBehaviour {

	/// <summary>
	/// 概要 : リタイア処理
	/// Author : 大洞祥太
	/// </summary>

    public void OnClick() {
        GameController.Instance.SetDelta(1.0f);
        GamePause.UnPause();
		SceneChanger.Instance.ChangeScene ("MainMenu", 1.0f, false);
	}
}
