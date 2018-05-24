using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cricket : MonoBehaviour {


    public Animator animator;
    public float damage;
    public float fireRate = 15f;
    private float nextTimetoFire = 0F;

    void OnEnable()
    {

        animator.SetBool("Shooting", false);
    }

    // Update is called once per frame
    void Update () {
        animator.SetBool("Shooting", false);

        if (Input.GetButton("Fire1") && Time.time >= nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1f / fireRate;
            Shoot();
        }

    }
    void Shoot()
    {
        animator.SetBool("Shooting", true);
    }
}
