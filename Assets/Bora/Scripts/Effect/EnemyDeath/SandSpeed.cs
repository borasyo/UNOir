using UnityEngine;
using System.Collections;

public class SandSpeed : MonoBehaviour {

	/// <summary>
	/// 概要 : 敵死亡時の砂煙エフェクトの移動処理
	/// Author : 大洞祥太
	/// </summary>

	public float fTime = 1.0f;
	public float fMove = 1.0f;

	void Update() {
		transform.position += new Vector3 (fMove * (Time.deltaTime / fTime), 0, 0);
	}
}
