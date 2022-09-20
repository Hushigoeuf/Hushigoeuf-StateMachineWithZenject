using UnityEngine;

namespace Hushigoeuf
{
    public interface IRandomService
    {
        float Range(float min, float max);
        int Range(int min, int max, bool inclusive);
        float Range(Vector2 range);
        int Range(Vector2Int range, bool inclusive);
        bool IsChance(float criteria);
        float GetRandomValue();
        int GetRandomIndex(int count);
        T Choose<T>(params T[] values);
    }
}