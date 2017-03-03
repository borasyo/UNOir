﻿using UnityEngine;
using System.Collections;

// 指定された型で、指定された範囲の三角波生成器
public class TriangleWave<T>
{
    // オシレータ(発振器) を持つ
    private TriangleWaveOscillator oscillator = null;

    private T Min;
    private T Max;

    private T range;
    private ICalculator<T> calculator;

    // operator に頼らず演算ができる
    public T CurrentValue { get { return calculator.Add (Min, calculator.Mul (range, oscillator.m_fValue)); } }

    // 型の演算方法を指定して生成する
    public TriangleWave (T min, T max, float halfPeriod_sec, ICalculator<T> calculator)
    {
        this.calculator = calculator;
        SetRange (min, max);
        oscillator = new TriangleWaveOscillator (halfPeriod_sec);
    }

    #region SetFunction

    //  動的に範囲を変更したい場合、実行
    public void SetRange (T min, T max)
    {
        Min = min;
        Max = max;
        range = calculator.Sub (max, min);
    }

    // TODO : 今のところ必要ない
    /*public void SetPeriod (float halfPeriod_sec) {
        oscillator.Set (harfPeriod_sec);
    }*/

    #endregion

    #region GetFunction

    public int GetHalfLapCnt { get { return oscillator.GetHalfLapCnt; } }
    //  何半復したかを返す(偶数の場合は加算、逆は減算)
    public int GetLapCnt { get { return oscillator.GetLapCnt; } }
    //  何往復したかを返す

    #endregion

    // 事前に時間を指定しておき、自律的に発振するように変更
    public void Progress ()
    {
        oscillator.Progress ();
    }
}
