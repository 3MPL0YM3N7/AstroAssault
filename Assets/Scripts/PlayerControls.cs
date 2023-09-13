using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    // developers note (ignore this comment) $ --> string interpolation

    [Header("Input")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction shooting;

    [Header("Player Speed")]
    [Tooltip("the speed of the player")] [SerializeField] float controlsSpeed = 20f;

    [Header("Player Position Constraints")]
    // player stays in screen with these variables
    [SerializeField] float xClamp = 6f;
    [SerializeField] float yClamp = 6f;

    [Header("Player Rotation")]
    // rotation due to position & controls (pressing buttons)
    // Pitch
    [SerializeField] float positionPitchFactor = -2.5f;
    [SerializeField] float controlsPitchFactor = -10f;
    // Yaw
    [SerializeField] float positionYawFactor = 3.5f;
    // Roll
    [SerializeField] float controlsRollFactor = -20f;

    [Header("Player Shooting Particles")]
    // Particle Systems for ProcessShooting() and bool to run laser.SetActive() only once
    [SerializeField] GameObject[] lasers;
    bool isShooting = false;
    
    float horizontalInput;
    float verticalInput;

    void OnEnable()
    {
        movement.Enable();
        shooting.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        shooting.Disable();
    }

    void Start()
    {

    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessShooting();
    }

    void ProcessTranslation()
    {
        horizontalInput = movement.ReadValue<Vector2>().x;
        float rawXPos = transform.localPosition.x + (horizontalInput * Time.deltaTime * controlsSpeed);
        float clampedXPos = Mathf.Clamp(rawXPos, -xClamp, xClamp);

        verticalInput = movement.ReadValue<Vector2>().y;
        float rawYPos = transform.localPosition.y + (verticalInput * Time.deltaTime * controlsSpeed);
        // +1f because the ship is not exactly in the middle of the y-axis
        float clampedYPos = Mathf.Clamp(rawYPos, -yClamp + 1f, yClamp);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        // first part is pitch due to position, second part is pitch due to controls
        float pitch = (transform.localPosition.y * positionPitchFactor) + (verticalInput * controlsPitchFactor);
        // for yaw position-dependant only
        float yaw = transform.localPosition.x * positionYawFactor;
        // for roll controls-dependant only
        float roll = horizontalInput * controlsRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessShooting()
    {
        if (shooting.ReadValue<float>() == 1 && !isShooting)
        {
            foreach (GameObject laser in lasers)
            {
                laser.GetComponent<ParticleSystem>().Play();
            }
            isShooting = true;
        }
        else if (shooting.ReadValue<float>() == 0 && isShooting == true)
        {
            foreach (GameObject laser in lasers)
            {
                laser.GetComponent<ParticleSystem>().Stop();
            }
            isShooting = false;
        }
    }
}
