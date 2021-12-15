using UnityEngine;

namespace THFUMO
{
    public static class Utilities
    {
        public static int Repeat(int value, int min, int max)
        {
            if (value < min)
            {
                return max;
            }
            else if (value > max)
            {
                return min;
            }
            else
            {
                return value;
            }
        }
        public static void Restart(this GameObject gameObject)
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}