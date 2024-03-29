﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManuevor : MonoBehaviour
{
    public Vector2 startWait;
    public float dodge;
    public Vector2 manueverTime;
    public Vector2 manueverWait;
    public Boundary boundary;
    public float tilt;
    public float smoothing;

    private float targetManeuver;
    private Rigidbody rb;
    private float currentSpeed;

    void Start()
    {
        StartCoroutine (Evade ());
        rb = GetComponent <Rigidbody> ();
        currentSpeed = rb.velocity.z;
    }


    IEnumerator Evade()
    {
        yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range (1, dodge * -Mathf.Sign (transform.position.x));
            yield return new WaitForSeconds (Random.Range (manueverTime.x, manueverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds (Random.Range (manueverWait.x, manueverWait.y));
        }

    }

    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
        rb.position = new Vector3 
        (
            Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
