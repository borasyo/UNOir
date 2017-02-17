using UnityEngine;
using System.Collections;

public class EnemyPos : MonoBehaviour {

	/// <summary>
	/// 概要 : 座標を保存
	/// Author : 大洞祥太
	/// </summary>

	static public Vector3 Pos { get; private set; }

	void Awake() {
		Pos = transform.position;
	}
}
