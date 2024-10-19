using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    
        [SerializeField] private TextMeshProUGUI titleText; 
        [SerializeField] private float animationDuration = 2f; 
        [SerializeField] private Color neonColor = new Color(); 
        [SerializeField] private float bounceDuration = 0.5f;
        [SerializeField] private CanvasGroup mainMenu;
        void Start() {
            
            ShowTitle();
        }
        void ShowTitle() {
            
            Sequence synthwaveSequence = DOTween.Sequence();
            synthwaveSequence.Append(titleText.transform.DOScale(1.5f, animationDuration / 2).SetEase(Ease.OutExpo));
            synthwaveSequence.Append(titleText.transform.DOScale(1f, animationDuration / 2).SetEase(Ease.InFlash));
            synthwaveSequence.Join(titleText.DOFade(1f, animationDuration/2));
            synthwaveSequence.AppendCallback(TitleEffect);
            synthwaveSequence.Play();
        }
        void TitleEffect()
        {
            StartBounceAnimation();
            StartColorAnimation();
            MainMenuAnimation();
        }
        void StartBounceAnimation() {
            titleText.transform.DOScale(1.2f, bounceDuration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
        void StartColorAnimation() {
            titleText.DOColor(neonColor, bounceDuration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }

        void MainMenuAnimation()
        {
            Sequence menuSequence = DOTween.Sequence();
            menuSequence.Append(mainMenu.transform.DOScale(1.1f, 0.5f).SetEase(Ease.OutExpo));
            menuSequence.Append(mainMenu.transform.DOScale(1f, 0.5f).SetEase(Ease.InFlash));
            menuSequence.Join(mainMenu.DOFade(1, 1f));
            mainMenu.interactable = true;
        }



}
