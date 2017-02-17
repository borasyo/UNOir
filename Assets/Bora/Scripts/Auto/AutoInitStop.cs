using UnityEngine;
using System.Collections;

public class AutoInitStop : MonoBehaviour {

	/// <summary>
	/// 概要 : Particleを強制ストップ
	/// PlayOnAwakeがあるため不要になった。
	/// Author : 大洞祥太
	/// </summary>

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem> ().Stop ();
	}
}
