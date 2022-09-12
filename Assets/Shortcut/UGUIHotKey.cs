using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class UGUIHotKey
{
    private static GameObject CheckSelection (MenuCommand menuCommand)
    {
        GameObject selectedObj = menuCommand.context as GameObject;
        
        if (selectedObj == null)
            selectedObj = Selection.activeGameObject;
        if (selectedObj == null || selectedObj != null && selectedObj.GetComponentInParent<Canvas> () == null)
            return null;
        return selectedObj;
    }

    //image
    [MenuItem ("GameObject/UGUI/Image #&i", false, 6)] 
    static void CreateImage (MenuCommand menuCommand)
    {
        GameObject selectedObj = CheckSelection (menuCommand);
        if (selectedObj == null)
            return;
        GameObject go = new GameObject ("Image");
        GameObjectUtility.SetParentAndAlign (go, selectedObj);
        Undo.RegisterCreatedObjectUndo (go, "Create " + go.name);
        Selection.activeObject = go;
        go.AddComponent<Image> ();
    }

    //text
    [MenuItem ("GameObject/UGUI/Text #&t", false, 6)]
    static void CreateText (MenuCommand menuCommand)
    {
        GameObject selectedObj = CheckSelection (menuCommand);
        if (selectedObj == null)
            return;
        GameObject go = new GameObject ("Text");
        GameObjectUtility.SetParentAndAlign (go, selectedObj);
        Undo.RegisterCreatedObjectUndo (go, "Create " + go.name);
        Selection.activeObject = go;

        Text t = go.AddComponent<Text> ();
        t.alignment = TextAnchor.MiddleCenter;
        t.color = Color.white;
        t.text = "New Text";
        t.rectTransform.sizeDelta = new Vector2 (150f, 30f);
    }
    
    //button
    [MenuItem ("GameObject/UGUI/Button #&b", false, 6)]
    static void CreateButton (MenuCommand menuCommand)
    {
        GameObject selectedObj = CheckSelection (menuCommand);
        if (selectedObj == null)
            return;
        GameObject go = new GameObject ("Button");
        GameObjectUtility.SetParentAndAlign (go, selectedObj);
        Undo.RegisterCreatedObjectUndo (go, "Create " + go.name);
        Selection.activeObject = go;
        go.AddComponent<RectTransform> ();
        go.AddComponent<Image> ();
        go.AddComponent<Button> ();
        go.AddComponent<CanvasRenderer> ();
    }
    
}
