using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrowDataManager
{
    public List<DrowData> m_DrowDataList = new List<DrowData> ();
    bool m_IsOldBattle = false;

    const float fDrowTwoLife = 10.0f;
    const float fDrowTwoAmount = 2.0f;
    const float fDrowFourLife = 20.0f;
    const float fDrowFourAmount = 2.0f;

    public void Update ()
    {
        // TODO : まだ綺麗にできそう
        if (m_DrowDataList.Count > 0) {

            if (!BattleManager.Instance.GetIsInBattle ()) {
                SoundManager.Instance.PauseBGM (true);
                m_IsOldBattle = BattleManager.Instance.GetIsInBattle ();
                return;
            }

            if (!m_IsOldBattle) {
                SoundManager.Instance.PauseBGM (false);
            }
        }

        // ドロー系更新
        List<int> Destroy = new List<int> ();
        foreach(DrowData data in m_DrowDataList) {
            data.Update ();
            if (data.m_fLife <= 0.0f) {
                Destroy.Add (m_DrowDataList.IndexOf(data));
                data.Run (false);
            } else {
                data.Run (true);
            }
        }

        for (int i = 0; i < Destroy.Count; i++) {
            m_DrowDataList.RemoveAt (Destroy [i] - i);
        }

        if (Destroy.Count > 0 && m_DrowDataList.Count <= 0) {
            SoundManager.Instance.StopBGM (SoundManager.eBgmValue.BGM_ATACKUP);
        }
        m_IsOldBattle = BattleManager.Instance.GetIsInBattle ();
    }

    //  追加するかチェック
    public void AddCheck(UnoStruct.tCard card)
    {
        switch (card.m_Number) {
       
        case UnoStruct.eNumber.NUMBER_DROWFOUR:
        case UnoStruct.eNumber.NUMBER_DROWTWO:
            Add (card);
            break;

        default:
            break;
        }
    }

    void Add (UnoStruct.tCard card)
    {
        float life   = 0.0f;
        float amount = 0.0f;
        if (card.m_Number == UnoStruct.eNumber.NUMBER_DROWTWO) {
            life   = fDrowTwoLife;
            amount = fDrowTwoAmount;
        } else {
            life   = fDrowFourLife;
            amount = fDrowFourAmount;
        }
        m_DrowDataList.Add (new DrowData (card.m_Color, life, amount));

        PowerUpAlpha.Instance.Reset ();
        if (!SoundManager.Instance.NowOnBGM (SoundManager.eBgmValue.BGM_ATACKUP)) {
            SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_ATACKUP);
        }
    }

    public void Calculate(ref TurnDataHolder data) 
    {
        // ドロー倍率
        foreach(DrowData drowdata in m_DrowDataList) {

            if (drowdata.DrowColor != UnoStruct.eColor.COLOR_WILD) {
                
                data.DataHolder[(int)drowdata.DrowColor].fNumber *= drowdata.m_fAmount; 
            } 
            else {
                data.Red.fNumber    *= drowdata.m_fAmount;
                data.Blue.fNumber   *= drowdata.m_fAmount;
                data.Green.fNumber  *= drowdata.m_fAmount;
                data.Yellow.fNumber *= drowdata.m_fAmount;
            }
        }
    }
}
