using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Data _data;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private GameObject _menuCanvasGroup;
    [SerializeField] private List<AudioSource> _buttonSounds;
    [SerializeField] private GameObject _mainTitle;

    private void Awake() {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }

    private void Start()
    {
       // SetMusicVolume();
       // SetSfxVolume();
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    public void ButtonFeedbackWithRotate(Button button) {
        
        _buttonSounds[Random.Range(0,_buttonSounds.Count)].Play();
        Sequence feedBackSequence = DOTween.Sequence();
        feedBackSequence.Append(button.transform.DOScale(1.1f, 0.2f).SetEase(Ease.Linear));
        feedBackSequence.Append(button.transform.DOScale(1f, 0.2f).SetEase(Ease.Linear));
        feedBackSequence.Join(button.transform.DORotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.OutCubic));
        feedBackSequence.Play();
    }

    public void ButtonFeedback(Button button) {
        
        _buttonSounds[Random.Range(0,_buttonSounds.Count)].Play();
        Sequence feedBackSequence = DOTween.Sequence();
        feedBackSequence.Append(button.transform.DOScale(1.1f, 0.2f).SetEase(Ease.Linear));
        feedBackSequence.Append(button.transform.DOScale(1f, 0.2f).SetEase(Ease.Linear));
        feedBackSequence.Play();
        
        
    }

    public void OpenPanel(GameObject panel) {
        
        _mainTitle.SetActive(false);
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        _menuCanvasGroup.GetComponent<CanvasGroup>().DOFade(0.02f, 0.5f);
        
        Sequence showSequence = DOTween.Sequence();
        showSequence.Append(panel.GetComponent<Transform>().DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
        showSequence.Join((canvasGroup.DOFade(1f, 0.5f)));
        showSequence.Play();
    }
    
     public void ClosePanel(GameObject panel)
     {
         _mainTitle.SetActive(true);
         CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
         canvasGroup.blocksRaycasts = false;
         canvasGroup.interactable = false;
         _menuCanvasGroup.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
         
         Sequence hideSequence = DOTween.Sequence();
         hideSequence.Append(panel.GetComponent<Transform>().DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack));
         hideSequence.Join(canvasGroup.DOFade(0f, 0.5f));
         hideSequence.Play();
    }
     
     public void SetMusicVolume() {
        _data.MusicVolume = _musicSlider.value;
        _mixer.SetFloat("Music", Mathf.Log10(_data.MusicVolume) * 20);
    }

    public void SetSfxVolume() {
        _data.SfxVolume = _sfxSlider.value;
        _mixer.SetFloat("Sfx", Mathf.Log10(_data.SfxVolume) * 20);
    }
    
    
    
}
