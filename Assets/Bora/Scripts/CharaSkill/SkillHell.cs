using UnityEngine;
using System.Collections;

public class SkillHell : CharaSkillBase
{
    /// <summary>
    /// 概要 : 回復スキル
    /// Author : 大洞祥太
    /// </summary>

    [Range (0.0f, 1.0f)] [SerializeField] float m_fPercentage;

    void Start ()
    {
        SkillType = eSkillType.SKILL_HEEL;
    }

    // 回復処理
    public override void ExecutionCharaSkill ()
    {
        Player player = GameMainUpperManager.instance.player;

        if (player.isDead || player.hpRemain <= 0)
            return;
		
        int nhp = player.hpMax;
        player.hpRemain += (int)(nhp * m_fPercentage);
        HeelEffect.Run (); 
    }
}
