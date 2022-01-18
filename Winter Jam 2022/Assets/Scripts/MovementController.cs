using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Vector3 m_localVelocity = Vector3.zero;

    float m_walkSpeed = 1.8f;
    float m_walkAcceleration = 3.6f;
    float m_sprintSpeed = 3.6f;
    float m_sprintAcceleration = 7.2f;
    float m_jumpVelocity = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 movementVelocity = Vector3.zero;

        float movementSpeed = Input.GetKey(KeyCode.LeftShift) ? m_sprintSpeed : m_walkSpeed;
        float acceleration = Input.GetKey(KeyCode.LeftShift) ? m_sprintAcceleration : m_walkAcceleration;

        if (Input.GetKey(KeyCode.W))
        {
            //movementVelocity += this.transform.forward * movementSpeed;
            movementVelocity += this.transform.forward * acceleration * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //movementVelocity -= this.transform.forward * movementSpeed;
            movementVelocity -= this.transform.forward * acceleration * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //movementVelocity -= this.transform.right * movementSpeed;
            movementVelocity -= this.transform.right * acceleration * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //movementVelocity += this.transform.right * movementSpeed;
            movementVelocity += this.transform.right * acceleration * Time.fixedDeltaTime;
        }

        // Add movement to local velocity, clamp speed.
        m_localVelocity += movementVelocity;
        m_localVelocity = Vector3.ClampMagnitude(m_localVelocity, movementSpeed);

        // Check if player hasn't moved this frame.
        if (movementVelocity.sqrMagnitude == 0.0f)
        {
            // Damped movement velocity.
            m_localVelocity *= 0.8f;
        }

        Rigidbody rigidbody = this.GetComponentInChildren<Rigidbody>();

        if (rigidbody)
        {
            rigidbody.velocity = m_localVelocity;
        }
    }
}
