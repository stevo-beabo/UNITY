using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruments : MonoBehaviour
{
    public int hits = 1;
    public int points = 1000;
    public Vector3 rotator;

    void Start()
    {
        // Controls the rotation effect
        transform.Rotate(rotator * (transform.position.x * transform.position.y) * 0.01f);
    }
    void Update()
    {
        // Creates a rotating effect
        transform.Rotate(rotator * Time.deltaTime);
    }

    // Method controlling what happens to the Brick on collision
    private void OnCollisionEnter(Collision collision)
    {
        // Lowers the 'hits' variable by 1 and adds 1 to the 'instrumentHit' variable
        GameManager.instrumentHit++;
        hits--;
        // Destroys Bricks when the 'hits' variable equals 0
        if (hits <= 0)
        {
            GameManager.Instance.Score += points;
            Destroy(gameObject);
        }
    }
}

