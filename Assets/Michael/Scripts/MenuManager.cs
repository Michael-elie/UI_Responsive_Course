using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
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

    
}
