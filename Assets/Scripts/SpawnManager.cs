using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnManager : MonoBehaviour
{
    [Header("Posiciones")]
    // 0 High, 1 mid, 2, low
    public Transform[] spawnPositions;
    public Transform maxPosition;
    
    [Header("Prefabs de obstaculos")]
    //0 Básico, 1 Falling, 2 UpDown
    public GameObject [] obstacles;
    
    [Header("Velocidades")]
    public float obstaclesSpeed;
    public float obstacleMaxSpeed = 15;
    public float speedIncreaseAmmount;
    public float increaseSpeedTinmer = 2;
    public float obstaclesSpawnRate = 3.5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(IncreaseObstacleSpeed());
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator IncreaseObstacleSpeed()
    {
        while (obstaclesSpeed <= obstacleMaxSpeed)
        {
            yield return new WaitForSeconds(increaseSpeedTinmer); 
            obstaclesSpeed += speedIncreaseAmmount;
            //Debug.Log(obstaclesSpeed);
        }
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(obstaclesSpawnRate);
            int randomObstacle = Random.Range(0, obstacles.Length);
            if (randomObstacle == 0)
            {
                int randomSpawnPoint = Random.Range(0, spawnPositions.Length);
                Instantiate(obstacles[randomObstacle], spawnPositions[randomSpawnPoint]);
            }
            else if (randomObstacle == 1)
            {
                Instantiate(obstacles[randomObstacle], spawnPositions[0]);
            }
            else
            {
                Instantiate(obstacles[randomObstacle], spawnPositions[1]);
            }
            
        }
    }
}
