using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

#pragma warning disable IDE0017 // Simplify object initialization
#pragma warning disable IDE0039 // Use local function

public static class Example {

    #region Styling

    public const int Margin = 15;
    public const int Spacing = 10;

    private static GUIStyle marginStyle;
    private static GUIStyle largeButtonStyle;
    private static GUIStyle headerStyle;
    private static GUIStyle footerStyle;

    public static GUIStyle MarginStyle {
        get => marginStyle ??= new GUIStyle { margin = new RectOffset(Margin, Margin, Margin, Margin) };
    }

    public static GUIStyle LargeButtonStyle {
        get => largeButtonStyle ??= new GUIStyle(GUI.skin.button) {
            fixedWidth = GUI.skin.button.CalcSize(new GUIContent("Cancel")).x * 1.5f,
            fixedHeight = GUI.skin.button.CalcSize(new GUIContent("Cancel")).y * 1.25f,
            margin = new RectOffset(Spacing, 0, 0, 0)
        };
    }

    public static GUIStyle HeaderStyle {
        get => headerStyle ??= new GUIStyle(EditorStyles.boldLabel) {
            margin = new RectOffset(0, 0, 0, Spacing)
        };
    }

    public static GUIStyle FooterStyle {
        get => footerStyle ??= new GUIStyle() {
            margin = new RectOffset(0, 0, Spacing, 0)
        };
    }

    public static T ShowExample<T> (Vector2? minSize = null) where T : EditorWindow {
        T window = EditorWindow.GetWindowWithRect<T>(new Rect(0, 0, 500, 500), false, typeof(T).Name);
        window.minSize = minSize ?? new Vector2(400, 150);
        window.maxSize = new Vector2(4000, 4000);
        return window;
    }

    #endregion

    public static VisualElement CreateSomeKindOfVisualElement () {

        // Based on https://docs.unity3d.com/ScriptReference/UIElements.ListView.html
        // Updated to use non-deprecated events.

        const int itemCount = 1000;
        var items = new List<string>(itemCount);
        for (int i = 1; i <= itemCount; i++)
            items.Add(i.ToString());

        Func<VisualElement> makeItem = () => new Label();
        Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = items[i];
        const int itemHeight = 16;

        var listView = new ListView(items, itemHeight, makeItem, bindItem);
        listView.selectionType = SelectionType.Multiple;
        listView.onItemsChosen += objects => Debug.Log(string.Join(", ", objects));
        listView.onSelectionChange += objects => Debug.Log(string.Join(", ", objects));
        // Original example set flexGrow; removed because we'll do it elsewhere.

        // Not in original example: make it look a little more like inspector (just because)
        StyleColor borderDark = new StyleColor(new Color32(160, 160, 160, 255));
        StyleColor borderLite = new StyleColor(new Color32(183, 183, 183, 255));
        listView.style.backgroundColor = new StyleColor(new Color32(240, 240, 240, 255));
        listView.style.borderLeftColor = listView.style.borderBottomColor = borderLite;
        listView.style.borderRightColor = listView.style.borderTopColor = borderDark;
        listView.style.borderLeftWidth = listView.style.borderBottomWidth =
            listView.style.borderRightWidth = listView.style.borderTopWidth = 1;
        listView.style.borderBottomLeftRadius = listView.style.borderBottomRightRadius =
            listView.style.borderTopLeftRadius = listView.style.borderTopRightRadius = 3;

        return listView;

    }

}

#pragma warning restore IDE0017 // Simplify object initialization
