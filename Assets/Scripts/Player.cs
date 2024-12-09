using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float ratioDisparo;



    [SerializeField] private GameObject disparoPrefab;
    [SerializeField] private GameObject spawnpoint1;
    [SerializeField] private GameObject spawnpoint2;

    private float timer = 0.5f;
    private int vidas = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento
        float inputV = Input.GetAxisRaw("Vertical");
        float inputH = Input.GetAxisRaw("Horizontal");
        transform.Translate( new Vector2(inputH,inputV).normalized * velocidad * Time.deltaTime);


        //Delimitar
        float XDelimitada = math.clamp(transform.position.x, -8.4f, 8.4f);
        float YDelimitada = math.clamp(transform.position.y, -4.5f, 4.5f);
        this.gameObject.transform.position = new Vector3(XDelimitada, YDelimitada,0);

        Disparar();

    }



    void Disparar()
    {
        timer += 1 * Time.deltaTime;
        if ((Input.GetKey(KeyCode.Space)) &&  timer > ratioDisparo)
        {
            Instantiate(disparoPrefab,spawnpoint1.transform.position,Quaternion.identity);
            Instantiate(disparoPrefab, spawnpoint2.transform.position,Quaternion.identity);
            timer = 0;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D elotro)
    {
        if (elotro.gameObject.CompareTag("ShotEnemy") || elotro.gameObject.CompareTag("Enemy"))
        {
            Destroy(elotro.gameObject);
            vidas--;
            if(vidas <= 0)
            {
                Destroy(this.gameObject);
            }
        } 
    }



}
