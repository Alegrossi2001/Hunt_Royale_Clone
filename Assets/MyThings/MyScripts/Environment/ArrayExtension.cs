using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtension
{
    private static System.Random random = new System.Random();

    public static void ShuffleIndices<T>(this T[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);

            // Swap array[i] and array[j]
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
