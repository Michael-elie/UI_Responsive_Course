using UnityEngine;

public class Fruit : MonoBehaviour
{
    
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
    public void SliceFruit() {
        
        wholefruit.SetActive(false);
        slicedFruit.SetActive(true);
        gameObject.GetComponent<Collider>().enabled = false;

        Rigidbody[] sliceRb = slicedFruit.transform.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody srb in sliceRb) {
            srb.AddExplosionForce(sliceForce, transform.position,10);
            srb.linearVelocity =  _rb.linearVelocity * 1.2f;
        }
    }

  
}
