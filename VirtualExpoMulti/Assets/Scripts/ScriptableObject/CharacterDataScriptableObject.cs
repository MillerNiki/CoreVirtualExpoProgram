using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirtualExpo.CharacterDatas
{

    [CreateAssetMenu(fileName = "Custom Character", menuName = "New Custom Character")]
    public class CharacterDataScriptableObject : ScriptableObject
    {

        [SerializeField]
        internal int characterID;
        [SerializeField]
        internal GameObject charPrefab;

    }

}