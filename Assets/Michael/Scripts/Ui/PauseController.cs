using UnityEngine;

public class PauseController : MonoBehaviour
{
   public static bool IsPaused = false;
    void Start()
    {
        
    }

    public void Pause()
    {
        IsPaused = !IsPaused;
    }

    // Update is called once per frame
  
    
    
    
}
