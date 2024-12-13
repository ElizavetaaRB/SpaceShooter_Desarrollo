using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;

public class PowerUp : MonoBehaviour
{

    [SerializeField] public bool activateShield;
    // Start is called before the first frame update
    [SerializeField] private GameObject circle;
    private float minspawn = 15f;
    private float maxspawn = 120f;
    private float timer;
    private float secondsspawn;
    private bool isActive = false;
    void Start()
    {
        timer = 0f;
        secondsspawn = UnityEngine.Random.Range(minspawn, maxspawn);
        circle.SetActive(false);
    }
    private void Update()
    {
        if (!isActive)
        {
            timer += Time.deltaTime;
            secondsspawn = UnityEngine.Random.Range(minspawn, maxspawn);
          //  Debug.Log("Timer: "+timer + "Segundos: "+secondsspawn);
            if (timer > secondsspawn)
            {
                
                    float randomX = UnityEngine.Random.Range(-8.4f, 8.4f);
                    float randomY = UnityEngine.Random.Range(-4.5f, 4.5f);

                    this.gameObject.transform.position = new Vector2(randomX, randomY);
                    circle.SetActive(true);
                    isActive = true;
                    timer = 0f;
                
            }
        }
        else
        {
            if(!circle.activeInHierarchy){
                isActive = false; timer = 0f; 
                secondsspawn = UnityEngine.Random.Range(minspawn, maxspawn); 
             //   Debug.Log("Preparando nueva aparición: " + secondsspawn);
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D elotro)
    {
        if (elotro.gameObject.CompareTag("Player"))
        {
            isActive = false;
            circle.SetActive(false);
        //    Debug.Log("CHOCADOOOO");
            timer = 0f;
            secondsspawn = UnityEngine.Random.Range(minspawn, maxspawn);
        }
    }


}
