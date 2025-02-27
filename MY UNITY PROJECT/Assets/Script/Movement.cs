﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Movemnt : MonoBehaviour
{
    public int health;
    public float scalemax;
    public float scalemin;
    public float maxOrbitSpeed;
    public float GrowingSpeed;
    float orbitSpeed;
    private bool IsScaled = false;
    private bool isAlive = true;
    public Transform orbitAnchor;
    Vector3 OrbitDirection;
    Vector3 maxScale;    
    public GameObject explosion;




    void CubeSettings()
    {
        orbitAnchor = Camera.main.transfrom;
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        OrbitDirection = new Vector3(x, y, z);
        orbitSpeed = Random.Range(0.5f, maxOrbitSpeed);
        float scale = Random.Range(scalemin, scalemax);
        transform.localScale = Vector3.zero;
        maxScale = new Vector3(scale, scale, scale);
        transform.localScale = Vector3.zero;




    }
    private void Update()
    {
        RotateCube();
        if (!IsScaled)
        {
            ScaleObject();
        }

    }
    void Start()
    {
        CubeSettings();

    }

    


    
    void ScaleObject()
    {
        if (transform.localScale != maxScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, maxScale, Time.deltaTime * GrowingSpeed);
            IsScaled = true;
        }
    }
    void RotateCube()
        {
            transform.RotateAround(orbitAnchor.position, OrbitDirection, orbitSpeed * Time.deltaTime);
        transform.Rotate(OrbitDirection * 30 * Time.deltaTime);

        }
  
    public bool Hit(int hitdamage)
    {
        health -= hitdamage;
        if (health >= 0 && isAlive)
        {
            StartCoroutine("Destroycube");
            return true;
        }
        return false;

    }


    private IEnumerator DestroyCube()
    {
        isAlive = false;
        Instantiate(explosion, transform.position, Quaternion.identity);
        //GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);



    }
    
}

