using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    [CreateAssetMenu(fileName = "New Dialogue Script", menuName = "Scriptable Objects/Dialogue Script")]
    public class DialogueSO : ScriptableObject
    {
        public Dialogue dialogue;
    }
}
