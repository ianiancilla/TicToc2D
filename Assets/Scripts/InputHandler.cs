using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // variables
    float horInput;
    float verInput;
    float spaceBarPressed;

    // cache
    CharaController charaController;
    CameraZoom cameraZoom;

    private void Start()
    {
        // cache
        charaController = GetComponent<CharaController>();
        cameraZoom = FindObjectOfType<CameraZoom>();
    }

    // Update is called once per frame
    void Update()
    {
        horInput = Input.GetAxis("Horizontal");
        verInput = Input.GetAxis("Vertical");
        spaceBarPressed = Input.GetAxis("Jump");

        if (spaceBarPressed > 0)
        {
            cameraZoom.ZoomOut();
        }
        else
        {
            cameraZoom.ZoomIn();
        }
    }

    private void FixedUpdate()
    {
        charaController.Move(horInput, 0);
        if (verInput != 0) { charaController.ClimbStairs(verInput); }

    }
}
