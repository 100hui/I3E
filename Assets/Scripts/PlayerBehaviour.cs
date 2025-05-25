using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    int maxHealth = 100;
    int currentHealth = 100;

    int score = 0;

    bool canInteract = false;
    CoinBehaviour currentCoin;

    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("Player collided with " + collision.gameObject.name);
    //     if (collision.gameObject.CompareTag("Collectable"))
    //     {
    //         ++score;
    //         Debug.Log("Score: " + score);
    //     }
    //     else if (collision.gameObject.CompareTag("Hazard"))
    //     {
    //         currentHealth -= 35;
    //         Debug.Log("Health: " + currentHealth);
    //         if (currentHealth <= 0)
    //         {
    //             Debug.Log("Player is dead!");
    //         }
    //     }
    // }

    // void OnCollisionExit(Collision collision)
    // {
    //     Debug.Log("Player stopped colliding with " + collision.gameObject.name);
    // }
    // void OnCollisionStay(Collision collision)
    // {
    //     Debug.Log("Player is still colliding with " + collision.gameObject.name);
    // }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("HealingArea"))
        {
            if (currentHealth < maxHealth)
            {
                ++currentHealth;
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                Debug.Log("Healing: " + currentHealth);
            }
        }
    }

    void OnInteract()
    {
        if (canInteract && currentCoin != null)
        {
            currentCoin.Collect(this);
            canInteract = false;
            currentCoin = null;
            Debug.Log("Interacting with object");
        }
        else
        {
            Debug.Log("Nothing to interact with");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            Debug.Log("Player is looking at " + other.gameObject.name);
            currentCoin = other.GetComponent<CoinBehaviour>();
            canInteract = true;
        }
    }

    public void ModifyScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }

    void OnTriggerExit(Collider other)
    {
        currentCoin = null;
        canInteract = false;
    }

}