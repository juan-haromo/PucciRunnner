using System.Collections;
using UnityEngine;

public class UpDownObstacle : BasicObstacle
{
    public float verticalSpeed = 5f;
    public float changeDirectionTimer = 1;
    private bool isGoindUp = true;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        VerticalMovement();
        HorizontalMovement();
    }

    void VerticalMovement()
    {
        if(isGoindUp)
            transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime);
        else
            transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime);
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDirectionTimer);
            if(isGoindUp)
                isGoindUp = false;
            else
                isGoindUp = true;
        }
    }
}
