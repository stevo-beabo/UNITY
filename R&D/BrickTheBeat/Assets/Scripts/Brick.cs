using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hits = 1;
    public int points = 100;
    public Vector3 rotator;
    public Material hitMaterial;

    Material _orgMaterial;
    Renderer _renderer;

    void Start()
    {
        // Adds uniqueness to the bricks so they rotate in a pattern instead of unison.
        transform.Rotate(rotator * (transform.position.x * transform.position.y) * 0.01f);

        // Set up orginial material so it can be reverted to after hitMaterial triggers flash effect
        _renderer = GetComponent<Renderer>();
        _orgMaterial = _renderer.sharedMaterial;
    }
    void Update()
    {
        // Creates a rottating effect
        transform.Rotate(rotator * Time.deltaTime);
    }

    // Method controlling what happens to the Brick on collision
    private void OnCollisionEnter(Collision collision)
    {
        // Lowers the 'hits' variable by 1
        hits--;
        // Destroys Bricks when the 'hits' variable equals 0
        if (hits <= 0)
        {
            GameManager.Instance.Score += points;
            Destroy(gameObject);
        }
        // Changes the material to the 'BrickHit' material
        _renderer.sharedMaterial = hitMaterial;
        // Restores to the original 'Brick' material after 0.05 seconds
        Invoke("RestoreMaterial", 0.05f);
    }

    // Method intended to restore the Brick's material to the original material
    void RestoreMaterial()
    {
        _renderer.sharedMaterial = _orgMaterial;
    }
}

