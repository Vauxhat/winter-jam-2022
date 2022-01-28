using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Store local camera rotation.
    private Vector3 m_cameraRotation = new Vector3(0.0f, 0.0f, 0.0f);

    // Camera speed variables.
    private float controllerRotationSpeed = 90.0f;
    private float mouseRotationSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise local camera rotation.
        m_cameraRotation = this.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // Initialise rotation for current frame.
        Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);

        // Get mouse movement this frame from input manager.
        rotation.x = Input.GetAxis("Mouse Y") * mouseRotationSpeed * -1.0f;
        rotation.y = Input.GetAxis("Mouse X") * mouseRotationSpeed;

        // Check if mouse movement was used.
        if (rotation.x == 0.0f && rotation.y == 0.0f)
        {
            // Handle keyboard input for up and down rotation.
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rotation.x += controllerRotationSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rotation.x -= controllerRotationSpeed * Time.deltaTime;
            }

            // Handle keyboard input for left and right rotation.
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rotation.y += controllerRotationSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotation.y -= controllerRotationSpeed * Time.deltaTime;
            }
        }

        // Clamp rotation within usable range.
        m_cameraRotation.x = Mathf.Clamp(m_cameraRotation.x + rotation.x, -90.0f, 90.0f);

        // Update camera rotation using euler orientation.
        this.transform.localRotation = Quaternion.Euler(m_cameraRotation);

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
