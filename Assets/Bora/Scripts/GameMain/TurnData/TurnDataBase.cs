using UnityEngine;
using System.Collections;

public class TurnDataBase {

    public int nNum { get; set;}        // 数字カードの枚数
    public float fNumber { get; set;}   // 数字合計(補正含めた)
    public int nReverse { get; set;}    // リバース使用有無
    public int nSkip { get; set;}       // スキップ使用有無

    public TurnDataBase ()
    {
        Init ();
    }

    public void Init ()
    {
        nNum = 0;
        fNumber = 0.0f;
        nReverse = 0;
        nSkip = 0;
    }
}
