using UnityEngine;

public class Coin : BasicObstacle
{
    [SerializeField] int score;
    new void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(score);
            Destroy(gameObject);
        }
    }
}
