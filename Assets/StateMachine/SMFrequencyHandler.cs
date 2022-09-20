using UnityEngine;

namespace Hushigoeuf.StateMachine
{
    public class SMFrequencyHandler
    {
        private float _value;
        private float _lastUpdateTime;

        public float Value
        {
            get => _value;
            set => _value = Mathf.Clamp(value, 0, float.MaxValue);
        }

        public SMFrequencyHandler(float startValue = 0)
        {
            Value = startValue;
            _lastUpdateTime = Time.time;
        }

        public bool Check() => !(Value > 0) || !(Time.time - _lastUpdateTime < Value);

        public bool CheckAndUpdate()
        {
            if (!Check()) return false;

            _lastUpdateTime = Time.time;
            return true;
        }

        public static SMFrequencyHandler operator +(SMFrequencyHandler target, float value)
        {
            target.Value += value;
            return target;
        }

        public static SMFrequencyHandler operator -(SMFrequencyHandler target, float value)
        {
            target.Value -= value;
            return target;
        }

        public static SMFrequencyHandler operator *(SMFrequencyHandler target, float value)
        {
            target.Value *= value;
            return target;
        }

        public static SMFrequencyHandler operator /(SMFrequencyHandler target, float value)
        {
            target.Value /= value;
            return target;
        }
    }
}