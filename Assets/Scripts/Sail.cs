using System;
using UnityEngine;

public class Sail : MonoBehaviour
{
    public static event Action useSail;

    public void UseSail()
    {
        useSail?.Invoke();
    }
}
