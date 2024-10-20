using System;
using System.Collections;
using System.Collections.Generic;
using Michael.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private InventorySlot bladeslot;
    [SerializeField] private Data data;

    private void Start()
    {
       // ChangeBladeMaterial();
    }

    private void Update()
    {
        ChangeBladeMaterial();
    }

    public void OnDrop(PointerEventData eventData) {
        
        GameObject droppedObject = eventData.pointerDrag;
        DraggableBladeItem draggableBladeItem = droppedObject.GetComponent<DraggableBladeItem>();
        
        if(transform.childCount != 0) {           
            GameObject current = transform.GetChild(0).gameObject;
            DraggableBladeItem currentDraggableBlade = current.GetComponent<DraggableBladeItem>();
            
            currentDraggableBlade.transform.SetParent(draggableBladeItem.ParentAfterDrag);
        }
        draggableBladeItem.ParentAfterDrag = transform;
       // ChangeBladeMaterial();
    }

    private void ChangeBladeMaterial() {
        if (bladeslot.GetComponentInChildren<DraggableBladeItem>()) {
            bladeslot.data.BladeMaterial = bladeslot.GetComponentInChildren<DraggableBladeItem>().BladeMaterial;
        }
        else
        {
            bladeslot.data.BladeMaterial = null;
        }
       
    }
    
}
