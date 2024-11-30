using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocidad;
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




    }
}
