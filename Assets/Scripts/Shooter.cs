using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject projectilePrefab2;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0.5f;
    [SerializeField] float minimumFiringRate = 0.1f;
    [SerializeField] bool usingProjectile;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    PowerupCreator powerupCreator;
    [HideInInspector] public bool projectile2enabled;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        powerupCreator = FindAnyObjectByType<PowerupCreator>();
    }
    void Start()
    {
        if(useAI && usingProjectile)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null) 
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }


    IEnumerator FireContinuously()
    {
        while(true)
        {
            if(projectile2enabled && !useAI)
            {
                GameObject instance = Instantiate(projectilePrefab2, transform.position, Quaternion.identity);
                Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
                if(rb != null) 
                {
                    rb.velocity = transform.up * projectileSpeed;               
                }
                Destroy(instance, projectileLifetime);
                float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
                timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
                
                audioPlayer.PlayShootingClip2();

                yield return new WaitForSeconds(timeToNextProjectile);
            }
            else 
            {
                GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
                if(rb != null) 
                {
                    rb.velocity = transform.up * projectileSpeed;               
                }
                Destroy(instance, projectileLifetime);
                float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
                timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
                
                audioPlayer.PlayShootingClip();

                yield return new WaitForSeconds(timeToNextProjectile);
            
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Speed Up" && !useAI)
        {
            ProjectileSpeedUp();
            Destroy(other.gameObject, 0.2f);
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            powerupCreator.SpawnTimer = 0f;
        }
        if(other.tag == "Projectile 2" && !useAI)
        {
            Destroy(other.gameObject, 0.2f);
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            projectile2enabled = true;
            powerupCreator.SpawnTimer = 0f;
        }                   
    }
    void ProjectileSpeedUp()
    {
        projectileSpeed += 2;
    }
}