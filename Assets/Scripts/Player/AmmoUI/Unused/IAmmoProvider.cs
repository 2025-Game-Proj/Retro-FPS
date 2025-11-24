using System;

// 탄약 제공자 인터페이스
public interface IAmmoProvider
{
    int Current { get; }
    int Max { get; }
    event Action<int, int> OnAmmoChanged;
}
