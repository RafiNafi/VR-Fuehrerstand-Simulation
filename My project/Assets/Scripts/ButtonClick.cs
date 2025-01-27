using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonClick : MonoBehaviour
{
    private Animator animator;
    [SerializeField] public String animationName;

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
        GetComponent<Animator>().Play(animationName, -1, 0f);
        Debug.Log("Button Clicked");
    }
}
