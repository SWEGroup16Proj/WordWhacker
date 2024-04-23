using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Tracked Data")]
    [SerializeField] public int health=3;
    [SerializeField] int points=0;
    [SerializeField] pointCounter counter;

    // Start is called before the first frame update
    void Start()
    {
        points=0;
        GameManager.SetPlayer(this);
    }

    // Update is called once per frame
      public void IncrementPoints(int p)
   {
        points+=p;
        counter.addPoints(points);
   }
}
