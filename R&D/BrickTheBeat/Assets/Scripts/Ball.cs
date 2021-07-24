using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Function that controls how the BALL game object behaves during gameplay.
public class Ball : MonoBehaviour
{
    // Setting variables
    float _speed = 20f;
    Rigidbody _rigidbody;
    Vector3 _velocity;
    Renderer _renderer;

    void Start()
    {
        // Initializing variables
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        // Delays the the initial launch of the ball object
        Invoke("Launch", 0.5f);
    }

    void Launch()
    {
        // Launches the ball at the defined speed and direction. Default direciton is upwards
        _rigidbody.velocity = Vector3.up * _speed;
    }

    void FixedUpdate()
    {
        // Initializing the velocity variables of the ball
        _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;
        _velocity = _rigidbody.velocity;

        // Function to destroy the BALL gameobject and reduce the Ball once it has goes passed out view.
        // Currently during testing this does not trigger if the ball is still able to be seen in either the "Game" Window  or the "SCENE" Window in Unity.
        if (!_renderer.isVisible)
        {
            // Reducing the Balls stat once ball leaves the screen
            GameManager.Instance.Balls--;
            // Destroys the BALL gameobject
            Destroy(gameObject);
        }
    }

    // Private function that controls how the BALL gameobject bounces off of other objects
    private void OnCollisionEnter(Collision collision)
    {
        // Controlling the velocity of the BALL gameobject after it has collided with another object.
        _rigidbody.velocity = Vector3.Reflect(_velocity, collision.contacts[0].normal);
    }
}
