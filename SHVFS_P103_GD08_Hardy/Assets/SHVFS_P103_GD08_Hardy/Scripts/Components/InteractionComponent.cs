using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Analytics;
using UnityEngine;



public class InteractionComponent : MonoBehaviour
{
    private PlayerActions playerActions;
    [SerializeField]
    protected float interactMultiplier;
   
    private float interacttDistance => interactMultiplier * Time.deltaTime;

    [SerializeField]
    protected PlayerinputComponent PlayerinputComponent;

    

    [SerializeField]
    private float playerWidth;
    [SerializeField]
    private float playerHeight;
    private void Awake()
    {
        playerActions = new PlayerActions();
        playerActions.PlayerInput.Enable();

    }

    private void Update()
    {
        if (playerActions.PlayerInput.InteractPrimary.WasPressedThisFrame())
        {
          
            TryInteract();
        }
    }

    private void TryInteract()
    {
        var hits = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, transform.forward, interacttDistance);

        Debug.Log("1");

        if (hits.Length < 1) return;

        foreach (var hit in hits)
        {
            Debug.Log($"2: {hit.transform.name}");

            var interactable = hit.transform.GetComponent<InteractableComponentBase>();

            if (interactable == null) return;

            interactable.Interact();
            Debug.Log("3");
        }
    }
}
