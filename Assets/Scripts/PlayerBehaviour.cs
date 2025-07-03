using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    int maxHealth = 100;
    int currentHealth = 100;

    int score = 0;

    bool canInteract = false;
    CoinBehaviour currentCoin;
    DoorBehaviour currentDoor;

    [SerializeField]
    GameObject projectfile;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float fireStrenth = 0f;

    [SerializeField]
    float interactionDistance = 5f;

    void Update()
    {
        RaycastHit hitInfo;
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * interactionDistance, Color.green);

        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo))
        {
            if (currentCoin != null)
            {
                currentCoin.Unhighlight(); // Unhighlight the coin if raycast no longer detects it
            }
            // Debug.Log("Raycast hit: " + hitInfo.collider.gameObject.name);
            if (hitInfo.collider.CompareTag("Collectable"))
            {
                // Set the canInteract flag to true
                // Get the CoinBehaviour component from the detected object
                canInteract = true;
                currentCoin = hitInfo.collider.gameObject.GetComponent<CoinBehaviour>();
                currentCoin.Highlight(); // Highlight the coin when detected by raycast
            }
        }
        else if (currentCoin != null)
        {
            // If the raycast does not hit a collectable, unhighlight the current coin
            currentCoin.Unhighlight();
            currentCoin = null; // Clear the current coin reference
        }
    }

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
        if (canInteract)
        {
            if (currentCoin != null)
            {
                Debug.Log("Interacting with coin");
                currentCoin.Collect(this);
            }
            else if (currentDoor != null)
            {
                Debug.Log("Interacting with door");
                currentDoor.Interact();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Collectable"))
        {
            canInteract = true;
            currentCoin = other.GetComponent<CoinBehaviour>();
            currentCoin.Highlight(); // Highlight the coin when player enters trigger
        }
        else if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.GetComponent<DoorBehaviour>();
        }
    }

    public void ModifyScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }

    void OnTriggerExit(Collider other)
    {
        if (currentCoin != null)
        {
            if (other.gameObject == currentCoin.gameObject)
            {
                canInteract = false;
                currentCoin.Unhighlight(); // Unhighlight the coin when player exits trigger
                currentCoin = null;
            }
        }
    }

    void OnFire()
    {
        GameObject newProjectile = Instantiate(projectfile, spawnPoint.position, spawnPoint.rotation);
        Vector3 fireForce = spawnPoint.forward * fireStrenth;
        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);
    }

}