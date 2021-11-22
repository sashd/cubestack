using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    public Color neutralColor = Color.gray;
    public Color friendlyColor = Color.blue;
    public float speed;

    // change local position by 1 in direction
    public void Move(Vector3 direction)
    {
        var newPos = transform.localPosition;
        newPos += direction;
        transform.localPosition = newPos;
    }

    // zeroing y position
    public void MoveToGround()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    public void SwitchToFriendly()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        ChangeColor(friendlyColor);
    }

    public void SwitchToNeutral()
    {
        ChangeColor(neutralColor);
    }

    private void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
}
