using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;





public class IngredientDestroyerComponent : InteractableComponentBase
{
    public override int Priority => 2;

    public override bool Interact(InteractorComponent interactor)
    {
        if (interactor.HoldingObject == null) return false;
        else
        { 
            Destroy(interactor.HoldingObject.gameObject); 
        }
        Debug.Log("Destroy the food!");
        return true;
    }
}
