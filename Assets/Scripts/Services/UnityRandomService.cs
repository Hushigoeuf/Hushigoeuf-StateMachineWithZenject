using UnityEngine;

namespace Hushigoeuf
{
    public class UnityRandomService : IRandomService
    {
        public float Range(float min, float max) => Random.Range(min, max);

        public int Range(int min, int max, bool inclusive) =>
            min != max ? Random.Range(min, max + (inclusive ? 1 : 0)) : min;

        public float Range(Vector2 range) => Range(range.x, range.y);
        public int Range(Vector2Int range, bool inclusive) => Range(range.x, range.y, inclusive);

        public bool IsChance(float criteria) => Mathf.Clamp01(criteria) >= Range(.0f, 1f);

        public float GetRandomValue() => Random.value;
        public int GetRandomIndex(int count) => Range(0, count, false);
        public T Choose<T>(params T[] values) => values.Length != 0 ? values[GetRandomIndex(values.Length)] : default;
    }
}