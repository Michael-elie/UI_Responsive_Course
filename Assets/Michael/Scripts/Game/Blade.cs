using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public static Action OnTargetSliced;
    public static Action OnBombTouched;
    public static Action OnCdSliced;
    [SerializeField] private int fruitCount = 0;
    [SerializeField] private float comboWindow = 0.5f;
    private Coroutine comboCoroutine;
    [SerializeField] private GameObject comboPrefab;
    [SerializeField] private GameObject perfectHitPrefab;
    [SerializeField] private GameObject discPrefab;
    [SerializeField] private GameObject malusPrefab;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Data _data;
    //[SerializeField] ParticleSystem _particle;
    private Rigidbody _rb;
    private TrailRenderer _tr;
    private Collider _collider;
    
    
    void Start() {
        _rb = GetComponent<Rigidbody>();
        _tr = GetComponent<TrailRenderer>();
        _collider = GetComponent<SphereCollider>();

        _tr.material = _data.BladeMaterial;
    }
    
    private void OnEnable()
    {
        OnBombTouched += DisplayMalusText;
        OnTargetSliced += SliceComboTarget;
        BeatManager.onPerfectHit += DisplayPerfectHit;
        OnCdSliced += DisplayCDText;
    }
    private void OnDisable()
    {
        OnBombTouched -= DisplayMalusText;
        OnCdSliced -= DisplayCDText;
        OnTargetSliced -= SliceComboTarget;
        BeatManager.onPerfectHit -= DisplayPerfectHit;
    }

    private void SliceComboTarget() {
        GameManager.Score += 1;
        fruitCount++;
        if (fruitCount == 1) {
            comboCoroutine = StartCoroutine(ComboTimer());
        }
        else if (fruitCount == 2) {
            DisplayText(comboPrefab, "x2 Combo", transform.position);
            GameManager.Score+= 2;

        }
        else if (fruitCount >= 2) {
            DisplayText(comboPrefab, "x3 Combo", transform.position);
            GameManager.Score += 3;
            fruitCount = 0;
            if (comboCoroutine != null) {
                StopCoroutine(comboCoroutine);
            }
        }
    }
    private IEnumerator ComboTimer() {
        yield return new WaitForSeconds(comboWindow);
        fruitCount = 0;
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            Debug.Log("fruit touché");
            other.GetComponent<Fruit>().SliceFruit();
            OnTargetSliced.Invoke();
        }
        if (other.CompareTag("Bomb"))
        {
            OnBombTouched.Invoke();
        }
        if (other.CompareTag("Cd"))
        {
            other.GetComponent<Fruit>().SliceFruit();
            OnCdSliced.Invoke();
        }
        
    }
    
    private void DisplayMalusText()
    {
        DisplayText(malusPrefab,"-20 pts ",transform.position + Vector3.right);
        GameManager.Score -= 20;
        if ( GameManager.Score < 0 ) {
            GameManager.Score = 0;
        }
    }

    private void DisplayCDText()
    {
        DisplayText(discPrefab,"+1 disque ",transform.position + Vector3.up);
        _data.DiscNumber += 1;
    }

    private void DisplayPerfectHit()
    {
        DisplayText(perfectHitPrefab,"synchro x5 ", transform.position + Vector3.down );
    }
    
    private void DisplayText(GameObject prefab, string message, Vector3 position) {
        
        GameObject Uiprefab = Instantiate(prefab, position, Quaternion.identity);
        Uiprefab.transform.SetParent(GameObject.Find("Canvas").transform, false);
        Uiprefab.transform.position = Camera.main.WorldToScreenPoint(position);
        Uiprefab.transform.DOScale(1, 0.3f);
        Uiprefab.GetComponent<TextMeshProUGUI>().text = message;
        Destroy(Uiprefab, 0.7f);
    }
    
    
}

