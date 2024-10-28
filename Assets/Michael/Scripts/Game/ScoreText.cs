using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI scoreText;
   [SerializeField] private TextMeshProUGUI highScoreText;
   [SerializeField] private Data data;

   private void Start()
   {
      RefreshScore();
   }

   private void OnEnable()
   {
      BladeController.OnTargetSliced += RefreshScore;
      BladeController.OnBombTouched += RefreshScore;
   }

   private void OnDisable()
   {
      BladeController.OnTargetSliced -= RefreshScore;
      BladeController.OnBombTouched -= RefreshScore;
   }

   private void RefreshScore() {
      scoreText.text = "score : " +  GameManager.Score;
      Sequence feedBackSequence = DOTween.Sequence();
      feedBackSequence.Append(scoreText.gameObject.transform.DOScale(1.1f, 0.2f).SetEase(Ease.Linear));
      feedBackSequence.Append(scoreText.gameObject.transform.DOScale(1f, 0.2f).SetEase(Ease.Linear));
      feedBackSequence.Play();

      if ( GameManager.Score > data.Highscore) {
         data.Highscore =  GameManager.Score;
      }
      highScoreText.text = "meilleur : " + data.Highscore;
   }
}
