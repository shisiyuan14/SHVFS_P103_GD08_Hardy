using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableComponentBase : MonoBehaviour
{
    public abstract int Priority { get; }
    public abstract bool Interact(InteractorComponent interactor);
}
