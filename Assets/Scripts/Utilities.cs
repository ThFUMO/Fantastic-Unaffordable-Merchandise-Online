using System;
using System.Collections;
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

        public static IEnumerator TranslateSmoothly(this Transform transform, Vector3 start, Vector3 end, float speed)
        {
            transform.position = start;
            float distance = Vector3.Distance(start, end);
            Vector3 direction = (end - start).normalized;
            while (true)
            {
                Vector3 nextPos = transform.position + speed * Time.deltaTime * direction;
                if (Vector3.Distance(nextPos, start) < distance)
                {
                    transform.position = nextPos;
                    yield return null;
                }
                else
                {
                    transform.position = end;
                    yield break;
                }
            }
        }

        public static IEnumerator TranslateSmoothly(Action<float> setter, float start, float end, float speed)
        {
            float value = start;
            setter(value);
            float distance = Mathf.Abs(start - end);
            float direction = Mathf.Sign(end - start);
            while (true)
            {
                float nextValue = value + direction * speed * Time.deltaTime;
                if (Mathf.Abs(nextValue - start) < distance)
                {
                    value = nextValue;
                    setter(value);
                    yield return null;
                }
                else
                {
                    value = end;
                    setter(value);
                    yield break;
                }
            }
        }

        public static IEnumerator CombineCoroutines(params IEnumerator[] coroutines)
        {
            foreach (IEnumerator coroutine in coroutines)
            {
                yield return coroutine;
            }
            yield break;
        }
    }
}