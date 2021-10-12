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
    [SerializeField] private AudioSource carAudio;
    [Space]
    [Header("Variables")]

    [SerializeField] private float carSpeed;
    [SerializeField] private float carTorque;
    [SerializeField] private float radius;

    private bool isGrounded;
    public bool toStart = false;

    #endregion

    #region Builtin Methods
    private void Start()
    {
        carAudio.volume = 0.4f;
    }
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
        float rigidBodyMangintude = carBody.velocity.magnitude;
        float pitch = MapValue(rigidBodyMangintude, 0f, 40f, 0f, 2f);

        carAudio.pitch = pitch;
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

    public void ApplyBrake()
    {
        frontWheel.angularVelocity = 0f;
        rearWheel.angularVelocity = 0f;
        carBody.angularVelocity = 0f;
    }

    private float MapValue(float mainValue, float inValueMin, float inValueMax, float outValueMin, float outValueMax)
    {
        return (mainValue - inValueMin) * (outValueMax - outValueMin) / (inValueMax - inValueMin) + outValueMin;
    }
    #endregion
}
