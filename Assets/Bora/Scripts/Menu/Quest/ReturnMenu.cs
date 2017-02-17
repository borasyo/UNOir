using UnityEngine;
using System.Collections;

public class ReturnMenu : MonoBehaviour {

	/// <summary>
	/// 概要 : 
	/// Author : 大洞祥太
	/// </summary>

	public void OnClick() {
		SceneChanger.Instance.ChangeScene ("MainMenu", 1.0f, false);
	}
}
