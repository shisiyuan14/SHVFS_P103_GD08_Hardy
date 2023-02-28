using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerComponent : InteractableComponentBase
{
    public event Action OnHolding
    {
        add => onHolding += value;
        remove => onHolding -= value;
    }
    private Action onHolding;
    public bool isHolding = false;

    public override int Priority => 3;

    public override bool Interact(InteractorComponent interactor)
    {
        if (isHolding)
        {
            return false;
        }
        else
        {
            interactor.Hold(this);
            onHolding?.Invoke();
            onHolding = null;
            return true;
        }
    }
}
