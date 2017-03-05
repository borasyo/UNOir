using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContinueAtackUpManager {

    List<ContinueAtackUp> m_ContinueAtackUpList = new List<ContinueAtackUp>();

    public void Add(ContinueAtackUp atackup)
    {
        m_ContinueAtackUpList.Add (atackup);
    }

    // コンティニューの攻撃力アップ分を計算する
    public void Calculate(ref TurnDataHolder data)
    {
        foreach (ContinueAtackUp atackup in m_ContinueAtackUpList) {
            float fAmount = atackup.AtackAmount;
            data.Red.fNumber *= fAmount;
            data.Blue.fNumber *= fAmount;
            data.Green.fNumber *= fAmount;
            data.Yellow.fNumber *= fAmount;
        }
    }
}
