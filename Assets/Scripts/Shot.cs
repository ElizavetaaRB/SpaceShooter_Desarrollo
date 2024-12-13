using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shot : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;

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
        transform.Translate(direction*speed*Time.deltaTime);

        // pool 
        timer += Time.deltaTime;
            if (timer >= 2.5f)
        {
            timer = 0;
            mypool.Release(this);
        }
    }
}
