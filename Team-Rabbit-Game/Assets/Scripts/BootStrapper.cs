using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BootStrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitializeDebugger()
    {
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Sound")));
    }
}
