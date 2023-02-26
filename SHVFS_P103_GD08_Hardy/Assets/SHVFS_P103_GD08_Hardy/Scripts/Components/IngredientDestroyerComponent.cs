using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDestroyerComponent : InteractableComponentBase
{
    public override void Interact()
    {
        Debug.Log("Destroy the food!");
    }

}
