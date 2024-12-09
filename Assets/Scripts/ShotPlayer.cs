using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPlayer : MonoBehaviour
{
    [SerializeField] private float velocidad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(new Vector3(1,0,0)*velocidad*Time.deltaTime);
    }
}
