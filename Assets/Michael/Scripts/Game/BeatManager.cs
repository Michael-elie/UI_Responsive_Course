using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    public static Action onBeatTrigger;
    public static Action onPerfectHit;
    [SerializeField] private float bpm;
    [SerializeField] private AudioSource source;
    [SerializeField] private List<Intervals> intervals;
    [SerializeField] private float timingTolerance = 0.1f;
    

    private void OnEnable()
    {
        BladeController.OnTargetSliced += CheckPlayerInput; 
    }

    private void OnDisable()
    {
        BladeController.OnTargetSliced -= CheckPlayerInput;
    }

    
    void Update() {
        
        if (!PauseController.IsPaused)
        {
            foreach (Intervals interval in intervals) {
                float sampledTime = (source.timeSamples / (source.clip.frequency * interval.GetIntervalength(bpm)));
                interval.CheckForNewInterval(sampledTime);
            }
        }
      
    }

    private void CheckPlayerInput()
    {
        float currentTime = source.timeSamples / (float)source.clip.frequency;
        foreach (Intervals interval in intervals)
        {
            float intervalLength = interval.GetIntervalength(bpm);
            float closestBeatTime = Mathf.Floor(currentTime / intervalLength) * intervalLength;
            
            if (Mathf.Abs(currentTime - closestBeatTime) <= timingTolerance)
            {
                Debug.Log("Perfect hit!");
                onPerfectHit?.Invoke();
                GameManager.Score += 4;
            }
        }
    }

    [System.Serializable] 
    public class Intervals {
        
        [SerializeField] private float steps;
        [SerializeField] private List<float> skips;
        private int _lastInterval;

        public float GetIntervalength(float bpm) {
            return 60f / (bpm * steps);
        }

        public void CheckForNewInterval(float interval) {
            
            if (Mathf.FloorToInt(interval) != _lastInterval) {
                _lastInterval = Mathf.FloorToInt(interval);

                if (!skips.Contains(_lastInterval))
                {
                    onBeatTrigger?.Invoke();
                    Debug.Log("beat");
                }
            }
        }
    }
}
