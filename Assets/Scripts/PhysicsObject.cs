﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float gravityModifier = 1f;
    public float minGroundNormalY = .65f;

    protected Vector2 groundNormal;
    protected bool isGrounded;
    protected Vector2 velocity;
    protected Rigidbody2D rb2d;
    protected const float minMoveDistance = 0.001f;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected const float shellRadius = 0.01f;
    protected List<RaycastHit2D>hitBufferList = new List<RaycastHit2D>(16);
    
    void OnEnable(){
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start(){
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate(){
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        isGrounded = false;
        Vector2 deltaPosition = velocity * Time.deltaTime;
        Vector2 move = Vector2.up * deltaPosition.y;
        Movement(move,true);
    }

    void Movement (Vector2 move, bool yMovement){
        float distance =  move.magnitude;
        if (distance > minMoveDistance){
        int count = rb2d.Cast(move,contactFilter, hitBuffer, distance + shellRadius);
        hitBufferList.Clear();
        for (int i = 0; i < count; i++){
            hitBufferList.Add(hitBuffer[i]);
        }
        for (int i = 0; i < count;i++){
            Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY){
                    isGrounded = true;
                    if (yMovement){
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
            
            float projection = Vector2.Dot(velocity, currentNormal);
            if (projection < 0){
                velocity = velocity - projection * currentNormal;
            }
            float modifiedDistance = hitBufferList[i].distance - shellRadius;
            distance = modifiedDistance < distance ? modifiedDistance: distance;
            }
        }
        rb2d.position += move;
    }
}
