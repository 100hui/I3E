using UnityEngine;

public class custom : MonoBehaviour
{
    // Variables should be here!!!
    int health = 10;
    int maxHealth =50;
    float speed = 5.0f;
    string demoString = "Ayam Penyet from Food Club is the best.";
    bool isAlive = true;
    // string numbers = "";
    // int a = 6; 
    // int b = 12;
    // int smaller, larger;
    // int increments = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // for (int i=1; i <= 10; i++)
        // {
        //     numbers += i + " ";
        // }
        // Debug.Log(numbers);

        // if (a < b)
        // {
        //     smaller = a;
        //     larger = b;
        // }
        // else
        // {
        //     smaller = b;
        //     larger = a;
        // }
        // while (smaller < larger)
        // {
        //     smaller++;
        //     increments++;
        // }
        // Debug.Log("It took " + increments + " increments to make the numbers equal.");

        //health = 10;
        // Debug.Log(--health);

        // Debug.Log(demoString);
        // Debug.Log(isAlive);

        // while (isAlive)
        // {
        //     --health;
        //     //if no curly braces, only the first line is part of the if statement
        //     if (health <= 0)
        //      isAlive = false;
        // }

        for (int i = 0; i < 10; ++i)
        {
            --health;
        }

        if (health >= maxHealth)
        {
            Debug.Log("Player is at full health.");
        }

        else if (health < maxHealth && health > 0)
        {
            Debug.Log("Player is not at full health.");
        }

        else if (health == 0)
        {
            Debug.Log("Player is dead.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("This is a demo component.");
    }
}
