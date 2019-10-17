using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    public static class CursorTools
    {
        static private List<Object> _cursorNeedObjects = new List<Object>();

        static public void AskForCursor(Object obj)
        {
            _cursorNeedObjects.Add(obj);
            UpdateCursor();
        }

        static public void LetGoCursor(Object obj)
        {
            _cursorNeedObjects.Remove(obj);
            UpdateCursor();
        }

        private static void UpdateCursor()
        {
            Cursor.lockState = _cursorNeedObjects.Count == 0 ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
