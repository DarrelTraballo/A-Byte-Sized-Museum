using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    [CreateAssetMenu(menuName = "GameEvent")]
    public class GameEvent : ScriptableObject
    {
        public List<GameEventListener> listeners = new List<GameEventListener>();

        // Raise event through different method signatures
        public void Raise(Component sender, object data)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].OnEventRaised(sender, data);
            }
        }

        // Manage listeners
        public void RegisterListener(GameEventListener listener)
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}
