using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Mouvement : MonoBehaviour
{
    [Header("Propulsion")]
    public float speedOfMouvement;
    public float maxSpeed = 10;

    [Header("Rotation")]
    public float speedOfRotation;

    [Header("Debug")]
    public float currentSpeed;
    public bool activeControl = true;

    private Rigidbody rigid_Player;
    private float _triggerAxis;
    private float _rightAxis;


    // Start is called before the first frame update
    void Start()
    {
        rigid_Player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(activeControl)
        {
            Acceleration();
            Rotation();
            SpeedGestion();
        }

    }

    public void OnAccelerartion(InputAction.CallbackContext ctx) => _triggerAxis = ctx.ReadValue<float>();
    public void OnRotation(InputAction.CallbackContext ctx) => _rightAxis = ctx.ReadValue<float>();

    public void Acceleration()
    {
        Debug.Log(_triggerAxis);
        Vector3 direction = transform.forward * _triggerAxis;
        rigid_Player.AddForce(direction.normalized * speedOfMouvement, ForceMode.Acceleration);
    }

    public void Rotation()
    {
        transform.Rotate(0, _rightAxis * speedOfRotation * Time.deltaTime, 0);
    }


    public void SpeedGestion()
    {
        if (rigid_Player.velocity.magnitude > maxSpeed)
        {
            rigid_Player.velocity = Vector3.ClampMagnitude(rigid_Player.velocity, maxSpeed);
        }
        currentSpeed = rigid_Player.velocity.magnitude;
    }

    public void ResetGame()
    {
        rigid_Player.velocity = new Vector3(0, 0, 0);
    }

}
