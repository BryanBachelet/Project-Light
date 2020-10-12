using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Mouvement : MonoBehaviour
{
    [Header("Propulsion")]
    public AnimationCurve speedOfMouvement;
    public float maxSpeed = 10;

    [Header("Rotation")]
    public float speedOfRotation;

    [Header("Debug")]
    public float currentSpeed;
    public bool activeControl = true;

    private Rigidbody rigid_Player;
    private float _triggerAxis;
    private float _rightAxis;

    private float factorAcceleration = 0;
    public float timerAcceleration = 0;

    public ParticleSystem reactor1;
    public ParticleSystem reactor2;
    public ParticleSystem Fire1;
    public ParticleSystem Fire2;
    public ParticleSystem.ShapeModule reactor1Shape;
    public ParticleSystem.ShapeModule reactor2Shape;
    // Start is called before the first frame update
    void Start()
    {
        if(reactor1 != null)
            reactor1Shape = reactor1.shape;
        if(reactor2 != null)
            reactor2Shape = reactor2.shape;
        rigid_Player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if(activeControl)
        {
            Acceleration();
            AccelerationInput();
            Rotation();
            SpeedGestion();
        }

    }

    public void OnTestTrigger(InputAction.CallbackContext ctx) { Manager_Score.PlayerDeath(gameObject); }
    public void OnAccelerartion(InputAction.CallbackContext ctx) => _triggerAxis = ctx.ReadValue<float>();
    public void OnRotation(InputAction.CallbackContext ctx) => _rightAxis = ctx.ReadValue<float>();

    public void Acceleration()
    {
        Debug.Log(_triggerAxis);
        reactor1.startLifetime = _triggerAxis * 20;
        reactor1Shape.angle = 0.07f + (100f * speedOfMouvement.Evaluate(timerAcceleration));
      //  Fire1.startSpeed = (speedOfMouvement.Evaluate(timerAcceleration) * _triggerAxis) / 8;
        reactor2.startLifetime = _triggerAxis * 20;
        reactor2Shape.angle = 0.07f + (100f * speedOfMouvement.Evaluate(timerAcceleration));
        //Fire2.startSpeed = (speedOfMouvement.Evaluate(timerAcceleration) * _triggerAxis) / 8;
        Vector3 direction = transform.forward * _triggerAxis;
        rigid_Player.AddForce(direction.normalized * speedOfMouvement.Evaluate(timerAcceleration), ForceMode.Acceleration);
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

    public void AccelerationInput()
    {
        factorAcceleration = _triggerAxis;
        if(factorAcceleration > 0)
        {
            timerAcceleration += Time.deltaTime * factorAcceleration;
        }
        else if(factorAcceleration <= 0)
        {
            timerAcceleration = 0;
        }
    }
    public void ResetGame()
    {
        rigid_Player.velocity = new Vector3(0, 0, 0);
    }

}
