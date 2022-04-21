using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    [SerializeField] private string name;
    public string sceneName
    {
        get { return name; }
    }

}
