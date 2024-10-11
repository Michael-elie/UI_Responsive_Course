using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Michael.Scripts
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [HideInInspector] public Image ItemImage;
        [HideInInspector] public Transform ParentAfterDrag;

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
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData) {
            transform.SetParent(ParentAfterDrag);
            ItemImage.raycastTarget = true;
            transform.position = ParentAfterDrag.position;
        }
    }
}
