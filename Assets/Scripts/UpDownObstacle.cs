using System.Collections;
using UnityEngine;

public class UpDownObstacle : BasicObstacle
{
    public float verticalSpeed = 5f;
    public float changeDirectionTimer = 1;
    private bool isGoingUp = true;

    public Transform upperLimit;
    public Transform downLimit;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upperLimit = GetComponentInParent<SpawnManager>().spawnPositions[0];
        downLimit = GetComponentInParent<SpawnManager>().spawnPositions[2];
        horizontalSpeed = GetComponentInParent<SpawnManager>().obstaclesSpeed;
        maxPosition = GetComponentInParent<SpawnManager>().maxPosition;
        //StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        VerticalMovement();
        HorizontalMovement();
    }


    void VerticalMovement()
    {
        if (isGoingUp)
        {
            transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime);
            if (transform.position.y >= upperLimit.position.y)
            {
                isGoingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime);
            if (transform.position.y <= downLimit.position.y)
            {
                isGoingUp = true;
            }
        }
    }
}
