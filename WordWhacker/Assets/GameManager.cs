using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static Player player;

       // Method to set the player reference
    public static void SetPlayer(Player playerObject)
    {
        player = playerObject;
    }

}
