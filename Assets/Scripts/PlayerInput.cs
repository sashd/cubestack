using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Player player;
    public float maxPressedTime = 1f;
    private float elapsedTime = 0f;
    private bool holding = false;

    private void Update() 
    {
        if (holding)
        {
            elapsedTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnTouchDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnTouchUp();
        }
    }

    private void OnTouchDown()
    {
        holding = true;
    }

    private void OnTouchUp()
    {
        holding = false;
        if (elapsedTime > maxPressedTime)
        {
            elapsedTime = maxPressedTime;
        }
        var pressFactor = elapsedTime / maxPressedTime;
        player.OnJumpInputUp(pressFactor);
        elapsedTime = 0;
    }

}
