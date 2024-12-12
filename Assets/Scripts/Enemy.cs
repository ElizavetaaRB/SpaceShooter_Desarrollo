using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float velocidad;
    [SerializeField] private Shot shotPrefab;


    [SerializeField] private Transform[] spawnpoints;
    private ObjectPool<Shot> pool;


    private void Awake()
    {
        pool = new ObjectPool<Shot>(CreateShot, null, ReleaseShot, DestroyShot);

    }

    private void DestroyShot(Shot shot)
    {
        Destroy(shot.gameObject);
    }

    private void ReleaseShot(Shot shot)
    {
        shot.gameObject.SetActive(false);
    }


    private Shot CreateShot()
    {
        Shot shotcopyEnemy = Instantiate(shotPrefab, transform.position, Quaternion.identity);
        shotcopyEnemy.Mypool = pool;
        return shotcopyEnemy;
    }



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shotsEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector2(-1,0) * velocidad * Time.deltaTime);

    }


    IEnumerator shotsEnemy()
    {
        while (true) {
            Shot copyshot = pool.Get();
            copyshot.gameObject.SetActive(true);
            copyshot.transform.position = spawnpoints[0].transform.position;
            copyshot = pool.Get();
            copyshot.gameObject.SetActive(true);
            copyshot.transform.position = spawnpoints[1].transform.position;
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
    }
}
