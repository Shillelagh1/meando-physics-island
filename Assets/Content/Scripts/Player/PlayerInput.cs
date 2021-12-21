using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public float mouseSensitivityX = 0.5f;
    public float mouseSensitivityY = 0.3f;

    public float moveX = 0;
    public float moveY = 0;

    public float mouseX = 0;
    public float mouseY = 0;

    public bool jump = false;

    private Keyboard kb;
    private Mouse inputMouse;

    void Start()
    {
        kb = Keyboard.current; //Do we really need to nullcheck here?
        inputMouse = Mouse.current;
    }

    void Update()
    {
        if (kb.wKey.isPressed) { moveY = 1; } else //WASD
        if (kb.sKey.isPressed) { moveY = -1; } else moveY = 0;
        if (kb.dKey.isPressed) { moveX = 1; } else
        if (kb.aKey.isPressed) { moveX = -1; } else moveX = 0;
        jump = kb.spaceKey.wasPressedThisFrame;

        mouseX = inputMouse.delta.x.ReadValue() * mouseSensitivityX;
        mouseY += inputMouse.delta.y.ReadValue() * mouseSensitivityY;
        mouseY = Mathf.Clamp(mouseY, -80,80);
    }
}
