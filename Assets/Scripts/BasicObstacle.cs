using UnityEngine;

public class BasicObstacle : MonoBehaviour
{
    public float horizontalSpeed = 5f;

    public Transform maxPosition;
    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
    }

    public void HorizontalMovement()
    {
        if(transform.position.x < maxPosition.position.x)
            Destroy(gameObject);
        else
            transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Destroy(other.gameObject);

    }
}
