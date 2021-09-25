using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    #region Variables
    [Header("Components")]

    [SerializeField] private Rigidbody2D frontWheel;
    [SerializeField] private Rigidbody2D rearWheel;
    [SerializeField] private Rigidbody2D carBody;
    [SerializeField] private ParticleSystem carDust;
    [SerializeField] private LayerMask whatIsGround;
    [Space]
    [Header("Variables")]

    [SerializeField] private float carSpeed;
    [SerializeField] private float carTorque;
    [SerializeField] private float radius;

    private bool isGrounded;
    public bool toStart = false;

    #endregion

    #region Builtin Methods
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(rearWheel.position, radius, whatIsGround);
        if(!isGrounded)
        {
            carDust.Stop();
        }
        if (toStart)
        {
            frontWheel.AddTorque(-carSpeed * Time.fixedDeltaTime);
            rearWheel.AddTorque(-carSpeed * Time.fixedDeltaTime);
            carBody.AddTorque(-carTorque * Time.fixedDeltaTime);
        }
        else if(!toStart)
        {
            frontWheel.AddTorque(0f);
            rearWheel.AddTorque(0f);
            carBody.AddTorque(0f);
        }
    }
    #endregion

    #region Custom Methods
    public void CarDustParticles()
    {
        if(isGrounded)
        {
            carDust.Play();
        }
    }
    #endregion
}
