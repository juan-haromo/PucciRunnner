using UnityEngine;

public class FallingObstacle : BasicObstacle
{
    public float fallingSpeed = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        horizontalSpeed = GetComponentInParent<SpawnManager>().obstaclesSpeed;
        maxPosition = GetComponentInParent<SpawnManager>().maxPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        FallingDown();
    }

    public void FallingDown()
    {
        transform.Translate(Vector3.down * fallingSpeed * Time.deltaTime);
    }
}
