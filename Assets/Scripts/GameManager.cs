using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager static singleton class initialization
    private static GameManager instance = new GameManager();
    public static GameManager Instance { get { return instance; } }
    private GameManager() { }
}
