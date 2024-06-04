using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    // This is the movement script for our player.
    // This is a clone of geometry dash so player must be moving forward at all times.
    // Player can only jump and fall down to avoid spikes and obstacles.
    // If we die, we will restart the level.

    private Rigidbody2D _rigidbody2D;
    private bool _isJumping;
    [Range(0,5)]
    public float jumpForce = 4f;
    [Range(0,5)]
    public float speed = 3.5f;

    //TODO:Make speed boost that change this variable x1 x2 x3 x4
    //TODO: Make jump boost x1 x2 x3 x4
    //TODO:goofy ass physics cube
    //TODO: Put spheres, falling things and weird stuff

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && Physics2D.Raycast(transform.position, Vector2.down, 0.1f))
        {
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 180), 0.5f);
            _isJumping = true;
        }
        else
        {
            _isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag($"Speed x1"))
        {
            speed = 2f;
            Destroy(other.GetComponent<SpriteRenderer>());
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
