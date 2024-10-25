using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Michael.Scripts.Ui
{
    public class StorePanel : MonoBehaviour
    {
        [SerializeField] private ItemData bladeData;
        private void Start()
        {
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                UpdateDiscNumber();
                // LoadInventory();

            }
        }

        public void BuyBlade(int cost)
        {
            if (MenuManager.Instance.Data.DiscNumber >= cost)
            {
                MenuManager.Instance.Data.DiscNumber -= cost;
                UpdateDiscNumber();
            }
            else
            {
                Debug.Log("pas assez dargent");
            }
        }
    
   

        public void UnlockBlade(int bladeItemIndex)
        {

            foreach (GameObject slot in MenuManager.Instance.InventorySlots)
            {
                if (slot.transform.childCount == 0)
                {
                    GameObject newBlade = Instantiate(MenuManager.Instance.Blades[bladeItemIndex], slot.transform);
                    newBlade.GetComponent<DraggableBladeItem>().BladeIndex = bladeItemIndex;
                    return;
                }
            }

        }
        private void UpdateDiscNumber()
        {
            if (MenuManager.Instance.DiscCountText)
            {
                if ( MenuManager.Instance.Data.DiscNumber < 0)
                {
                    MenuManager.Instance.Data.DiscNumber = 0;
                }

                MenuManager.Instance.DiscCountText.text =   MenuManager.Instance.Data.DiscNumber.ToString();
            }
        }

    }
}
