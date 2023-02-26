using System.Linq;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    RaycastHit fwdhit;
    RaycastHit bwdhit;
    RaycastHit lefthit;
    RaycastHit righthit;
    [SerializeField]
    protected float player_speed = 5, jump_height = 2;
    [SerializeField]
    protected float rotateSpeed;

    [SerializeField]
    protected PlayerinputComponent PlayerinputComponent;

    protected Vector3 movementDirection;

    [SerializeField]
    private float playerWidth;
    [SerializeField]
    private float playerHeight;

    private float movementDistance => player_speed * Time.deltaTime;
    private void Update()
    {


        /*movementDirection = new Vector3(PlayerinputComponent.GetInputDirectionNormalized().x, 0f, PlayerinputComponent.GetInputDirectionNormalized().y).normalized;
        Vector3 fwd = movementDirection;
        Physics.Raycast(transform.position, fwd, out fwdhit, 1);

        if (PlayerinputComponent.GetInputDirectionNormalized().magnitude >= 0)
        {
            if (Vector3.Dot(movementDirection, fwdhit.normal) >= 0)
            {

                var targetPosition = transform.position + player_speed * movementDirection * Time.deltaTime;
                var targetLookRotation = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotateSpeed);
                transform.SetPositionAndRotation(targetPosition, Quaternion.LookRotation(targetLookRotation, Vector3.up));
            }
        }

        Vector3 tangent = new Vector3(fwdhit.normal.z, fwdhit.normal.y, -fwdhit.normal.x);
        if (PlayerinputComponent.GetInputDirectionNormalized().magnitude >= 0)
        {
            if (Vector3.Dot(movementDirection, tangent) > 0)
            {
                movementDirection = tangent;
                var targetPosition = transform.position + player_speed * Vector3.Dot(movementDirection, tangent) * movementDirection * Time.deltaTime;
                var targetLookRotation = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotateSpeed);
                transform.SetPositionAndRotation(targetPosition, Quaternion.LookRotation(targetLookRotation, Vector3.up));
            }
            else if (Vector3.Dot(movementDirection, tangent) < 0)
            {
                movementDirection = -tangent;
                var targetPosition = transform.position + player_speed * -1 * Vector3.Dot(movementDirection, tangent) * movementDirection * Time.deltaTime;
                var targetLookRotation = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotateSpeed);
                transform.SetPositionAndRotation(targetPosition, Quaternion.LookRotation(targetLookRotation, Vector3.up));
            }
        }*/

        //Chris's way:
        if (!(PlayerinputComponent.GetInputDirectionNormalized().magnitude > 0f)) return;
        var movementDirection = new Vector3(PlayerinputComponent.GetInputDirectionNormalized().x, 0f, PlayerinputComponent.GetInputDirectionNormalized().y);
        // This is the wrong way to use lerp/slerp, use it in production with caution
        // Idealy, we calculate a difference in current versus target rotation/forward, a time based on the magnitude of this difference, and then interpolate/tween/ease
        var targetLookRotation = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotateSpeed);
        transform.rotation = Quaternion.LookRotation(targetLookRotation, Vector3.up);

        //TODO Refactor, make DRY, solve issue with colliding being blocked by ball..
        //var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, movementDirection, movementDistance);



        //if(canMove)
        //{
        //    var targetPosition = transform.position + player_speed * movementDirection * Time.deltaTime;
        //    transform.position = targetPosition;
        //    return;
        //}

        if (TryMove(movementDirection)) return;

        if (TryMove(new Vector3(movementDirection.x, 0f, 0f).normalized)) return;

        TryMove(new Vector3(0f, 0f, movementDirection.z).normalized);
        //var movementDirectionX = new Vector3(movementDirection.x, 0f, 0f).normalized;
        //var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, movementDirectionX, movementDistance);

        //if(canMove)
        //{
        //    var targetPosition = transform.position + player_speed * movementDirectionX * Time.deltaTime;
        //    transform.position = targetPosition;
        //    return;

        //}

        //var movementDirectionZ = new Vector3(0f, 0f, movementDirection.z).normalized;
        //var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, movementDirectionZ, movementDistance);

        //if(canMove)
        //{
        //    var targetPosition = transform.position + player_speed * movementDirectionZ * Time.deltaTime;
        //    transform.position = targetPosition;
        //}
        


    }

    private bool TryMove(Vector3 direction)
    {
        var hits = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, direction, movementDistance);

        //Limit nesting it makes it hard to follow what's going on 
        if (hits.Length >= 1)
        {
            if (hits.All(hit => hit.transform.GetComponent<StructureComponent>() == null))

                Move(direction);
                
            return true;

            return false;
        }
        //if (!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, direction,out hit, movementDistance)|| hit.collider.GetComponent<Rigidbody>())
        //{
        //    var targetPosition = transform.position + player_speed * direction * Time.deltaTime;
        //    transform.position = targetPosition;
        //    return true;
        //}

        //return false;
        Move(direction);
        return true;
    }

    private void Move(Vector3 direction)
    {
        var targetPosition = transform.position + player_speed * direction * Time.deltaTime;
        transform.position = targetPosition;
    }

    
}

    
    
        
    


