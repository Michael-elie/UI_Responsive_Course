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
    public bool isBladeslot = false;
   
    

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
            if (isBladeslot)
            {
               /* data.unlockedBlades.Remove(current);
                data.unlockedBlades.Add(droppedObject);*/
              Debug.Log("chnage blade");
            }
            
        }
        draggableBladeItem.ParentAfterDrag = transform;
       // ChangeBladeMaterial();
    }

    private void ChangeBladeMaterial()
    {
        bladeslot.data.BladeMaterial = bladeslot.GetComponentInChildren<DraggableBladeItem>() ? bladeslot.
            GetComponentInChildren<DraggableBladeItem>().BladeMaterial : null;
    }
    
}
