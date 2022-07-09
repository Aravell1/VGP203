using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem explosion;
    public TrailRenderer trail;

    public int lifetime = 2;
    public float life = 0;

    public float blastRadius = 4;
    public int damage = 100;

    // Update is called once per frame
    void Update()
    {
        life += Time.deltaTime;

        if (life >= lifetime)
        {
            Explode();
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            trail.gameObject.SetActive(false);
            Destroy(gameObject, 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        trail.gameObject.SetActive(false);
        Destroy(gameObject, 1);
    }

    private void Explode()
    {
        explosion.Play();

        Collider[] Colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider collider in Colliders)
        {
            float dist = Vector3.Distance(transform.position, collider.transform.position);
            if (dist < 1f)
                dist = 1f;

            if (collider.gameObject.tag == "Enemy")
            {
                collider.gameObject.GetComponent<Enemies>().health -= (int)Mathf.Ceil(damage / dist);
            }
        }
    }
}
