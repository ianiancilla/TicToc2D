using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // variables
    float horInput;
    float verInput;

    // cache
    CharaController charaController;

    private void Start()
    {
        // cache
        charaController = GetComponent<CharaController>();
    }

    // Update is called once per frame
    void Update()
    {
        horInput = Input.GetAxis("Horizontal");
        verInput = Input.GetAxis("Vertical");


    }

    private void FixedUpdate()
    {
        charaController.Move(horInput, 0);
        if (verInput != 0) { charaController.ClimbStairs(verInput); }
    }
}
