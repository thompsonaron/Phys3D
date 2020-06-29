using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : Collisions
{
    public float mass = 0.5f;
    public float drag = 0.2f;

    PlayerController playerController;
    Position p, p0;
    Velocity v0, v;
    float _projectileAngleX, _projectileAnglelY;
    bool isGrounded = false;
    [Range(0, 90)]
    public float angleX, angleY;
    float degToRad = Mathf.PI / 180;
    public float projectileForce = 15f;
    public float bounciness = 0f;

    //[Range(0, 180)]
    //public float angleY;
    private void Awake()
    {
       // mass = 0.5f;
        solids = GameObject.FindGameObjectsWithTag("Wall");
        liquids = GameObject.FindGameObjectsWithTag("Liquid");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        p0 = new Position(transform.position.x, transform.position.y, transform.position.z);
        p = p0;
        v0 = new Velocity(0, 0, 0);
        v = v0;

        // Shooting force (movement "added") - works w up to 90 degreess
        v0.x = projectileForce + playerController.v.x;
        v0.y = projectileForce + playerController.v.y;
        v0.z = projectileForce + playerController.v.z;

        // ANGLE ON WHICH THE BULLET IS SHOOTED
        _projectileAngleX = angleX * degToRad;
        _projectileAnglelY = angleY * degToRad;

        v.x = v0.x * Mathf.Cos(_projectileAngleX) * Mathf.Cos(_projectileAnglelY);
        v.y = v0.y * Mathf.Sin(_projectileAngleX);
        v.z = v0.z * Mathf.Cos(_projectileAngleX) * Mathf.Sin(_projectileAnglelY);
    }

    private void Update()
    {
        Collisions();

        p.x = p0.x + v0.x * Time.deltaTime;
        p.z = p0.z + v.z * Time.deltaTime;
        transform.position = new Vector3(p.x, p.y, p.z);

        // Drag
        if (isGrounded)
        {
            if (v.x > drag) { v.x -= drag; }
            if (v.x < -drag) { v.x += drag; }
            if (v.x >= -drag && v.x <= drag) { v.x = 0; }
            if (v.z > drag) { v.z -= drag; }
            if (v.z < -drag) { v.z += drag; }
            if (v.z >= -drag && v.z <= drag) { v.z = 0; }
        }

        p0 = p;
        v0 = v;
    }

    private void Collisions()
    {
        if (CollidingBottomYAxis(gameObject))
        {
            isGrounded = true;
            v.y = -v.y * 0.9f;
        }
        else
        {
            // Fall
            v.y = v0.y - PhyConstants.gravity * mass * Time.deltaTime;
            p.y = p0.y + v.y * Time.deltaTime;
        }
        if (CollidingTopYAxis(gameObject)) { v.y = -v.y * bounciness; }
        if (CollidingLeft(gameObject)) { v.x = -v.x * bounciness; }
        if (CollidingRight(gameObject)) { v.x = -v.x * bounciness; }
        if (CollidingBottomZAxis(gameObject))
        {
            v.z = -v.z* bounciness;
            //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 0.05f);
        }
        if (CollidingTopZAxis(gameObject))
        {
            v.z = -v.z * bounciness;
            //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.05f);
        }
    }
}
