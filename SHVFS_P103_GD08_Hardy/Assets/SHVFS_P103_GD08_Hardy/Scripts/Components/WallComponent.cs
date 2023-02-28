using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallComponent : InteractableComponentBase
{
    public override int Priority => 1;
    [SerializeField] private Transform position;
    private SpawnerComponent spawner;

    public override bool Interact(InteractorComponent interactor)
    {
        if (spawner != null)
        {
            return false;
        }
        else
        {
            spawner = interactor.UnHold(position);
            
            if(spawner!=null)spawner.OnHolding += () => { spawner=null; };
            return spawner != null;
            
        }
    }
}
