using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public Terrain terrain;


    void Awake()
    {
        instance = this;
    }
}