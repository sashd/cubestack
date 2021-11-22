using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float maxJumpHeight = 2f;
    public float timeToJumpApex = 0.5f;
    public LayerMask groundLayer;
    public static Action<int> onCubeCountChange;

    private Stack<Cube> cubes = new Stack<Cube>(10);
    private bool grounded = true;
    private float rayLength = 0.5f;
    private Vector3 velocity;
    private float gravity;
    private float maxJumpVelocity;
    private bool jumping = false;
    private float jumpResetTime = 0.1f;

    private void Start()
    {    
        var cube = GetComponentInChildren<Cube>();
        if (cube is null)
        {
            Debug.LogError("No cube in children");
            return;
        }
        AddCube(cube); 
        velocity.z = moveSpeed;
        
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, rayLength, groundLayer);

        if (grounded && !jumping)
        {
            velocity.y = 0f; 
        }
        else  
        {
            velocity.y += gravity * Time.deltaTime;
        } 
        
        transform.Translate(velocity * Time.deltaTime);
    }

    public void OnJumpInputUp(float pressedTime)
    {
        if (!grounded)
            return;

        jumping = true;     
        velocity.y = maxJumpVelocity * pressedTime;
        StartCoroutine(JumpReset());
    }

    private IEnumerator JumpReset()
    {     
        yield return new WaitForSeconds(jumpResetTime);
        jumping = false;
    }

    private void AddCube(Cube newCube)
    {
        foreach (var cube in cubes)
        {
            cube.Move(Vector3.up);
        }
        cubes.Push(newCube);
        newCube.transform.parent = gameObject.transform;
        newCube.transform.localPosition = Vector3.zero;
        newCube.SwitchToFriendly();

        onCubeCountChange?.Invoke(1);
    }

    private void LeaveCube()
    {
        if (cubes.Count > 1)
        {
            Cube leavedCube = cubes.Pop();
            leavedCube.SwitchToNeutral();
            leavedCube.MoveToGround();
            leavedCube.transform.parent = null;

            foreach (var cube in cubes)
            {
                cube.Move(Vector3.down);
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        onCubeCountChange?.Invoke(-1);
    }


    private void OnTriggerEnter(Collider other) 
    {        
        if (other.CompareTag("Friendly"))
        {
            var cube = other.GetComponent<Cube>();
            if (cube is null) 
                return;

            AddCube(cube);
        }
        else if (other.CompareTag("Enemy"))
        {
            LeaveCube();
        }
        else if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
