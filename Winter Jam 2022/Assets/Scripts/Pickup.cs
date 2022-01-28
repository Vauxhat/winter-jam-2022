using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Global variables.
    private Vector3 m_startPosition;
    private Vector3 m_targetPosition;
    private Quaternion m_startRotation;
    private Quaternion m_targetRotation;

    private float m_lerpTime = 0.0f;
    public float m_timeToLerp = 0.1f;

    public bool b_isCollidable { get; private set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise position variables.
        m_startPosition = transform.position;
        m_targetPosition = m_startPosition;

        // Initialise rotation variables.
        m_startRotation = transform.rotation;
        m_targetRotation = transform.rotation;

        // Initialise lerp variables.
        m_lerpTime = m_timeToLerp;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if object object has interpolated to destination.
        if (!Mathf.Approximately(m_lerpTime, m_timeToLerp))
        {
            // Increment lerp time.
            m_lerpTime = Mathf.Clamp(m_lerpTime + Time.deltaTime, 0.0f, 1.0f);

            // Calculate lerp factor (normalised).
            float lerpFactor = m_lerpTime / m_timeToLerp;

            // Interpolate position and rotation.
            transform.position = Vector3.Lerp(m_startPosition, m_targetPosition, lerpFactor);
            transform.rotation = Quaternion.Slerp(m_startRotation, m_targetRotation, lerpFactor);

            // Check if lerp has finished this frame.
            if (Mathf.Approximately(m_lerpTime, m_timeToLerp))
            {
                // Check if object should be collidable, update collision.
                OnEndLerp();
            }
        }
    }

    // Attach pickup to selected gameobject.
    public void AttachItem(GameObject holder, bool collidable)
    {
        // Reset lerp time.
        m_lerpTime = 0.0f;

        // Attach object to holder.
        transform.SetParent(holder.transform);

        // Update start orientation.
        m_startPosition = transform.localPosition;
        m_startRotation = transform.localRotation;

        // Update target orientation.
        m_targetPosition = Vector3.zero;
        m_targetRotation = Quaternion.identity;

        // Update collision flag.
        b_isCollidable = collidable;

        // Disable collision while interpolating.
        OnStartLerp();
    }

    // Drop item in world.
    public void DropItem()
    {
        // Remove object from parent, make collidable.
        transform.parent = null;
        SetCollision(true);
    }

    private void OnStartLerp()
    {
        // Make object intangible.
        SetCollision(false);
    }

    private void OnEndLerp()
    {
        // Check if object should be collidable.
        if (b_isCollidable)
        {
            // Make object tangible.
            SetCollision(true);
        }
        else
        {
            // Remain intangible.
            SetCollision(false);
        }
    }

    private void SetCollision(bool collidable)
    {
        // TODO: Update collision state.
    }
}
