using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    [SerializeField] private GameObject wholefruit;
    [SerializeField] private GameObject slicedFruit;
    void Start()
    {

    }

    void Update()
    {

    }

    private void ShowSlicedFruit() {
        
        wholefruit.SetActive(false);
        slicedFruit.SetActive(true);

    }

    private void OnTriggerEnter(Collider other)
    {
        ShowSlicedFruit();
    }
}
