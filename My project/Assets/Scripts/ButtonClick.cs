using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonClick : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private string animationTrigger = "Pressed";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnEnable()
    {
        GetComponent<XRBaseInteractable>().selectEntered.AddListener(OnSelectEntered);        
    }

    void OnDisable()
    {
        GetComponent<XRBaseInteractable>().selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        animator.SetTrigger(animationTrigger);
        Debug.Log("Button Clicked");
    }
}
