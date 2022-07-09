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

    public float projectileForce;
    public int fireRate;
    public float timeSinceLastFire;

    public int maxIterations = 10000;
    public int maxSegmentCount = 300;
    public float segmentStepModulo = 10f;

    private Vector3[] segments;
    private int numSegments = 0;

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

        float stepDrag = 1 - timestep;
        Vector3 velocity = firePoint.transform.forward / ((projectileForce / 5) * timestep);
        Vector3 gravity = Physics.gravity * timestep * timestep;
        Vector3 position = firePoint.transform.position;

        if (segments == null || segments.Length != maxSegmentCount)
        {
            segments = new Vector3[maxSegmentCount];
        }

        segments[0] = position;
        numSegments = 1;

        for (int i = 0; i < maxIterations && numSegments < maxSegmentCount && position.y > 0f; i++)
        {
            velocity += gravity;
            velocity *= stepDrag;

            position += velocity;

            if (i % segmentStepModulo == 0)
            {
                segments[numSegments] = position;
                numSegments++;
            }
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
    }

}
