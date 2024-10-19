using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Michael.Scripts
{
    public class Spawner : MonoBehaviour 
    { 
        [SerializeField]private GameObject[] fruitPrefabs;
        [SerializeField] private float left;
        [SerializeField] private float right;
        [SerializeField] private BeatManager _beatManager;
        void Start() {
            //InvokeRepeating("SpawnRandomly",3f,RandomRepeat());
        }

        private void OnEnable()
        {
            BeatManager.onBeatTrigger += SpawnRandomly;
        }

        private void OnDisable()
        {
            BeatManager.onBeatTrigger -= SpawnRandomly;
        }

        private void SpawnRandomly() {
            int randomFruit = Random.Range(0, fruitPrefabs.Length);
            GameObject fruit = Instantiate(fruitPrefabs[randomFruit], transform.position, fruitPrefabs[randomFruit].transform.rotation);
        
            fruit.GetComponent<Rigidbody>().AddForce(RandomVector() * RandomForce(),ForceMode.Impulse);
            fruit.transform.rotation = Random.rotation;
            
            Destroy(fruit,3f);
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
