using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    ParticleSystem fog;

    // Start is called before the first frame update
    void Start()
    {
        fog = GetComponent<ParticleSystem>();
        InvokeRepeating("ChangeColor",1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeColor()
    {
        fog.startColor = Random.ColorHSV(0.5f, 0.8f, 1f, 1f, 1f, 1f, 0.25f, 0.3f);

    } 
}
