using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillDelay : CharaSkillBase
{
    /// <summary>
    /// 概要 : 遅延スキル
    /// Author : 大洞祥太
    /// </summary>

    [Range (0.0f, 1.0f)] [SerializeField] float m_fSpeed_OneSec;
    [SerializeField] float m_fTime_Sec = 2.0f;
    float m_fNowTime_Sec = 0.0f;

    public bool m_IsRun { get; private set; }

    List<Enemy> enemyList = new List<Enemy> ();

    // Use this for initialization
    void Start ()
    {	
        SkillType = eSkillType.SKILL_DELAY;
    }
	
    // Update is called once per frame
    void Update ()
    {
        if (!m_IsRun)
            return;

        CheckDeathEnemy ();

        if (!BattleManager.Instance.GetIsInBattle ())
            return;

        m_fNowTime_Sec += Time.deltaTime;
        foreach (Enemy enemy in enemyList) {
            enemy.SetGaugeSpeed (m_fSpeed_OneSec);
        }

        if (m_fNowTime_Sec < m_fTime_Sec)
            return;

        Reset ();
        SoundManager.Instance.StopBGM (SoundManager.eBgmValue.BGM_THUNDERNOW);
    }

    public override void Run ()
    {
        m_IsRun = true;
        enemyList = GameMainUpperManager.instance.enemyList;
    }

    void CheckDeathEnemy ()
    {
        List<Enemy> checkList = GameMainUpperManager.instance.enemyList;

        if (checkList.Count > 0 && checkList [0] == enemyList [0])
            return;

        enemyList = checkList;  // enemy情報が更新されているので再登録
        Reset ();
    }

    void Reset() 
    {
        m_IsRun = false;
        foreach (Enemy enemy in enemyList) {
            enemy.SetGaugeSpeed (1.0f);
        }
        m_fNowTime_Sec = 0.0f;
    }
}
