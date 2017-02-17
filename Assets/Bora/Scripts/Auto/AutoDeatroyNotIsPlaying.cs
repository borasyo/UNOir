using UnityEngine;
using System.Collections;

public class AutoDeatroyNotIsPlaying : MonoBehaviour {

	/// <summary>
	/// 概要 : パーティクルが終了したら自動破壊
	/// Author : 大洞祥太
	/// </summary>

	ParticleSystem particle = null;

	void Start() {
		particle = GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		if (!particle.isPlaying) {
			Destroy (this.gameObject);
		}
	}
}
