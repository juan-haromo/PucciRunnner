using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PucciTests
{
    [UnityTest]
    public IEnumerator GravityTest()
    {
        GameObject player = Resources.Load<GameObject>("Prefabs/Player");
        GameObject playerInstance = GameObject.Instantiate(player, Vector3.zero, player.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(Physics.gravity.y, -9.81f);
        playerInstance.GetComponentInChildren<PlayerMovement>().Flip();
        Debug.Log(Physics.gravity);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(Physics.gravity.y, 9.81f);
    }

    [UnityTest]
    public IEnumerator PlayerDeath()
    {
        GameObject player = Resources.Load<GameObject>("Prefabs/Player");
        GameObject playerInstance = GameObject.Instantiate(player, Vector3.zero, player.transform.rotation);
        GameObject scoreManager = Resources.Load<GameObject>("Prefabs/GameManager");
        GameObject scoreInstance = GameObject.Instantiate(scoreManager);
        yield return null;
        GameObject.Destroy(playerInstance);
        yield return null;
        Assert.IsTrue(scoreInstance.GetComponent<ScoreManager>().losePanel.activeSelf);
    }

    [UnityTest]
    public IEnumerator PlayerUp()
    {
        GameObject player = Resources.Load<GameObject>("Prefabs/Player");
        GameObject playerInstance = GameObject.Instantiate(player, Vector3.zero, player.transform.rotation);

        Assert.AreEqual(Vector3.up.y, playerInstance.transform.GetChild(0).transform.up.y, 0.1);
        yield return null;
        playerInstance.GetComponentInChildren<PlayerMovement>().Flip();
        yield return new WaitForSeconds(2f);
        Assert.AreEqual(Vector3.down.y, playerInstance.transform.GetChild(0).transform.up.y, 0.1);
    }

    [UnityTest]
    public IEnumerator PlayerJump()
    {
        GameObject player = Resources.Load<GameObject>("Prefabs/Player");
        GameObject playerInstance = GameObject.Instantiate(player, new Vector3(0, 2, 0), player.transform.rotation);

        GameObject ground = Resources.Load<GameObject>("Prefabs/Ground");
        GameObject groundInstance = GameObject.Instantiate(ground, Vector3.zero, player.transform.rotation);
        yield return new WaitForSeconds(2.0f);

        float startY = playerInstance.transform.GetChild(0).transform.position.y;
        Debug.Log("Start " + startY);
        playerInstance.GetComponentInChildren<PlayerMovement>().Jump();
        yield return new WaitForSeconds(0.1f);
        Assert.GreaterOrEqual( playerInstance.transform.GetChild(0).position.y, startY);
    }

    [UnityTest]
    public IEnumerator PositionObstacles()
    {
        //
        GameObject spawner = Resources.Load<GameObject>("Prefabs/SpawnManager");
        GameObject spawnerInstance = GameObject.Instantiate(spawner);

        yield return new WaitForSeconds(1.5f);
        BasicObstacle obstacle = GameObject.FindFirstObjectByType<BasicObstacle>();
        float start = obstacle.transform.position.y;
        yield return new WaitForSeconds(1.5f);
        Assert.IsTrue(obstacle.transform.position.y < start);
    }



    [UnityTest]
    public IEnumerator ObstacleSpawn()
    {
        //
        GameObject spawner = Resources.Load<GameObject>("Prefabs/SpawnManager");
        GameObject spawnerInstance = GameObject.Instantiate(spawner);

        yield return new WaitForSeconds(1.0f);
        BasicObstacle obstacle = GameObject.FindFirstObjectByType<BasicObstacle>();
        Assert.IsNotNull(obstacle);
    }

    [UnityTest]
    public IEnumerator SpawnedObstaclesSpeed()
    {
        GameObject spawner = Resources.Load<GameObject>("Prefabs/SpawnManager");
        GameObject spawnerInstance = GameObject.Instantiate(spawner);
        SpawnManager spawnManager = spawnerInstance.GetComponent<SpawnManager>();
        float obstacleInitialVelocity = spawnManager.obstaclesSpeed;
        yield return new WaitForSeconds(spawnManager.increaseSpeedTinmer + 0.1f);
        Assert.AreEqual(obstacleInitialVelocity + spawnManager.speedIncreaseAmmount, spawnManager.obstaclesSpeed);
    }

}
