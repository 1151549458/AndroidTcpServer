using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SQFramework
{
    public class GUIEventListener : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IUpdateSelectedHandler
    {
        public delegate void VoidDelegate(GameObject go);

        public delegate void IntDelegate(GameObject go, int index);
        public delegate void IntIntDelegate(GameObject go, int index, int indey);
        public delegate void IntIntIntDelegate(GameObject go, int index, int indey, int indek);
        public VoidDelegate onClick;
        public IntDelegate onIntClick;
        public IntIntDelegate onIntIntClick;
        public IntIntIntDelegate onIntIntIntClick;
        public VoidDelegate onDown;
        public VoidDelegate onEnter;
        public VoidDelegate onExit;
        public VoidDelegate onUp;
        public VoidDelegate onSelect;
        public VoidDelegate onUpdateSelect;

        [HideInInspector]
        private int index;
        private int indey;
        private int indek;

        static public GUIEventListener Get(GameObject go)
        {
            GUIEventListener listener = go.GetComponent<GUIEventListener>();
            if (listener == null) listener = go.AddComponent<GUIEventListener>();
            return listener;
        }

        static public GUIEventListener Get(GameObject go, int i)
        {
            GUIEventListener listener = go.GetComponent<GUIEventListener>();
            if (listener == null) listener = go.AddComponent<GUIEventListener>();
            listener.index = i;
            return listener;
        }
        static public GUIEventListener Get(GameObject go, int i, int j)
        {
            GUIEventListener listener = go.GetComponent<GUIEventListener>();
            if (listener == null) listener = go.AddComponent<GUIEventListener>();
            listener.index = i;
            listener.indey = j;
            return listener;
        }
        static public GUIEventListener Get(GameObject go, int i, int j, int k)
        {
            GUIEventListener listener = go.GetComponent<GUIEventListener>();
            if (listener == null) listener = go.AddComponent<GUIEventListener>();
            listener.index = i;
            listener.indey = j;
            listener.indek = k;
            return listener;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null) onClick(gameObject);
            else if (onIntClick != null) onIntClick(gameObject, index);
            else if (onIntIntClick != null) onIntIntClick(gameObject, index, indey);
            else if (onIntIntIntClick != null) onIntIntIntClick(gameObject, index, indey, indek);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (onUp != null) onUp(gameObject);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (onDown != null) onDown(gameObject);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (onEnter != null) onEnter(gameObject);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (onExit != null) onExit(gameObject);
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (onSelect != null) onSelect(gameObject);
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            if (onUpdateSelect != null) onUpdateSelect(gameObject);
        }
    }
}