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
    public GameObject CurrentBladeSlots;
    public TextMeshProUGUI DiscCountText;
    public GameObject StoreText;
    public GameObject BladeItemPrefab;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private GameObject _menuCanvasGroup;
    [SerializeField] private List<AudioSource> _buttonSounds;
    [SerializeField] private GameObject _mainTitle;
 
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;


    }

    private void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Menu") )
        {
            UpdateInventoryUI();
        }
        _sfxSlider.value = Data.SfxVolume;
        _musicSlider.value = Data.MusicVolume;
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
    
    public void UpdateInventoryUI()
    {
        foreach (var blade in Data.Items )
        {
            if (blade != Data.CurrentBlade)
            {
                Debug.Log("est different ");
                foreach (GameObject slot in InventorySlots)
                {
                    if (slot.transform.childCount == 0)
                    {
                        GameObject newBlade = Instantiate(BladeItemPrefab, slot.transform);
                        newBlade.GetComponent<Image>().color = blade.IconColor;
                        newBlade.GetComponent<DraggableBladeItem>().BladeMaterial = blade.BladeMaterial;
                        newBlade.GetComponent<DraggableBladeItem>().bladeData = blade;
                        break;
                    }
                   
                }
            }
            else 
            {
                GameObject newBlade = Instantiate(BladeItemPrefab, CurrentBladeSlots.transform);
                newBlade.GetComponent<Image>().color = Data.CurrentBlade.IconColor;
                newBlade.GetComponent<DraggableBladeItem>().BladeMaterial = Data.CurrentBlade.BladeMaterial;
                newBlade.GetComponent<DraggableBladeItem>().bladeData = blade;
            }
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

        Handheld.Vibrate();
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
    
   /* public void loadInventory()
    {
        foreach (int bladeindex in MenuManager.Instance.Data.BladesUnlocked)
        {
              
            foreach (GameObject slot in Instance.InventorySlots)
            {
                if (slot.transform.childCount == 0)
                {
                    GameObject newBlade = Instantiate(Instance.Blades[bladeindex], slot.transform);
                    return;
                }
            }
        }
    }*/
    
    
   public void UpdateInventoryUITEST()
   {
       
       foreach (GameObject slot in InventorySlots)
       {
           foreach (var blade in Data.Items)
           {
               if (blade != Data.CurrentBlade)
               {
                   if (slot.transform.childCount == 0)
                   {
                       GameObject newBlade = Instantiate(BladeItemPrefab, slot.transform);
                       newBlade.GetComponent<Image>().color = blade.IconColor;
                       newBlade.GetComponent<DraggableBladeItem>().BladeMaterial = blade.BladeMaterial;
                       newBlade.GetComponent<DraggableBladeItem>().bladeData = blade;
                       // return;
                   }
               }
               else 
               {
                   GameObject newBlade = Instantiate(BladeItemPrefab, CurrentBladeSlots.transform);
                   newBlade.GetComponent<Image>().color = Data.CurrentBlade.IconColor;
                   newBlade.GetComponent<DraggableBladeItem>().BladeMaterial = Data.CurrentBlade.BladeMaterial;
                   newBlade.GetComponent<DraggableBladeItem>().bladeData = blade;
               }
              
           }
       }
          
   }

}



