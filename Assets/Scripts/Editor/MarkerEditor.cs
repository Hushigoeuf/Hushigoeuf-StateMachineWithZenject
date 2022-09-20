using UnityEditor;
using UnityEngine;

namespace Hushigoeuf
{
    [CustomEditor(typeof(WorldMarker), true)]
    public class MarkerEditor : Editor
    {
        private const float MARKER_RADIUS = .5f;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(WorldMarker marker, GizmoType gizmo)
        {
            Gizmos.color = marker.MarkerColor;
            Gizmos.DrawWireSphere(marker.transform.position, MARKER_RADIUS);
        }
    }
}