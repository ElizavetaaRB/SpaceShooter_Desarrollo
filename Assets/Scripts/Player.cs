using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float ratioShot;
    [SerializeField] private PowerUp powerUp;
    [SerializeField] private GameObject shield;
    [SerializeField] private Shot shotPrefab;
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private Image[] hearts;
    private ObjectPool<Shot> pool;
    private int numshots = 5;
    private float timer = 0.5f;
    private int lifes = 5;
    private int powerUpLevel = 0;
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




    void Start()
    {
        DeactivateShield();
    }


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
            if (powerUpLevel == 0)
            {
                for (int i = 0; i < 2; i++) //power up cambiar de 2 a 3 para que apunte con 3 cañones y definir otro spawn. el video anterior decir de las probabilidades 
                {
                    Shot copyshot = pool.Get();
                    copyshot.gameObject.SetActive(true);
                    copyshot.transform.position = spawnpoints[i].transform.position;
                }

                timer = 0;
            }
            else if (powerUpLevel == 1)
            {
                for (int i = 0; i < 3; i++) //power up cambiar de 2 a 3 para que apunte con 3 cañones y definir otro spawn. el video anterior decir de las probabilidades 
                {
                    Shot copyshot = pool.Get();
                    copyshot.gameObject.SetActive(true);
                    copyshot.transform.position = spawnpoints[i].transform.position;
                }

                timer = 0;
            }
            else if(powerUpLevel == 2)
            {
                StartCoroutine(MaxPower());
            }

        }
    }

    private IEnumerator MaxPower()
    {
        float gradepershot = 360 / numshots;
        for(float i =0;i < 360; i += gradepershot)
        {
            Shot shotcopypower = pool.Get();
            shotcopypower.gameObject.SetActive(true);
            shotcopypower.transform.position = transform.position;
            shotcopypower.transform.eulerAngles = new Vector3(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }
    }



    private void OnTriggerEnter2D(Collider2D elotro)
    {
        if (elotro.gameObject.CompareTag("ShotEnemy") || elotro.gameObject.CompareTag("Enemy"))
        {
            Destroy(elotro.gameObject);
            if(lifes >= 1)
            {
                hearts[lifes - 1].enabled = false;
            }
            if (HasShield())
            {
                DeactivateShield();
            }
            else
            {
                lifes--;
                ActivateShield();
            }

            if (lifes <= 0)
            {
                Destroy(this.gameObject);
                GameOver();
            }
        }else if (elotro.gameObject.CompareTag("PowerUp"))
        {
            if (powerUp.activateShield)
            {
                ActivateShield();
                powerUpLevel = UnityEngine.Random.Range(0,3);
            }

        } 
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void ActivateShield()
    {
        shield.SetActive(true);
    }
    private void DeactivateShield()
    {
        shield.SetActive(false);
    }

    private bool HasShield()
    {
        return shield.activeSelf;
    }

}
