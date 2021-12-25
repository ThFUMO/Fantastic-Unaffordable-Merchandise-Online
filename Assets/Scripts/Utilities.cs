using System;
using UnityEngine;

namespace THFUMO
{
    public static class Utilities
    {
        public static int Repeat(int value, int min, int max)
        {
            if (min == max)
            {
                return min;
            }
            if (min > max)
            {
                int t = min;
                min = max;
                max = t;
            }
            if (value > max)
            {
                return (value - max - 1) % (max - min + 1) + min;
            }
            else if (value < min)
            {
                return max - (min - value - 1) % (max - min + 1);
            }
            else
            {
                return value;
            }
        }
        public static void Reactivate(this GameObject gameObject)
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}