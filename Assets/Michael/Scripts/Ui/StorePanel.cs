using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Michael.Scripts.Ui
{
    public class StorePanel : MonoBehaviour
    { 
        // [SerializeField] private ItemData bladeData;
        // [SerializeField] private int bladeIndex;
        [SerializeField] private ItemData bladeData;
        [SerializeField] private Button buyButton;
        [SerializeField] private int bladeCost;
        [SerializeField] private TextMeshProUGUI costText;
        private CanvasGroup _canvasGroup;
        private Sequence feedBackSequence;
      
        
        
        
        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            costText.text = bladeCost.ToString();
            UpdateDiscNumber();
            updateBuyButton();
            
        }

        private void updateBuyButton()
        {
            foreach (var blade in MenuManager.Instance.Data.Items)
            {
                if (blade == bladeData)
                {
                    _canvasGroup.interactable = false; 
                    break;
                }
            }
        }

        public void BuyBlade()
        {
            if (MenuManager.Instance.Data.DiscNumber >= bladeCost)
            {
                MenuManager.Instance.ButtonFeedback(buyButton);
                MenuManager.Instance.Data.DiscNumber -= bladeCost;
                _canvasGroup.interactable = false;
                UpdateDiscNumber();
                UnlockBlade();
            }
            else {
                ShowErrorMessage(MenuManager.Instance.StoreText);
            }
        }
        

     
        private void UnlockBlade()
        {
            foreach (GameObject slot in MenuManager.Instance.InventorySlots)
            {
                if (slot.transform.childCount == 0)
                {
                    MenuManager.Instance.Data.Items.Add(bladeData);
                    GameObject newBlade = Instantiate(MenuManager.Instance.BladeItemPrefab, slot.transform);
                    newBlade.GetComponent<Image>().color = bladeData.IconColor;
                    newBlade.GetComponent<DraggableBladeItem>().BladeMaterial = bladeData.BladeMaterial;
                    newBlade.GetComponent<DraggableBladeItem>().bladeData = bladeData;
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
        
        private void ShowErrorMessage(GameObject message) {
            
            Vector3 originalPosition = message.transform.position;
            message.transform.position = originalPosition + new Vector3(0, -10, 0);
            feedBackSequence.Kill();
            feedBackSequence = DOTween.Sequence();
            message.SetActive(true);
            feedBackSequence.Join(message.transform.DOMoveY(originalPosition.y, 0.5f).SetEase(Ease.OutQuad));
            feedBackSequence.Join((message.GetComponent<TextMeshProUGUI>().DOFade(1f, 0.5f)));
            feedBackSequence.Append((message.GetComponent<TextMeshProUGUI>().DOFade(0f, 0.5f)));
            feedBackSequence.Play();
            feedBackSequence.OnComplete(() => { message.SetActive(false);
            });
        }
        
        

    }
}
