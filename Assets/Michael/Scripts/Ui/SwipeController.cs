using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private int maxPage;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Vector3 pageStep;
    [SerializeField] private RectTransform levelPagesRect;
    [SerializeField] private float transitionTime;
    [SerializeField] private Ease tweenType;
    [SerializeField] private int _currentPage;
    private Vector3 _targetPos;
    private float _dragTreshould;

 
    private void Awake()
    {
        _currentPage = 1;
        _targetPos = levelPagesRect.anchoredPosition;
        _dragTreshould = Screen.width / 15;
    }

    private void Update() {
        previousButton.interactable = _currentPage > 1;
        nextButton.interactable = _currentPage < maxPage;
    }
    
    
    public void Next()
    {
        MenuManager.Instance.ButtonFeedback(nextButton);
        if (_currentPage < maxPage)
        {
            _currentPage++;
            _targetPos += pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        MenuManager.Instance.ButtonFeedback(previousButton);
        if (_currentPage > 1)
        {
            _currentPage--;
            _targetPos -= pageStep;
            MovePage();
        }
    }
    
    public void MovePage()
    {
        levelPagesRect.DOAnchorPos(_targetPos, transitionTime).SetEase(tweenType);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > _dragTreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x)Previous();
            else Next();
        }
        else
        {
            MovePage();
        }
    }
    
}



