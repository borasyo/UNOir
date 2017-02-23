using UnityEngine;
using System.Collections;

public class EndParticleDestroyer : MonoBehaviour {

	/// <summary>
	/// 概要 : 終了したパーティクルを破壊
	/// Author : 大洞祥太
	/// </summary>

	ParticleSystem m_Particle = null;

	void Start() {
		m_Particle = GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		if (m_Particle.time < m_Particle.duration) 
            return;

        Destroy(this.gameObject);
	}
}
