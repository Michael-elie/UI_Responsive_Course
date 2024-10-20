using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Michael.Scripts
{
    [System.Serializable]
    public class FruitData {

        public GameObject Prefab; 
        public float SpawnProbability;
    }
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<FruitData> fruitsList;
        [SerializeField] private float left;
        [SerializeField] private float right;
        private float _timer;

        private void OnEnable()
        {
            BeatManager.onBeatTrigger += SpawnRandomly;
        }

        private void OnDisable()
        {
            BeatManager.onBeatTrigger -= SpawnRandomly;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= 10)
            {
                fruitsList[0].SpawnProbability = 0.6f;
            }
        }

        private void SpawnRandomly()
        {
            int randomFruit = GetRandomFruitIndex();
            GameObject fruit = Instantiate(fruitsList[randomFruit].Prefab, transform.position, fruitsList[randomFruit].Prefab.transform.rotation);
        
            fruit.GetComponent<Rigidbody>().AddForce(RandomVector() * RandomForce(),ForceMode.Impulse);
            fruit.transform.rotation = Random.rotation;
            
            Destroy(fruit,3f);
        }
        
        private int GetRandomFruitIndex() {
            float totalProbability = 0f;
            
            foreach (FruitData fruitData in fruitsList) {
                totalProbability += fruitData.SpawnProbability;
            }
            
            float randomValue = Random.Range(0f, totalProbability);
            float cumulativeProbability = 0f;
            
            for (int i = 0; i < fruitsList.Count; i++) {
                cumulativeProbability += fruitsList[i].SpawnProbability;
                if (randomValue <= cumulativeProbability) {
                    return i;
                }
            }

            return 0; 
        }
    
        private float RandomForce() {
            float force = Random.Range(10f, 13.5f);
            return force;
        }
    
        private float RandomRepeat() {
            float repeatRate = Random.Range(0.5f, 3f);
            return repeatRate;
        }
    
        private Vector2 RandomVector() {
            Vector2 moveDirection = new Vector2(Random.Range(left,right),1).normalized;
            return moveDirection;
        }
    
    
    }
}
