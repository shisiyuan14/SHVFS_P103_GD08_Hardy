using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawnerComponent : InteractableComponentBase
{

    public SpawnerComponent prefab;
    [SerializeField] private Transform spawnPosition;
    public bool isSpawned=false;
    private SpawnerComponent spawndGameObject;

    public override int Priority => 2;

    //public IngredientSpawnerComponent Spawner;
    public override bool Interact(InteractorComponent interactor)
    {
        if (interactor.HoldingObject != null) return false;
        if(!isSpawned)
        {
            Debug.Log("Spawn the food!");
            spawndGameObject = Instantiate(prefab, spawnPosition);
            spawndGameObject.OnHolding += () => { isSpawned = false; };
            isSpawned = true;
            return true;
        }
        else
        {
            Debug.Log("Already Spawned!");
            return false;
            //spawndGameObject.transform.SetParent(interactor.HandPoistion);
        }
       
    }

}
