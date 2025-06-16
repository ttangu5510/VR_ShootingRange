using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool isLoaded;
    public int loadedBullet;
    protected ParticleSystem fireFlash;
    protected ParticleSystem outCartridge;
    
    void Start()
    {
       isLoaded = false;
        loadedBullet = 0;
    }


}
