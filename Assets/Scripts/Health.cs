using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    ShieldManager shieldManager;
    ForceFieldManager forceFieldManager;
    CircleCollider2D bodyCollider;
    PowerupCreator powerupCreator;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        shieldManager = FindObjectOfType<ShieldManager>();
        forceFieldManager = FindObjectOfType<ForceFieldManager>();
        bodyCollider = GetComponent<CircleCollider2D>();
        powerupCreator = FindAnyObjectByType<PowerupCreator>();
    }
    public void HitForceField() 
    {
        if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Force Field")) && !isPlayer) 
        {
            Destroy(gameObject, 0.2f);
        }
    }
 
    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if(damageDealer != null)
        {
            if(forceFieldManager.forceFieldEnabled && isPlayer)
            {
                if(shieldManager.shieldEnabled && isPlayer)
                    ShieldDamage(0);
                else
                    BodyDamage(0);
            }
            else
            {
                if(shieldManager.shieldEnabled && isPlayer)
                    ShieldDamage(damageDealer.GetDamage());
                else
                    BodyDamage(damageDealer.GetDamage());
            }
            PlayHitEffect();
            ShakeCamera();
            audioPlayer.PlayDamageClip();
            if(damageDealer.tag != "Asteroid") 
            {
                damageDealer.Hit();
            }
        }
        if(other.tag == "Red Pills")
        {
            RecoverHealth();
            Destroy(other.gameObject, 0.2f);
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            powerupCreator.SpawnTimer = 0f;
        }
        if(other.tag == "Yellow Pills")
        {
            shieldManager.shieldHealth = 50;
            shieldManager.shieldEnabled = true;
            shieldManager.shield.gameObject.SetActive(true);
            Destroy(other.gameObject, 0.2f);
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            powerupCreator.SpawnTimer = 0f;
        }
        if(other.tag == "Green Pills")
        {
            forceFieldManager.forceFieldEnabled = true;
            forceFieldManager.forceField.gameObject.SetActive(true);
            Destroy(other.gameObject, 0.2f);
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            powerupCreator.SpawnTimer = 0f;
        }
    }

    public int GetHealth()
    {
        return health;
    }

    void ShieldDamage(int damage)
    {
        shieldManager.shieldHealth -= damage;
        if(shieldManager.shieldHealth <= 0)
        {
            shieldManager.shieldEnabled = false;  
        }        
    }

    void BodyDamage(int damage) 
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    void RecoverHealth() 
    {
        if(health <= 30)
            health += 20;
        else
            health = 50;
    }
    void Die() 
    {
        if(!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);        
    }

    void PlayHitEffect() 
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera() 
    {
        if(cameraShake != null && applyCameraShake) 
        {
            cameraShake.Play();
        }
    }
}
