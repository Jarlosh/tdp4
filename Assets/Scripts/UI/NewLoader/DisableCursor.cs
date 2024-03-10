using System;
using UnityEngine;

namespace UI.NewLoader
{
    public class DisableCursor: MonoBehaviour
    {
        private void Awake()
        {
            Cursor.visible = false;
        }

        private void Update()
        {
            Cursor.visible = !IsMouseInBounds();
        }
        
        public static bool IsMouseInBounds()
        {
            var mp = Input.mousePosition;
            return mp.x > 0 && mp.x < Screen.width && mp.y >= 0 && mp.y < Screen.height;
        }
    }
}