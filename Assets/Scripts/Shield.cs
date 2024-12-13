using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    [SerializeField] SpriteRenderer sprite;

    // Update is called once per frame
    void Update()
    {
        if(sprite != null)
        {
            sprite.enabled = !sprite.enabled;
        }
    }
}
