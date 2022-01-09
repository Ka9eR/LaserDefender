using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLivetime = 5f;
    [SerializeField] float baseFireRate = 0.5f;

    [Header("AI")]
    [SerializeField] float minimumFireRate = 0.1f;
    [SerializeField] float fireRateVariance = 0f;
    [SerializeField] bool useAI;

    [HideInInspector] public bool isFiring;
    
    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
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
        if (isFiring && firingCoroutine == null)
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
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            float timeToNextProjectile = Random.Range(baseFireRate - fireRateVariance, baseFireRate + fireRateVariance);

            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLivetime);

            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFireRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
