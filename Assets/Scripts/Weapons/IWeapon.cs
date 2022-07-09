using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IWeapon : MonoBehaviour
{
    public Rigidbody projectilePrefab;
    public Transform firePoint;
    public Player player;
    public LineRenderer lr;
    public Slider reloadBar;
    public GameObject hitTarget;

    public float projectileForce;
    public int fireRate;
    public float timeSinceLastFire;

    public int maxSegmentCount = 300;
    public float segmentStepModulo = 1f;

    private Vector3[] segments;
    private int numSegments = 0;
    public bool rayHit = false;

    public bool Enabled
    {
        get
        {
            return lr.enabled;
        }
        set
        {
            lr.enabled = value;
        }
    }

    public void SimulatePath()
    {
        float timestep = Time.fixedDeltaTime;

        Vector3 velocity = firePoint.transform.forward / (projectileForce * timestep);
        Debug.Log(velocity);
        Vector3 gravity = Physics.gravity * timestep * timestep;
        Vector3 position = firePoint.transform.position;

        if (segments == null || segments.Length != maxSegmentCount)
        {
            segments = new Vector3[maxSegmentCount];
        }

        segments[0] = position;
        numSegments = 1;

        for (int i = 1; i < maxSegmentCount && position.y > 0f; i++)
        {
            velocity += gravity;

            position += velocity;

            segments[i] = position;
            numSegments = i;

            if (Physics.Raycast(firePoint.position, position - firePoint.position, Vector3.Distance(position, firePoint.position)))
            {
                rayHit = true;
                break;
            }
            else
                if (rayHit == true)
                    rayHit = false;
        }

        Draw();
    }

    private void Draw()
    {
        lr.transform.position = segments[0];

        lr.positionCount = numSegments;
        for (int i = 0; i < numSegments; i++)
        {
            lr.SetPosition(i, segments[i]);
        }

        if (rayHit == true && timeSinceLastFire >= fireRate)
        {
            if (hitTarget.activeSelf == false)
                hitTarget.SetActive(true);
            hitTarget.transform.position = segments[numSegments];
        }
        else
            if (hitTarget.activeSelf == true)
                hitTarget.SetActive(false);
    }

}
