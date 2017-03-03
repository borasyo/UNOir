using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	/// <summary>
	/// 概要 : エネミーの上下移動
	/// Author : 大洞祥太
	/// </summary>

	[SerializeField] float m_fTime = 1.0f;

	[SerializeField] float m_fRange = 0.1f;

	[SerializeField]
	Enemy enemy = null;

	Vector3 InitPos = Vector3.zero;
	Transform[] child = new Transform[2];

    TriangleWave<Vector3> m_TriangleWaveVector3 = null;

	void Start () {
		InitPos = transform.position;

		for (int i = 0; i < child.Length; i++) {
			child [i] = transform.GetChild (i);
		}

        Vector3 min = transform.position;
        Vector3 max = transform.position + new Vector3 (0, m_fRange, 0);
        m_TriangleWaveVector3 = TriangleWaveFactory.Vector3 (min, max, m_fTime);
	}

	void Update () {
		
		if (enemy.isDead || enemy.gaugeSpeed < 1.0f)
			return;

		Vector2[] tempPos = new Vector2[child.Length];
		for (int i = 0; i < child.Length; i++) {
			tempPos[i] = child [i].position;
		}

        m_TriangleWaveVector3.Progress ();
        transform.position = m_TriangleWaveVector3.CurrentValue;

		for (int i = 0; i < child.Length; i++) {
			child [i].position = tempPos[i];
		}
	}
}
