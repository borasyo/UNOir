using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class EnemyMove : MonoBehaviour
{
    /// <summary>
    /// 概要 : エネミーの上下移動
    /// Author : 大洞祥太
    /// </summary>

    [SerializeField] float m_fTime = 1.0f;

    [SerializeField] float m_fRange = 0.1f;

    [SerializeField] Enemy enemy = null;

    Transform[] m_Childs = new Transform[2];

    TriangleWave<Vector3> m_TriangleWaveVector3 = null;

    void Start ()
    {
        m_Childs = m_Childs.Select ((child, index) => {
            child = transform.GetChild (index);   //  子を取得
            return child;
        }).ToArray ();

        Vector3 min = transform.position;
        Vector3 max = transform.position + new Vector3 (0, m_fRange, 0);
        m_TriangleWaveVector3 = TriangleWaveFactory.Vector3 (min, max, m_fTime);
    }

    void Update ()
    {
        if (enemy.isDead || enemy.gaugeSpeed < 1.0f)
            return;
        
        Vector3[] tempPos = m_Childs.Select (child => child.position).ToArray ();

        m_TriangleWaveVector3.Progress ();
        transform.position = m_TriangleWaveVector3.CurrentValue;
       
        // for分の代入式をLINQに変更
        m_Childs = m_Childs.Select ((child, index) => {
            child.position = tempPos [index];   //  座標格納
            return child;
        }).ToArray ();
    }
}
