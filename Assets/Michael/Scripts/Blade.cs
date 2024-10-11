using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
   // [SerializeField] ParticleSystem _particle;
    private Rigidbody _rb;
    private TrailRenderer _tr;
    private Collider _collider;
    
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _tr = GetComponent<TrailRenderer>();
        _collider = GetComponent<SphereCollider>();
        
    }
    
    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            
            _tr.enabled = true;
            _collider.enabled = true;
          //  _particle.Play();
            
        }
        if (Input.GetMouseButtonUp(0)) {
            _tr.enabled = false;
            _collider.enabled = false;
          //  _particle.Stop();
        }
        
      
        BladeFollowMouse();
    }

    private void BladeFollowMouse() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 8;
        _rb.position = mainCamera.ScreenToWorldPoint(mousePos);
    }
    
    
}

