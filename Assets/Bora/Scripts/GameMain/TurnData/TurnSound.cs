using UnityEngine;
using System.Collections;

public class TurnSound {

    public void SetCardSound(int raw_nType) 
    {
        if (raw_nType <= 0)
            return;

        int nType = 0;
        if(raw_nType > 1) { // 1は0除算になってしまうため
            nType = (raw_nType - 1) / 4;    //  変換
        }

        switch(nType) {
        case 0:
            SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SETCARD_DO);
            break;
        case 1:
            SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SETCARD_MI);
            break;
        case 2:
            SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SETCARD_SO);
            break;
        case 3:
            SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SETCARD_SI);
            break;
        }        
    }

    public void SkipSound(int num)
    {
        if (num <= 0)
            return;
        
        SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SKIP);
    }
}
