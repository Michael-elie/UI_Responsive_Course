using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static float MusicVolume;
    public static float SfxVolume;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private List<GameObject> _menuPanels;
    
    void Start()
    {
        
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }
    
    public void MenuPanel(GameObject panel) {
        foreach (GameObject menuPanel in _menuPanels)
        {
            menuPanel.SetActive(panel == menuPanel);
        }
    }
    public void SetMusicVolume()
    {
        MusicVolume = _musicSlider.value;
        _mixer.SetFloat("Music", Mathf.Log10(MusicVolume) * 20);
    }

    public void SetSfxVolume()
    {
        SfxVolume = _sfxSlider.value;
        _mixer.SetFloat("Sfx", Mathf.Log10(SfxVolume) * 20);
    }
    
    
    
}
