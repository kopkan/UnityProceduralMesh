using UnityEngine; //Mathf

namespace Wcom.Shared
{
    [System.Serializable]
    public class MinMaxValue
    {
        [SerializeField]
        private float _min = float.MaxValue;
        [SerializeField]
        private float _max = -float.MaxValue;

        public void newContender(float value)
        {
            _min = Mathf.Min(_min, value);
            _max = Mathf.Max(_max, value);
        }

        public float GGG
        {
            get
            {
                return _min;
            }
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
