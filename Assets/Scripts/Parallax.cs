using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;// se moverá a la izquierda 
    [SerializeField] private float ImageAn;
    private Vector3 InicialPos;



    // Start is called before the first frame update
    void Start()
    {
        InicialPos = this.gameObject.transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        //1. Calcular el espacio (espacio = velocidad * tiempo) y hay que contabilizar el espacio por el ancho de imagen
            // cuando  transcurre un ancho de imagen (la diferencia entre el centro de imagen/camara del padre y el centro de imagen/camara de la hija)
            // se habrá cumplido un ciclo y tenemos que volver al comienzo . Es decir el ancho de imagen es el valor del hijo que tenga la x en position 
        float space = speed * Time.time;

        float rest = space % ImageAn; // cuanto me queda de recorrido para alcanzar un nuevo ciclo. 0 = nuev0 ciclo 

        transform.position = InicialPos + rest * direction; // refrescar posicion desde la pos inicial sumando tanto como resto me quede en mi direccion deseada










    }
}
