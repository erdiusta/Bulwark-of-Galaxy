using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;
    Shooter shooter;

    [HideInInspector] PowerupCreator powerupCreator;
    void Awake() 
    {
        powerupCreator = FindAnyObjectByType<PowerupCreator>();
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        shooter.isFiring = true;
        InitBounds();
    }
    void Update()
    {
        Move();
    }

    void InitBounds() 
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);

        Vector2 startPosition = transform.position;

        transform.position = Vector2.Lerp(startPosition, newPos, moveSpeed);
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    /*void OnFire(InputValue value) 
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }*/

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Blue Pills")
        {
            SpeedUp();
            Destroy(other.gameObject, 0.2f);
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            powerupCreator.SpawnTimer = 0f;
        }
        if (other.tag == "Asteroid")
        {
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
        }
    }

    void SpeedUp()
    {
        moveSpeed += 0.1f;
    }
}
