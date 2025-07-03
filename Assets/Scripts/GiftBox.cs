using UnityEngine;

public class GiftBox : MonoBehaviour
{
    [SerializeField]
    GameObject coinPrefab;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Instantiate a coin at the position of the gift box
            Instantiate(coinPrefab, transform.position, Quaternion.identity);

            // Destroy the gift box after it has been hit
            Destroy(gameObject);
            Destroy(collision.gameObject); // Optionally destroy the projectile as well
        }
        
    }
}
