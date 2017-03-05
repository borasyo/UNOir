using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TurnDataHolder
{
    /// <summary>
    /// 概要 : ターンのデータを数値として返すものだけを扱う
    /// </summary>

    public int nSetCard { get; set; }
    public int nReverseNum { get; set; }
    public int nSkipNum { get; set; }

    // HACK : 担当外のソースの変更が必要になるので、消さない
    public TurnDataBase Red = new TurnDataBase ();
    public TurnDataBase Green = new TurnDataBase ();
    public TurnDataBase Blue = new TurnDataBase ();
    public TurnDataBase Yellow = new TurnDataBase ();

    public TurnDataBase[] DataHolder = new TurnDataBase[4];

    public TurnDataHolder ()
    {
        Init ();   
    }

    public void Init ()
    {
        nSetCard = 0;
        nSkipNum = 0;
        Red.Init ();
        Blue.Init ();
        Green.Init ();
        Yellow.Init ();
        DataHolder [(int)UnoStruct.eColor.COLOR_RED] = Red;
        DataHolder [(int)UnoStruct.eColor.COLOR_GREEN] = Green;
        DataHolder [(int)UnoStruct.eColor.COLOR_BLUE] = Blue;
        DataHolder [(int)UnoStruct.eColor.COLOR_YELLOW] = Yellow;
    }

    public void Add (UnoStruct.tCard card)
    {
        // カードが追加された
        nSetCard++;  

        switch (card.m_Number) {

        // スキップ処理
        case UnoStruct.eNumber.NUMBER_SKIP:
            AddSkip (card.m_Color);
            break;

        // リバース処理
        case UnoStruct.eNumber.NUMBER_REVERSE:
            AddReverse (card.m_Color);
            break;

        // 数字処理
        case UnoStruct.eNumber.NUMBER_ZERO:
            float fNum = (float)Random.Range (1, 10);
            AddNumber (fNum, card.m_Color);
            break;

        case UnoStruct.eNumber.NUMBER_ONE:
        case UnoStruct.eNumber.NUMBER_TWO:
        case UnoStruct.eNumber.NUMBER_THREE:
        case UnoStruct.eNumber.NUMBER_FOUR:
        case UnoStruct.eNumber.NUMBER_FIVE:
        case UnoStruct.eNumber.NUMBER_SIX:
        case UnoStruct.eNumber.NUMBER_SEVEN:
        case UnoStruct.eNumber.NUMBER_EIGHT:
        case UnoStruct.eNumber.NUMBER_NINE:
            AddNumber ((float)card.m_Number, card.m_Color);
            break;
        }
    }

    void AddNumber (float fNum, UnoStruct.eColor color)
    {
        DataHolder [(int)color].nNum++;
        DataHolder [(int)color].fNumber += fNum;
    }

    void AddSkip (UnoStruct.eColor color)
    {       
        DataHolder [(int)color].nSkip++;
    }

    void AddReverse (UnoStruct.eColor color)
    {
        ReverseEffectManager.Instance.Add ();
        DataHolder [(int)color].nReverse++;
    }
};