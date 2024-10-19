using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public static Action OnSliced;
    [SerializeField] private GameObject wholefruit;
    [SerializeField] private GameObject slicedFruit;
    [SerializeField] private float sliceForce = 130f;
    [SerializeField] private float rotationForce = 200f ;
    private Rigidbody _rb;
    private void Start()
    { 
        _rb = GetComponent<Rigidbody>();
    }
    

    void Update() {
        if (wholefruit) {
            wholefruit.transform.Rotate(Vector2.right * (Time.deltaTime * rotationForce));
        }
      
    }
    private void SliceFruit() {
        
        wholefruit.SetActive(false);
        slicedFruit.SetActive(true);

        Rigidbody[] sliceRb = slicedFruit.transform.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody srb in sliceRb) {
            srb.AddExplosionForce(sliceForce, transform.position,10);
            srb.velocity =  _rb.velocity * 1.2f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("blade"))
        {
            Debug.Log("fruit touché");
            SliceFruit();
            OnSliced.Invoke();
        }
    }
}
