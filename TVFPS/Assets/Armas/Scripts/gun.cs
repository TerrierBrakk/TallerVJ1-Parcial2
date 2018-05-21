
using UnityEngine;

public class gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public ParticleSystem particles;
    public GameObject ImpactEffect;
    public float ImpactForce = 200f;

    public Camera fpsCam;

	// Update is called once per frame
	void Update () {
       

        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
		
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
