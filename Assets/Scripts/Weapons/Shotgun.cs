using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : IWeapon
{
    public void Start()
    {
        if (projectileForce <= 0)
            projectileForce = 100;
        if (fireRate <= 0)
            fireRate = 1;
        if (timeSinceLastFire <= 0)
            timeSinceLastFire = fireRate;
        reloadBar.maxValue = fireRate;
        reloadBar.gameObject.SetActive(false);

        Enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (timeSinceLastFire >= fireRate)
            {
                Rigidbody temp = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                temp.AddForce(firePoint.transform.forward * projectileForce, ForceMode.Impulse);
                timeSinceLastFire = 0;
            }
        }
        if (Input.GetButtonDown("Fire2"))
            Enabled = !Enabled;

        if (Enabled)
            SimulatePath();
        else if (hitTarget.activeSelf == true)
            hitTarget.SetActive(false);

        if (timeSinceLastFire < fireRate)
        {
            timeSinceLastFire += Time.deltaTime;
            reloadBar.value = timeSinceLastFire;
            if (reloadBar.gameObject.activeSelf == false)
                reloadBar.gameObject.SetActive(true);
        }
        else if (timeSinceLastFire >= fireRate && reloadBar.gameObject.activeSelf == true)
            reloadBar.gameObject.SetActive(false);
    } 
}
