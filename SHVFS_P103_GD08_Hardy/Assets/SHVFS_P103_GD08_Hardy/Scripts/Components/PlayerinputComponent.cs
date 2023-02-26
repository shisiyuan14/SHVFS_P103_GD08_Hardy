using UnityEngine;

public class PlayerinputComponent : InputComponentBase
{
    private Vector2 inputDirection;

    private PlayerActions playerActions;

    private void Awake()
    {
      playerActions = new PlayerActions();
      playerActions.PlayerInput.Enable();

    }

    public override Vector2 GetInputDirection()
    {
        return playerActions.PlayerInput.Movement.ReadValue<Vector2>();
    }

    public override Vector2 GetInputDirectionNormalized()
    {
        return GetInputDirection().normalized;
    }

    
}
