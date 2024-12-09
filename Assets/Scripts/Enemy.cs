using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float velocidad;
    [SerializeField] private GameObject disparoprefab;
    [SerializeField] private GameObject spawnpoint;
    [SerializeField] private GameObject spawnpoint2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(disparos());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector2(-1,0) * velocidad * Time.deltaTime);

    }


    IEnumerator disparos()
    {
        while (true) {
            Instantiate(disparoprefab, spawnpoint.transform.position, Quaternion.identity);
            Instantiate(disparoprefab, spawnpoint2.transform.position, Quaternion.identity);
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
