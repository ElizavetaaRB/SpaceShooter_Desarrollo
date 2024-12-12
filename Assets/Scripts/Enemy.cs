using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;



public class Enemy : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Shot shotPrefab;
    [SerializeField] private Transform[] spawnpoints;

    private static ObjectPool<Shot> shotPool;  // Static to ensure all instances share the same pool
    private static bool isPoolInitialized = false;
    private int poolsize = 50;

    private void Awake()
    {
        // Inicializar el pool solo una vez, cuando sea necesario
        if (!isPoolInitialized)
        {
            InitializeShotPool();
            isPoolInitialized = true;
        }
    }

    private void InitializeShotPool()
    {
        shotPool = new ObjectPool<Shot>(CreateShot, OnGetShot, ReleaseShot, DestroyShot);

        // Prellenar el pool con disparos inactivos
        for (int i = 0; i < poolsize; i++)
        {
            Shot shot = shotPool.Get();
            shotPool.Release(shot);
        }
    }

    private Shot CreateShot()
    {
        Shot shotcopyEnemy = Instantiate(shotPrefab);
        shotcopyEnemy.gameObject.SetActive(false);
        shotcopyEnemy.Mypool = shotPool;
        return shotcopyEnemy;
    }

    private void OnGetShot(Shot shot)
    {
        if (shot != null)
        {
            shot.gameObject.SetActive(true);
        }
    }

    private void ReleaseShot(Shot shot)
    {
        if (shot != null)
        {
            shot.gameObject.SetActive(false);
        }
    }

    private void DestroyShot(Shot shot)
    {
        if (shot != null)
        {
            Destroy(shot.gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(shotsEnemy());
    }

    void Update()
    {
        this.transform.Translate(new Vector2(-1, 0) * velocidad * Time.deltaTime);
    }

    private IEnumerator shotsEnemy()
    {
        while (true)
        {
            for (int i = 0; i < spawnpoints.Length; i++)
            {
                Shot copyshot = shotPool.Get();
                if (copyshot != null)
                {
                    copyshot.transform.position = spawnpoints[i].position;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D elotro)
    {
        if (elotro.gameObject.CompareTag("ShotPlayer"))
        {
            Destroy(elotro.gameObject);
            Destroy(this.gameObject);
        }
        else if (elotro.gameObject.CompareTag("Limit"))
        {
            Shot shot = elotro.GetComponent<Shot>();
            if (shot != null)
            {
                shotPool.Release(shot);  // Liberar el disparo al pool
            }

            Destroy(this.gameObject);
        }
    }


}

