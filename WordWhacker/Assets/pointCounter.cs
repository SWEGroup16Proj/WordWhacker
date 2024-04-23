using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class pointCounter : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] public TMP_Text points;

    // Update is called once per frame

    public void Update()
    {

    }
   public void addPoints(int num)
   {
        points.SetText(num.ToString());
   }




}
