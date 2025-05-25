using UnityEngine;

public class cube : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string numbers = "";
    int a = 6; 
    int b = 12;
    int smaller, larger;
    int increments = 0;
    void Start()
    {
        for (int i=1; i <= 10; i++)
        {
            numbers += i + " ";
        }
        Debug.Log(numbers);

        if (a < b)
        {
            smaller = a;
            larger = b;
        }
        else
        {
            smaller = b;
            larger = a;
        }
        while (smaller < larger)
        {
            smaller++;
            increments++;
        }
        Debug.Log("It took " + increments + " increments to make the numbers equal.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
