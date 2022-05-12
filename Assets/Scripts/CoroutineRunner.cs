using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
