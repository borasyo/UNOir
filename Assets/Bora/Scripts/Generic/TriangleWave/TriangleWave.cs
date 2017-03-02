using UnityEngine;
using System.Collections;

// 指定された型で、指定された範囲の三角波生成器
public class TriangleWave<T>
{
    // オシレータ(発振器) を持つ
    private TriangleWaveOscillator oscillator = new TriangleWaveOscillator();

    public readonly T Min;
    public readonly T Max;

    private T range;
    private ICalculator<T> calculator;

    // operator に頼らず演算ができる
    public T CurrentValue { get { return calculator.Add(Min, calculator.Mul(range, oscillator.Value)); } }

    // 型の演算方法を指定して生成する
    public TriangleWave(T min, T max, float time_sec, ICalculator<T> calculator)
    {
        Min = min;
        Max = max;
        this.calculator = calculator;
        range = calculator.Sub(max, min);
        oscillator.Set (time_sec);
    }

    // 事前に時間を指定しておき、自律的に発振するように変更
    public void Progress() { oscillator.Progress(); }
}
