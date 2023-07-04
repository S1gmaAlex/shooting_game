using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Boss))]
public class ViewEdit : Editor
{
    private void OnSceneGUI()
    {
        Boss fov = (Boss)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.howClose);
        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle  / 2);

        Handles.color = Color.red;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.howClose);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.howClose);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.Target.transform.position);
        }
    }
    private Vector3 DirectionFromAngle (float eulerY, float angleInDegress)
    {
        angleInDegress += eulerY;
        return new Vector3(Mathf.Sin(angleInDegress * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegress * Mathf.Deg2Rad));
    }
}
