using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CollectableItem))]
    public class ItemEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            CollectableItem collectableItem = (CollectableItem)target;
            Handles.color = Color.magenta;
            Handles.DrawWireArc(collectableItem.transform.position, Vector3.up, Vector3.forward, 360, collectableItem.checkRadius);
        }
    }
}