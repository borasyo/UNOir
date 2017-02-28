using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillAtack : CharaSkillBase
{

    /// <summary>
    /// 概要 : 割合ダメージスキル
    /// Author : 大洞祥太
    /// </summary>

    [Range (0.0f, 1.0f)] public float m_fPercentage;

    // Use this for initialization
    void Start ()
    {	
        SkillType = eSkillType.SKILL_ATACK;
    }

    //  キャラスキルの内部処理を実行する
    public override void Run ()
    {
        if (GameMainUpperManager.instance.player.isDead)
            return;

        List<Enemy> enemyList = GameMainUpperManager.instance.enemyList;
        foreach (Enemy enemy in enemyList) {
            int hp = enemy.hpMax;
            int damage = (int)(hp * m_fPercentage);

            // TODO : 型変換が危ないけど、Charactorが自分の担当箇所ではないので、一度保留にする
            enemy.Damaged (damage, (Charactor.Attribute)SkillColor);
        }
    }
}
