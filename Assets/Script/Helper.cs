using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper 
{
    public static TextMesh CreateWorldText(Transform parent,string text,Vector3 localPosition,Color color)
    {
        GameObject gameObject = new GameObject("WorldText", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.color = color;
        return textMesh;
    }
    public static Vector3 GetMouseWorldPosition(Camera currentCamera,Vector3 mousePos)
    {
        Vector3 worldMousePosition = currentCamera.ScreenToWorldPoint(mousePos);
        worldMousePosition.z = 0;
        return worldMousePosition;
    }
    public static Vector3 GetMouseWorldPositionIn_3D(Camera cam,Vector3 mousePos)
    {
        Ray ray = cam.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f))
        {
            Debug.Log("hitted");
            return hit.point;
        }
        return Vector3.zero;
    }
}
