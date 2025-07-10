using System.Collections;
using UnityEngine;

public class Coroutines : MonoBehaviour
{
    [SerializeField]
    int[] intsToPrint;

    [SerializeField]
    float pauseDuration;

    [SerializeField]
    bool continueCoroutine = false;

    void Start()
    {
        StartCoroutine(TaskOne());
        StartCoroutine(TaskTwo());
    }

    IEnumerator TaskOne()
    {
        for (int i = 0; i < intsToPrint.Length; i++)
        {
            Debug.Log("Printing integer: " + intsToPrint[i]);
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    IEnumerator TaskTwo()
    {
        while (!continueCoroutine)
        {
            Debug.Log("Waiting for continueCoroutine to be true...");
            yield return null;
        }
        Debug.Log("continueCoroutine is now true, proceeding with TaskTwo.");
    }
}
