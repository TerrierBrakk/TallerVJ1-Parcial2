using System.Collections;
using UnityEngine;

public class gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public ParticleSystem particles;
    public GameObject ImpactEffect;
    public float ImpactForce = 200f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;

    private bool IsReloading = false;


    public Camera fpsCam;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

	// Update is called once per frame
	void Update () {
       
        if(IsReloading)
        {
            return;
        }
        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            Reload();
            return;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
		
	}

    IEnumerator Reload()
    {
        IsReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        IsReloading = false;
    }


    void Shoot()
    {
        particles.Play();
        RaycastHit hit;
       if ( Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if( target != null)
            {
                target.TakeDamage(damage);
            }
            if( hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * ImpactForce );
            }
           GameObject Flare= Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(Flare, 1f);
        }
    }
}
