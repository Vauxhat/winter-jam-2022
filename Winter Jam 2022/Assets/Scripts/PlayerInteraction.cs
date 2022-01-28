using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Global references.
    Camera m_playerCamera;
    GameObject m_leftHand, m_rightHand;

    float m_interactRange = 2.0f;

    Vector3 m_startPosition;
    Vector3 m_targetPosition;
    Quaternion m_startRotation;
    Quaternion m_targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        m_playerCamera = this.GetComponentInChildren<Camera>();

        //m_leftHand = transform.Find("Left Hand").GameObject;
        //m_rightHand = transform.Find("Right Hand").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if interaction key has been pressed.
        if(Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            // Check if raycast has hit anything within interaction range.
            if(Physics.Raycast(m_playerCamera.transform.position, m_playerCamera.transform.forward, out hit, m_interactRange))
            {
                // Check if either hand is free.
                if (!m_leftHand || !m_rightHand)
                {
                    // Check if hit object is a pickup
                    if (hit.transform.tag == "Pickup")
                    {
                        // Check if item is within a holder.
                        if (hit.transform.parent.tag == "Item Holder")
                        {
                            // Remove object from holder.
                        }

                        // Attach object to player.
                        hit.transform.SetParent(m_playerCamera.transform);

                        Rigidbody colliderBody = hit.transform.GetComponentInChildren<Rigidbody>();

                        if (colliderBody)
                        {
                            colliderBody.isKinematic = true;
                        }

                        hit.transform.localPosition = new Vector3(0.5f, -0.5f, 1.0f);
                        hit.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

                        // Check if right hand is free.
                        if (m_leftHand)
                        {
                            // Attach object to right hand.
                            //hit.transform.SetParent(m_rightHand.transform);
                            m_rightHand = hit.transform.gameObject;
                        }
                        else 
                        {
                            // Attach object to left hand.
                            //hit.transform.SetParent(m_leftHand.transform);
                            m_leftHand = hit.transform.gameObject;
                        }
                    }
                }
                

                // Call 'use' on hit object.
                hit.transform.SendMessage("Use", this.gameObject);
            }
        }
    }
}
