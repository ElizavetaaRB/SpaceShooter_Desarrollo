using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{


    [SerializeField] private GameObject enemyprefab;
    [SerializeField] private TextMeshProUGUI textooleadas;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnenemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //quiere que dispare en x tiempo haciendo caso de una corutina

    IEnumerator spawnenemy()
    {
        for (int n = 0; n < 5; n++) // niveles
        {

            for (int i = 0; i < 3; i++) //oleadas
            {
                yield return new WaitForSeconds(1f);
                textooleadas.text = "Nivel " + (n + 1) + " - Oleada " + (i+1);
                yield return new WaitForSeconds(3f);
                textooleadas.text = "";
                for (int j = 0; j < 10; j++)// generar enemigos
                {
                    Vector2 randomnumber = new Vector2(transform.position.x, Random.Range(-4.5f, 4.5f));
                    Instantiate(enemyprefab, randomnumber, Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(3f);
        }
        






    }


}
