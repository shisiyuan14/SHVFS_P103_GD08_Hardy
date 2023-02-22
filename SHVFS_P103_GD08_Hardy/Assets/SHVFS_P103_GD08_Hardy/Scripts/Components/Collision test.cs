using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisiontest : MovementComponent
{
    RaycastHit hit;
    private void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Physics.Raycast(transform.position,fwd,out hit, 1);
        if(Vector3.Dot(movementDirection, hit.normal)>=0)
        {
            movementDirection = new Vector3(PlayerinputComponent.GetInputDirectionNormalized().x, 0f, PlayerinputComponent.GetInputDirectionNormalized().y).normalized;
            var targetPosition = transform.position + player_speed * movementDirection * Time.deltaTime;
            var targetLookRotation = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotateSpeed);
            transform.SetPositionAndRotation(targetPosition, Quaternion.LookRotation(targetLookRotation, Vector3.up));
        }
    }





}
