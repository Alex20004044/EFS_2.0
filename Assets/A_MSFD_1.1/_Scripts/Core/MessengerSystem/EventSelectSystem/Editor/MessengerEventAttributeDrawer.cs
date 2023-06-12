using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using System.Linq;

namespace MSFD
{
    public class MessengerEventAttributeDrawer : OdinAttributeDrawer<MessengerEventAttribute, string>
    {
        static Color existingEventChoosedColor = Color.green;
        static Color warningColor = Color.yellow;
        static Color globalColor = Color.white;
        protected override void DrawPropertyLayout(GUIContent label)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            string currentEvent = ValueEntry.SmartValue;

            List<string> eventsWithSources;
            List<string> events;
            events = MessengerEventStore.GetEvents(out eventsWithSources);

            int index = events.IndexOf(currentEvent);
            string displayedLabel;
            if (index == -1)
            {
                currentEvent = "Mode: Custom Event!";
                GUI.contentColor = warningColor;
            }
            else
            {
                currentEvent = eventsWithSources[index];
                GUI.contentColor = existingEventChoosedColor;
            }

            var selectedValues = OdinSelector<string>.DrawSelectorDropdown(rect, currentEvent, rect =>
            {
                var selector = new GenericSelector<string>(eventsWithSources);
                selector.EnableSingleClickToSelect();
                selector.ShowInPopup(rect);
                return selector;
            });
            if (selectedValues != null)
            {
                ValueEntry.SmartValue = MessengerEventStore.RemoveSourceFromEvent(selectedValues.First());
            }
            GUI.contentColor = globalColor;

            CallNextDrawer(label);
        }
    }
}