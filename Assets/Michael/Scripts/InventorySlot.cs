using System.Collections;
using System.Collections.Generic;
using Michael.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        
        GameObject droppedObject = eventData.pointerDrag;
        DraggableItem draggableItem = droppedObject.GetComponent<DraggableItem>();
        
        if(transform.childCount != 0) {           
            GameObject current = transform.GetChild(0).gameObject;
            DraggableItem currentDraggable = current.GetComponent<DraggableItem>();

            currentDraggable.transform.SetParent(draggableItem.ParentAfterDrag);
        }
        draggableItem.ParentAfterDrag = transform;
    }
    
}
