using UnityEngine;
//Abstract classed cannot be used directly
//We inherit from them, and implement their memeber
public abstract class InputComponentBase : MonoBehaviour
{
    public abstract Vector2 GetInputDirection();
    public abstract Vector2 GetInputDirectionNormalized();
}
