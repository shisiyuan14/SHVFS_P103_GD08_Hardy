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
    private void Update()
    {
        movementDirection = new Vector3(PlayerinputComponent.GetInputDirectionNormalized().x, 0f, PlayerinputComponent.GetInputDirectionNormalized().y).normalized;
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
            else if(Vector3.Dot(movementDirection, tangent) < 0)
            {
                movementDirection = -tangent;
                var targetPosition = transform.position + player_speed * -1       *  Vector3.Dot(movementDirection, tangent) * movementDirection * Time.deltaTime;
                var targetLookRotation = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotateSpeed);
                transform.SetPositionAndRotation(targetPosition, Quaternion.LookRotation(targetLookRotation, Vector3.up));
            }
        }
       



    }

    
}
