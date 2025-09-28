using RG.Systems.Tests.Player;
using UnityEngine;
using System;
using static InputSystem_Actions;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader"), Serializable]
public class InputReader : ScriptableObject, IPlayerActions
{
    private InputSystem_Actions _inputActions;

    public Vector2 Direction => _inputActions.Player.Move.ReadValue<Vector2>();

    public Vector2 LookDirection => _inputActions.Player.Look.ReadValue<Vector2>();

    public void DisableMovementInputs()
    {
        _inputActions.Player.Disable();
    }
    public void EnableMovmentInput()
    {
        _inputActions.Player.Enable();
    }

    public event Action Attack = delegate
    {
    };

    public event Action<bool> Crouch = delegate
    {
    };

    public event Action<bool> Jump = delegate
    {
    };

    public event Action<bool> Run = delegate
    {
    };

    public event Action<Vector2, bool> Look = delegate
    {
    };

    public event Action<Vector2> Move = delegate
    {
    };
    public event Action Grab = delegate
    {
    };

    public void EnablePlayerInputActions()
    {
        if (_inputActions == null)
        {
            _inputActions = new InputSystem_Actions();
            _inputActions.Player.SetCallbacks(this);
        }

        _inputActions.Enable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        
    }
    public void OnDrop(InputAction.CallbackContext context)
    {

    }
    
    public void OnUse(InputAction.CallbackContext context)
    {

    }

    private void OnDisable()
    {
        if (_inputActions != null)
        {
            _inputActions.Disable();
        }
    }

    
}
