using UnityEngine;
using System.Collections;

public class AutoDeatroyNotIsPlaying : MonoBehaviour {

	/// <summary>
	/// 概要 : パーティクルが終了したら自動破壊
	/// Author : 大洞祥太
	/// </summary>

	ParticleSystem m_Particle = null;

	void Start() {
		m_Particle = GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		if (m_Particle.isPlaying)
			return;
		
		Destroy (this.gameObject);
	}
}
