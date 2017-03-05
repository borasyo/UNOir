using UnityEngine;
using System.Collections;
using System.Linq;

public class TurnDataCalculator
{
    const int nColorMagNum = 5;
    const float fColorMagnification = 2.0f;
    float[] fMagnification;

    public TurnDataCalculator(float[] mag)
    {
        fMagnification = mag;
    }

    public TurnDataHolder Calculate(TurnDataHolder turnData)
    {
        // リバースとスキップの合計を計算
        turnData.nSkipNum = turnData.DataHolder.Sum (data => data.nSkip);
        turnData.nReverseNum = turnData.DataHolder.Sum (data => data.nReverse);

        // --- 各色毎の倍率チェック ----
        MagCalculate (ref turnData.Red);
        MagCalculate (ref turnData.Blue);
        MagCalculate (ref turnData.Green);
        MagCalculate (ref turnData.Yellow);

        //  出した枚数に応じた倍率を計算
        SetCardNumMagCalc (ref turnData);

        return turnData;
    }

    void SetCardNumMagCalc (ref TurnDataHolder data)
    {
        if (data.nSetCard <= 0)
            return;

        data.Red.fNumber *= fMagnification [data.nSetCard - 1];
        data.Blue.fNumber *= fMagnification [data.nSetCard - 1];
        data.Yellow.fNumber *= fMagnification [data.nSetCard - 1];
        data.Green.fNumber *= fMagnification [data.nSetCard - 1];
    }

    void MagCalculate (ref TurnDataBase data)
    {
        if (data.nNum < nColorMagNum)
            return;

        data.fNumber *= fColorMagnification;
    }
}
