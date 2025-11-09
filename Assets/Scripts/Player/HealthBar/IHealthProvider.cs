using System;

// 체력 제공자 인터페이스
public interface IHealthProvider
{
    float Current { get; }
    float Max { get; }

    event Action<float, float> OnHealthChanged;
}
