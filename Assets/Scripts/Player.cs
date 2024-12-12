using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float ratioShot;



    [SerializeField] private Shot shotPrefab;
    [SerializeField] private Transform[] spawnpoints;

    private ObjectPool<Shot> pool;

    private float timer = 0.5f;
    private int vidas = 5;


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
       Shot shotcopy1 = Instantiate(shotPrefab,transform.position, Quaternion.identity);
       shotcopy1.Mypool = pool;
        return shotcopy1;
    }



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
        transform.Translate( new Vector2(inputH,inputV).normalized * speed * Time.deltaTime);


        //Delimitar
        float XDelimitada = math.clamp(transform.position.x, -8.4f, 8.4f);
        float YDelimitada = math.clamp(transform.position.y, -4.5f, 4.5f);
        this.gameObject.transform.position = new Vector2(XDelimitada, YDelimitada);

        Disparar();

    }



    void Disparar()
    {
        timer += 1 * Time.deltaTime;
        if ((Input.GetKey(KeyCode.Space)) &&  timer > ratioShot)
        {
            for (int i = 0; i < 2; i++) //power up cambiar de 2 a 3 para que apunte con 3 cañones y definir otro spawn. el video anterior decir de las probabilidades 
            {
                Shot copyshot = pool.Get();
                copyshot.gameObject.SetActive(true);
                copyshot.transform.position=spawnpoints[i].transform.position;
            }
            
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
