using UnityEditor;
using UnityEngine;

namespace Hushigoeuf
{
    [CustomEditor(typeof(ScreenMarker), true)]
    public class ScreenMarkerEditor : MarkerEditor
    {
        private const float MARKER_LINE_SIZE = 12;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderLineFromMarkerDirection(ScreenMarker marker, GizmoType gizmo)
        {
            switch (marker.Direction)
            {
                case ScreenMarker.Directions.Left:
                    DrawVerticalLineByMarker(marker);
                    break;

                case ScreenMarker.Directions.Right:
                    DrawVerticalLineByMarker(marker);
                    break;

                case ScreenMarker.Directions.Top:
                    DrawHorizontalLineOfMarker(marker);
                    break;

                case ScreenMarker.Directions.Bottom:
                    DrawHorizontalLineOfMarker(marker);
                    break;
            }
        }

        private static void DrawHorizontalLineOfMarker(ScreenMarker marker)
        {
            Vector3 fromPosition = marker.transform.position;
            fromPosition.x -= MARKER_LINE_SIZE / 2f;

            Vector3 toPosition = fromPosition;
            toPosition.x += MARKER_LINE_SIZE;

            DrawGizmosLine(fromPosition, toPosition, marker.MarkerColor);
        }

        private static void DrawVerticalLineByMarker(ScreenMarker marker)
        {
            Vector3 fromPosition = marker.transform.position;
            fromPosition.y -= MARKER_LINE_SIZE / 2f;

            Vector3 toPosition = fromPosition;
            toPosition.y += MARKER_LINE_SIZE;

            DrawGizmosLine(fromPosition, toPosition, marker.MarkerColor);
        }

        private static void DrawGizmosLine(Vector3 fromPosition, Vector3 toPosition, Color gizmosColor)
        {
            Gizmos.color = gizmosColor;
            Gizmos.DrawLine(fromPosition, toPosition);
        }
    }
}