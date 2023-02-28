using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Analytics;
using UnityEngine;



public class InteractorComponent : MonoBehaviour
{
    private PlayerActions playerActions;
    [SerializeField]
    protected float interactMultiplier;

    private float interacttDistance => interactMultiplier;

    [SerializeField]
    protected PlayerinputComponent PlayerinputComponent;

    [SerializeField]
    protected Transform handPosition;
    public SpawnerComponent HoldingObject { get; private set; }
    [SerializeField]
    private float playerWidth;
    [SerializeField]
    private float playerHeight;
    private void Awake()
    {
        playerActions = new PlayerActions();
        playerActions.PlayerInput.Enable();

        playerActions.PlayerInput.InteractPrimary.started += (parm) => TryInteract();

    }

    //private void Update()
    //{
    //    if (playerActions.PlayerInput.InteractPrimary.WasPressedThisFrame())
    //    {

    //        TryInteract();
    //    }
    //}
    private List<InteractableComponentBase> detectedInteractions = new();
    private void TryInteract()
    {
        var hits = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, transform.forward, interacttDistance);

        //Debug.Log("1");

        if (hits.Length < 1) return;
        foreach (var hit in hits)
        {
            //Debug.Log($"2: {hit.transform.name}");

            var interactable = hit.transform.GetComponents<InteractableComponentBase>();

            if (interactable == null) continue;

            for(int i = 0; i < interactable.Length; i++)
            {
                detectedInteractions.Add(interactable[i]);
            }
            //Debug.Log("3");
        }

        if (detectedInteractions.Count == 0) return;

        var interactions = detectedInteractions.OrderByDescending(interaction => interaction.Priority).ToList();
        foreach(var interaction in interactions)
        {
            if (interaction.Interact(this)) break;
        }
        detectedInteractions.Clear();
    }

    public bool Hold(SpawnerComponent spawner) {
        if (HoldingObject != null) return false;
        spawner.transform.SetParent(handPosition);
        spawner.isHolding = true;
        HoldingObject = spawner;
        return true;
    }

    public SpawnerComponent UnHold(Transform slot)
    {
        if (HoldingObject != null)
        {
            HoldingObject.transform.SetParent(slot,true);
            //HoldingObject.transform.position = slot.position;
            HoldingObject.isHolding=false;
            var spawner = HoldingObject;
            HoldingObject = null;
            return spawner;
        }
        return null;
       
    }
}
