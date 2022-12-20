using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input")][SerializeField] float controlSpeed = 10f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 7f;
    [SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float postionYawFactor = 3f;
    [SerializeField] float controlRollFactor = -25f;

    float xInput, yInput;

    void Start()
    {

        SetLasersActive(false);
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessShooting();

    }
    void ProcessRotation()
    {
        // Pitch rotation(around x-axis)
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yInput * controlPitchFactor;

        //Roll rotation(z-axis)
        float rollDueToControlThrow = xInput * controlRollFactor;


        float pitch = pitchDueToPosition + pitchDueToControlThrow ;
        float yaw = transform.localPosition.y + postionYawFactor;
        float roll = rollDueToControlThrow;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        float xOffset = xInput * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);


        float yOffset = yInput * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3
            (clampedXPos, clampedYPos, transform.localPosition.z);
    }
    void ProcessShooting()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool fireOrNotFire)
    {
        foreach (GameObject item in lasers)
        {
            ParticleSystem particleSystem = item.GetComponent<ParticleSystem>();

            var emissionModule = particleSystem.emission;
            emissionModule.enabled = fireOrNotFire;
        }
    }

}
