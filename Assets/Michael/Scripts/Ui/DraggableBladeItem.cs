using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Michael.Scripts
{
    public class DraggableBladeItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Material BladeMaterial; 
        public int BladeIndex;
        [HideInInspector] public Image ItemImage;
        [HideInInspector] public Transform ParentAfterDrag;
     

        private void Update()
        {
            if (GetComponentInParent<InventorySlot>())
            {
                ItemImage.raycastTarget = !GetComponentInParent<InventorySlot>().isBladeslot;
            }
        }

        private void Start() {
            ItemImage = GetComponent<Image>();
        }
        
        public void OnBeginDrag(PointerEventData eventData) {
            ParentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            ItemImage.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData) {
          /*  transform.position = Input.mousePosition;*/
          RectTransformUtility.ScreenPointToLocalPointInRectangle(
              transform.parent as RectTransform,
              eventData.position, // Use eventData.position instead of Input.mousePosition
              eventData.pressEventCamera,
              out Vector2 localPoint);
          transform.localPosition = new Vector3(localPoint.x, localPoint.y, 0);
        }
        
        public void OnEndDrag(PointerEventData eventData) {
            transform.SetParent(ParentAfterDrag);
            ItemImage.raycastTarget = true;
            // transform.position = ParentAfterDrag.position;
            Vector3 position = transform.localPosition;
            position.z = 0;
            transform.localPosition = position;
            
            
        }
    }
}
