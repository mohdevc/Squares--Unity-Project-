using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _enemySpeed;
    [SerializeField] ParticleSystem _enemyCrash;
    int _enemyHealth = 100;
    Vector2 _enemyDirection = Vector2.right;
    AudioSource _audioSource;
    public Player _player;

    void Update()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        transform.Translate(_enemyDirection * _enemySpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        HitWall(other);
        CrashPlayer(other);

    }

    private void CrashPlayer(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _enemyCrash.Play();
            _audioSource.Play();
            StartCoroutine(EnemyEnds());
        }
        else
        {
            _enemyCrash.Stop();
        }
        // if (other.gameObject.CompareTag("Enemy"))
        // {
        //     _enemyHealth -= 50;
            
        //     if (_enemyHealth <= 0)
        //     {
        //         StartCoroutine(EnemyEnds());
        //     }
        // }
    }
    IEnumerator EnemyEnds()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);

    }

    void HitWall(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy") )
        {
            _enemyHealth -= 25;
            if (_enemyHealth <= 0)
            {
                StartCoroutine(EnemyEnds());
            }
            if (transform.position.x < 0)
            {
                _enemyDirection = Vector2.right;
            }
            else
            {
                _enemyDirection = Vector2.left;
            }
        }
    }


}
