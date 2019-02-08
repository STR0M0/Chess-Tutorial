using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridPreview))]
public class GridPreviewEditor : Editor
{
    private static float lastExecutionTime;

    private void OnSceneGUI()
    {
        var targets = FindObjectsOfType<GridPreview>();

        foreach (var t in targets)
        {
            var startPos = t.transform.position + new Vector3(-1, -1) * (t.boardSize / 2);

            for (int x = 0; x < t.boardSpaces + 1; x++)
            {
                var p1 = startPos + new Vector3(x * (t.boardSize / t.boardSpaces), 0);
                var p2 = p1 + Vector3.up * t.boardSize;
                Handles.DrawLine(p1, p2);
            }
            for (int y = 0; y < t.boardSpaces + 1; y++)
            {
                var p1 = startPos + new Vector3(0, y * (t.boardSize / t.boardSpaces));
                var p2 = p1 + Vector3.right * t.boardSize;
                Handles.DrawLine(p1, p2);
            }
        }
    }
}
