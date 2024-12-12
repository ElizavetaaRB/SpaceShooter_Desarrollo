using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shot : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Vector2 direccion;

    private ObjectPool<Shot> mypool; //intern
    private float timer;

    public ObjectPool<Shot> Mypool { get => mypool; set => mypool = value; } //extern


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direccion*velocidad*Time.deltaTime);

        // pool 
        timer += Time.deltaTime;
            if (timer >= 2.5f)
        {
            timer = 0;
            mypool.Release(this);
        }
    }
}
