using UnityEngine; //Mathf

namespace Shared
{
    public class MinMaxValue
    {
        float _min = float.MaxValue;
        float _max = -float.MaxValue;

        public void newContender(float value)
        {
            _min = Mathf.Min(_min, value);
            _max = Mathf.Max(_max, value);
        }

        public float min
        {
            get
            {
                return _min;
            }
        }
        public float max
        {
            get
            {
                return _max;
            }
        }
    }
}
