using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Store local camera rotation.
    private Vector3 m_cameraRotation = new Vector3(0.0f, 0.0f, 0.0f);

    // Camera speed variables.
    private float rotationSpeed = 90.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise local camera rotation.
        m_cameraRotation = this.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);

        // Handle keyboard input for up and down rotation.
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rotation.x += rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rotation.x -= rotationSpeed * Time.deltaTime;
        }

        // Clamp rotation within usable range.
        m_cameraRotation.x = Mathf.Clamp(m_cameraRotation.x + rotation.x, -90.0f, 90.0f);

        // Update camera rotation using euler orientation.
        this.transform.localRotation = Quaternion.Euler(m_cameraRotation);

        // Handle keyboard input for left and right rotation.
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation.y += rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotation.y -= rotationSpeed * Time.deltaTime;
        }

        // Get main collision hull from parent or child object.
        Collider collider = this.transform.parent.GetComponentInChildren<Collider>();

        // Check if parent has a collider.
        if (collider)
        {
            // Rotate collider along global y axis.
            collider.transform.Rotate(0.0f, rotation.y, 0.0f);
        }
        else
        {
            // Rotate camera along global y axis.
            this.transform.Rotate(0.0f, rotation.y, 0.0f);
        }
    }
}
