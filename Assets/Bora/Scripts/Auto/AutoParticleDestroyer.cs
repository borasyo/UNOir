using UnityEngine;
using System.Collections;

public class AutoParticleDestroyer : MonoBehaviour {

	/// <summary>
	/// 概要 : パーティクルがduration分進んだら破壊
	/// Author : 大洞祥太
	/// </summary>

	ParticleSystem particle = null;

	void Start() {
		particle = GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		if (particle.time < particle.duration) 
            return;

        Destroy(this.gameObject);
	}
}
