using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillRise : CharaSkillBase
{
    /// <summary>
    /// 概要 : カードの出現確率アップスキル
    ///        残り枚数をストックして判定
    /// Author : 大洞祥太
    /// </summary>

    static int m_nStock = 0;
    static public int GetStock { get { return m_nStock; } private set { m_nStock = value; } }
    [SerializeField] int m_nSetStock = 5;

    public int GetMaxNum { get { return m_nSetStock * m_nDuplicateNum; } private set { m_nSetStock = value; } }

    static public bool m_IsRun = false; //  スキルが重複した場合分かるようにするため
    static int m_nDuplicateNum = 0;     //  スキル重複数
    static public int m_nCardNum = 0; 

    void Start ()
    {
        m_nStock = 0;
        SkillType = eSkillType.SKILL_RISE;
    }

    public override void ExecutionCharaSkill ()
    {
        m_IsRun = true;
        m_nStock += m_nSetStock;
        m_nDuplicateNum++;
    }

    static public void Reduce (int nAmount)
    {
        m_nStock -= nAmount;

        if (m_nStock > 0)
            return;
        
        m_IsRun = false;
        m_nStock = 0;
        m_nDuplicateNum = 0;
    }
}
