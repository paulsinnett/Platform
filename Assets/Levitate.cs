using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate : MonoBehaviour
{
    public float speed = 1.0f;
    public float height = 10.0f;
    public float maxAcceleration = 15.0f;
    public float minAcceleration = 5.0f;
    public float alternate = 5.0f;

    Rigidbody physics;
    float target;

    private void Start()
    {
        physics = GetComponent<Rigidbody>();
        target = 0;
        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        while (true)
        {
            yield return new WaitForSeconds(alternate);
            target = height - target;
        }
    }

    private void FixedUpdate()
    {
        float force = CalculateForce();
        physics.AddForce(Vector3.up * force);
    }

    float CalculateForce()
    {
        float F = 0.0f;
        float m = physics.mass;
        float t = Time.deltaTime;
        if (t > 0.0f)
        {
            float v = (target - transform.position.y) * speed;
            float u = physics.velocity.y;
            float a = (v - u) / t;
            F = m * Mathf.Clamp(a, minAcceleration, maxAcceleration);
        }
        return F;
    }
}
