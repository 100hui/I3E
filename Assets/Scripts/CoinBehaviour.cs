using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public int coinValue = 10;

    public void Collect(PlayerBehaviour player)
    {
        player.ModifyScore(coinValue);
        Destroy(gameObject);
    }
}
