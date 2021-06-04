using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class GUILayoutEx {

    public static VisualElement UIElement (VisualElement element, params GUILayoutOption[] options) =>
        DoLayout(element, GUILayoutUtility.GetRect(0, 4000, 0, 4000, options));

    public static VisualElement UIElement (VisualElement element, GUIStyle style, params GUILayoutOption[] options) =>
        DoLayout(element, GUILayoutUtility.GetRect(0, 4000, 0, 4000, style, options));

    public static VisualElement UIElement (VisualElement element, float width, float height, params GUILayoutOption[] options) =>
        DoLayout(element, GUILayoutUtility.GetRect(width, height, options));

    public static VisualElement UIElement (VisualElement element, float width, float height, GUIStyle style, params GUILayoutOption[] options) =>
        DoLayout(element, GUILayoutUtility.GetRect(width, height, style, options));

    public static VisualElement UIElement (VisualElement element, float minWidth, float maxWidth, float minHeight, float maxHeight, params GUILayoutOption[] options) =>
        DoLayout(element, GUILayoutUtility.GetRect(minWidth, maxWidth, minHeight, maxHeight, options));

    public static VisualElement UIElement (VisualElement element, float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, params GUILayoutOption[] options) =>
        DoLayout(element, GUILayoutUtility.GetRect(minWidth, maxWidth, minHeight, maxHeight, style, options));

    private static VisualElement DoLayout (VisualElement element, Rect geometry) {
        if (Event.current.type == EventType.Layout) {
            element.style.position = Position.Absolute;
        } else if (Event.current.type == EventType.Repaint) {
            element.style.left = geometry.xMin;
            element.style.top = geometry.yMin;
            element.style.width = geometry.width;
            element.style.height = geometry.height;
        }
        return element;
    }

}
