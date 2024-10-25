using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using DG.Tweening;
using Michael.Scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;

public class MenuManager : MonoBehaviourSingleton<MenuManager>
{
    public Data Data;
    public List<GameObject> InventorySlots;
    public List<GameObject> Blades;
    public TextMeshProUGUI DiscCountText;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private GameObject _menuCanvasGroup;
    [SerializeField] private List<AudioSource> _buttonSounds;
    [SerializeField] private GameObject _mainTitle;
    [SerializeField] private GameObject StoreText;
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;


    }

    private void Start()
    {
        SetMusicVolume();
        SetSfxVolume();
        PauseController.IsPaused = false;
    }

    void Update()
    {
        if (PauseController.IsPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }


    public void LoadScene(string title)
    {
        SceneManager.LoadScene(title);
    }

    public void ButtonFeedbackWithRotate(Button button)
    {

        _buttonSounds[Random.Range(0, _buttonSounds.Count)].Play();
        Sequence feedBackSequence = DOTween.Sequence();
        feedBackSequence.Append(button.transform.DOScale(1.1f, 0.2f).SetEase(Ease.Linear));
        feedBackSequence.Append(button.transform.DOScale(1f, 0.2f).SetEase(Ease.Linear));
        feedBackSequence.Join(button.transform.DORotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360)
            .SetEase(Ease.OutCubic));
        feedBackSequence.Play();
    }

    public void ButtonFeedback(Button button)
    {

        _buttonSounds[Random.Range(0, _buttonSounds.Count)].Play();
        Sequence feedBackSequence = DOTween.Sequence();
        feedBackSequence.Append(button.transform.DOScale(1.1f, 0.2f).SetEase(Ease.Linear));
        feedBackSequence.Append(button.transform.DOScale(1f, 0.2f).SetEase(Ease.Linear));
        feedBackSequence.Play();


    }

    public void OpenPanel(GameObject panel)
    {

        if (_mainTitle)
        {
            _mainTitle.SetActive(false);
        }

        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        if (_menuCanvasGroup)
        {
            _menuCanvasGroup.GetComponent<CanvasGroup>().DOFade(0.02f, 0.5f);
        }


        Sequence showSequence = DOTween.Sequence();
        showSequence.Append(panel.GetComponent<Transform>().DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
        showSequence.Join((canvasGroup.DOFade(1f, 0.5f)));
        showSequence.Play();
    }

    public void ClosePanel(GameObject panel)
    {
        if (_mainTitle)
        {
            _mainTitle.SetActive(true);
        }

        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        if (_menuCanvasGroup)
        {
            _menuCanvasGroup.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        }


        Sequence hideSequence = DOTween.Sequence();
        hideSequence.Append(panel.GetComponent<Transform>().DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack));
        hideSequence.Join(canvasGroup.DOFade(0f, 0.5f));
        hideSequence.Play();
    }

    public void SetMusicVolume()
    {
        Data.MusicVolume = _musicSlider.value;
        _mixer.SetFloat("Music", Mathf.Log10(Data.MusicVolume) * 20);
    }

    public void SetSfxVolume()
    {
        Data.SfxVolume = _sfxSlider.value;
        _mixer.SetFloat("Sfx", Mathf.Log10(Data.SfxVolume) * 20);
    }
    
  
  

}