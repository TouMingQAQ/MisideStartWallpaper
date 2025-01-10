#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;
using static VInspector.Libs.VUtils;
using static VInspector.Libs.VGUI;


namespace VInspector
{
    class VInspectorMenu
    {
        public static bool navigationBarEnabled { get => EditorPrefs.GetBool("vInspector-navigationBarEnabled", false); set => EditorPrefs.SetBool("vInspector-navigationBarEnabled", value); }
        public static bool copyPasteButtonsEnabled { get => EditorPrefs.GetBool("vInspector-copyPasteButtonsEnabled", false); set => EditorPrefs.SetBool("vInspector-copyPasteButtonsEnabled", value); }
        public static bool saveInPlaymodeButtonEnabled { get => EditorPrefs.GetBool("vInspector-saveInPlaymodeButtonEnabled", false); set => EditorPrefs.SetBool("vInspector-saveInPlaymodeButtonEnabled", value); }
        public static bool componentWindowsEnabled { get => EditorPrefs.GetBool("vInspector-componentWindowsEnabled", false); set => EditorPrefs.SetBool("vInspector-componentWindowsEnabled", value); }
        public static bool componentAnimationsEnabled { get => EditorPrefs.GetBool("vInspector-componentAnimationsEnabled", false); set => EditorPrefs.SetBool("vInspector-componentAnimationsEnabled", value); }
        public static bool minimalModeEnabled { get => EditorPrefs.GetBool("vInspector-minimalModeEnabled", false); set => EditorPrefs.SetBool("vInspector-minimalModeEnabled", value); }
        public static bool attributesEnabled { get => EditorPrefs.GetBool("vInspector-attributesEnabled", false); set => EditorPrefs.SetBool("vInspector-attributesEnabled", value); }
        public static bool resettableVariablesEnabled { get => EditorPrefs.GetBool("vInspector-resettableVariablesEnabled", false); set => EditorPrefs.SetBool("vInspector-resettableVariablesEnabled", value); }
        public static bool hideScriptFieldEnabled { get => EditorPrefs.GetBool("vInspector-hideScriptFieldEnabled", false); set => EditorPrefs.SetBool("vInspector-hideScriptFieldEnabled", value); }
        public static bool hideHelpButtonEnabled { get => !helpButtonEnabled; set => helpButtonEnabled = !value; }
        public static bool hidePresetsButtonEnabled { get => !presetsButtonEnabled; set => presetsButtonEnabled = !value; }

        public static bool toggleActiveEnabled { get => EditorPrefs.GetBool("vInspector-toggleActiveEnabled", true); set => EditorPrefs.SetBool("vInspector-toggleActiveEnabled", value); }
        public static bool deleteEnabled { get => EditorPrefs.GetBool("vInspector-deleteEnabled", true); set => EditorPrefs.SetBool("vInspector-deleteEnabled", value); }
        public static bool toggleExpandedEnabled { get => EditorPrefs.GetBool("vInspector-toggleExpandedEnabled", true); set => EditorPrefs.SetBool("vInspector-toggleExpandedEnabled", value); }
        public static bool collapseEverythingElseEnabled { get => EditorPrefs.GetBool("vInspector-collapseEverythingElseEnabled", true); set => EditorPrefs.SetBool("vInspector-collapseEverythingElseEnabled", value); }
        public static bool collapseEverythingEnabled { get => EditorPrefs.GetBool("vInspector-collapseEverythingEnabled", true); set => EditorPrefs.SetBool("vInspector-collapseEverythingEnabled", value); }

        public static bool attributesDisabled { get => IsSymbolDefinedInAsmdef(nameof(VInspector), "VINSPECTOR_ATTRIBUTES_DISABLED"); set => SetSymbolDefinedInAsmdef(nameof(VInspector), "VINSPECTOR_ATTRIBUTES_DISABLED", value); }
        public static bool pluginDisabled { get => EditorPrefs.GetBool("vInspector-pluginDisabled-" + GetProjectId(), false); set => EditorPrefs.SetBool("vInspector-pluginDisabled-" + GetProjectId(), value); }


        public static int componentButtons_defaultButtonsCount { get => EditorPrefs.GetInt("vInspector-componentButtons_defaultButtonsCount", 3); set => EditorPrefs.SetInt("vInspector-componentButtons_defaultButtonsCount", value); }
        public static bool menuButtonEnabled { get => componentButtons_defaultButtonsCount >= 1; set => componentButtons_defaultButtonsCount = value ? 1 : 0; }
        public static bool presetsButtonEnabled { get => componentButtons_defaultButtonsCount >= 2; set => componentButtons_defaultButtonsCount = value ? 2 : 1; }
        public static bool helpButtonEnabled { get => componentButtons_defaultButtonsCount >= 3; set => componentButtons_defaultButtonsCount = value ? 3 : 2; }



        public static void RepaintInspectors()
        {
            Resources.FindObjectsOfTypeAll(typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow"))
                     .Cast<EditorWindow>()
                     .ForEach(r => r.Repaint());

            Resources.FindObjectsOfTypeAll(typeof(Editor).Assembly.GetType("UnityEditor.PropertyEditor"))
                     .Where(r => r.GetType().BaseType == typeof(EditorWindow))
                     .Cast<EditorWindow>()
                     .ForEach(r => r.Repaint());
        }



        const string dir = "Tools/vInspector/";
#if UNITY_EDITOR_OSX
        const string cmd = "Cmd";
#else
        const string cmd = "Ctrl";
#endif

        const string navigationBar = dir + "Navigation bar";
        const string copyPasteButtons = dir + "Copy \u2215 Paste components";
        const string saveInPlaymodeButton = dir + "Save in play mode";
        const string componentWindows = dir + "Component windows";
        const string componentAnimations = dir + "Component animations";
        const string minimalMode = dir + "Minimal mode";
        const string resettableVariables = dir + "Resettable variables";
        const string hideScriptField = dir + "Hide script field";
        const string hideHelpButton = dir + "Hide help button";
        const string hidePresetsButton = dir + "Hide presets button";

        const string toggleActive = dir + "A to toggle component active";
        const string delete = dir + "X to delete component";
        const string toggleExpanded = dir + "E to expand \u2215 collapse component";
        const string collapseEverythingElse = dir + "Shift-E to expand only one component";
        const string collapseEverything = dir + "Ctrl-Shift-E to expand \u2215 collapse all components";

        const string disableAttributes = dir + "Disable attributes";
        const string disablePlugin = dir + "Disable vInspector";







        [MenuItem(dir + "Features", false, 1)] static void dadsas() { }
        [MenuItem(dir + "Features", true, 1)] static bool dadsas123() => false;

        [MenuItem(navigationBar, false, 2)] static void dadsadsadasdsadadsas() { navigationBarEnabled = !navigationBarEnabled; RepaintInspectors(); }
        [MenuItem(navigationBar, true, 2)] static bool dadsaddsasadadsdasadsas() { Menu.SetChecked(navigationBar, navigationBarEnabled); return !pluginDisabled; }

        [MenuItem(copyPasteButtons, false, 3)] static void dadsaasddsadaasdsdsadadsas() { copyPasteButtonsEnabled = !copyPasteButtonsEnabled; RepaintInspectors(); }
        [MenuItem(copyPasteButtons, true, 3)] static bool dadsaddasdsasaasddadsdasadsas() { Menu.SetChecked(copyPasteButtons, copyPasteButtonsEnabled); return !pluginDisabled; }

        [MenuItem(saveInPlaymodeButton, false, 4)] static void dadsadsadaasasdsdsadadsas() { saveInPlaymodeButtonEnabled = !saveInPlaymodeButtonEnabled; RepaintInspectors(); }
        [MenuItem(saveInPlaymodeButton, true, 4)] static bool dadsaddsasaadsasddadsdasadsas() { Menu.SetChecked(saveInPlaymodeButton, saveInPlaymodeButtonEnabled); return !pluginDisabled; }

        [MenuItem(componentWindows, false, 5)] static void dadsadsadaasdsdsadadsas() { componentWindowsEnabled = !componentWindowsEnabled; RepaintInspectors(); }
        [MenuItem(componentWindows, true, 5)] static bool dadsaddsasaasddadsdasadsas() { Menu.SetChecked(componentWindows, componentWindowsEnabled); return !pluginDisabled; }

        [MenuItem(componentAnimations, false, 6)] static void dadsadsadsadaasdsdsadadsas() { componentAnimationsEnabled = !componentAnimationsEnabled; RepaintInspectors(); }
        [MenuItem(componentAnimations, true, 6)] static bool dadsadddsasasaasddadsdasadsas() { Menu.SetChecked(componentAnimations, componentAnimationsEnabled); return !pluginDisabled; }

        [MenuItem(minimalMode, false, 7)] static void dadsadsadsadsadasdsadadsas() { minimalModeEnabled = !minimalModeEnabled; RepaintInspectors(); }
        [MenuItem(minimalMode, true, 7)] static bool dadsadasdasddsasadadsdasadsas() { Menu.SetChecked(minimalMode, minimalModeEnabled); return !pluginDisabled; }

        [MenuItem(resettableVariables, false, 8)] static void dadsadsadsadasdsadadsas() { resettableVariablesEnabled = !resettableVariablesEnabled; RepaintInspectors(); }
        [MenuItem(resettableVariables, true, 8)] static bool dadsadasddsasadadsdasadsas() { Menu.SetChecked(resettableVariables, resettableVariablesEnabled); return !pluginDisabled; }

        [MenuItem(hideScriptField, false, 9)] static void dadsadsdsaadsadsadasdsadadsas() { hideScriptFieldEnabled = !hideScriptFieldEnabled; RepaintInspectors(); }
        [MenuItem(hideScriptField, true, 9)] static bool dadsadasadsdasddsasadadsdasadsas() { Menu.SetChecked(hideScriptField, hideScriptFieldEnabled); return !pluginDisabled; }

        [MenuItem(hideHelpButton, false, 10)] static void dadsadsadsdsaadsadsadasdsadadsas() { hideHelpButtonEnabled = !hideHelpButtonEnabled; RepaintInspectors(); }
        [MenuItem(hideHelpButton, true, 10)] static bool dadsaadsdasadsdasddsasadadsdasadsas() { Menu.SetChecked(hideHelpButton, hideHelpButtonEnabled); return !pluginDisabled; }

        [MenuItem(hidePresetsButton, false, 11)] static void dadsadsdsaadssdadsadasdsadadsas() { hidePresetsButtonEnabled = !hidePresetsButtonEnabled; RepaintInspectors(); }
        [MenuItem(hidePresetsButton, true, 11)] static bool dadsadasadsddsasddsasadadsdasadsas() { Menu.SetChecked(hidePresetsButton, hidePresetsButtonEnabled); return !pluginDisabled; }




        [MenuItem(dir + "Shortcuts", false, 1001)] static void dadsadsas() { }
        [MenuItem(dir + "Shortcuts", true, 1001)] static bool dadsadsas123() => false;

        [MenuItem(toggleActive, false, 1002)] static void dadsadadsas() => toggleActiveEnabled = !toggleActiveEnabled;
        [MenuItem(toggleActive, true, 1002)] static bool dadsaddasadsas() { Menu.SetChecked(toggleActive, toggleActiveEnabled); return !pluginDisabled; }

        [MenuItem(delete, false, 1003)] static void dadsadsadasdadsas() => deleteEnabled = !deleteEnabled;
        [MenuItem(delete, true, 1003)] static bool dadsaddsasaddasadsas() { Menu.SetChecked(delete, deleteEnabled); return !pluginDisabled; }

        [MenuItem(toggleExpanded, false, 1004)] static void dadsaddsasadasdsadadsas() => toggleExpandedEnabled = !toggleExpandedEnabled;
        [MenuItem(toggleExpanded, true, 1004)] static bool dadsaddsdsasadadsdasadsas() { Menu.SetChecked(toggleExpanded, toggleExpandedEnabled); return !pluginDisabled; }

        [MenuItem(collapseEverythingElse, false, 1005)] static void dadsadsasdadasdsadadsas() => collapseEverythingElseEnabled = !collapseEverythingElseEnabled;
        [MenuItem(collapseEverythingElse, true, 1005)] static bool dadsaddsdasasadadsdasadsas() { Menu.SetChecked(collapseEverythingElse, collapseEverythingElseEnabled); return !pluginDisabled; }

        [MenuItem(collapseEverything, false, 1006)] static void dadsadsdasadasdsadadsas() => collapseEverythingEnabled = !collapseEverythingEnabled;
        [MenuItem(collapseEverything, true, 1006)] static bool dadsaddssdaasadadsdasadsas() { Menu.SetChecked(collapseEverything, collapseEverythingEnabled); return !pluginDisabled; }




        [MenuItem(dir + "More", false, 10001)] static void daasadsddsas() { }
        [MenuItem(dir + "More", true, 10001)] static bool dadsadsdasas123() => false;

        [MenuItem(dir + "Open manual", false, 10002)]
        static void dadadssadsas() => Application.OpenURL("https://kubacho-lab.gitbook.io/vinspector2");

        [MenuItem(dir + "Join our Discord", false, 10003)]
        static void dadasdsas() => Application.OpenURL("https://discord.gg/pUektnZeJT");




        // [MenuItem(dir + "Clear state", false, 5555)] static void dassaadsasddc() { VInspectorState.Clear(); VInspector.firstAttrStateCacheLayer.Clear(); RepaintInspectors(); }
        // [MenuItem(dir + "Clear state", true, 5555)] static bool dassaadsadsasddc() { return !pluginDisabled; }

        // [MenuItem(dir + "Save state", false, 5556)] static void dassaaasddsasddc() { VInspectorState.Save(); }
        // [MenuItem(dir + "Save state", true, 5556)] static bool dassaadsaasddsasddc() { return !pluginDisabled; }

        // [MenuItem(disableAttributes, false, 5557)] static void dadsadsdasadasdasdsadadsas() { attributesDisabled = !attributesDisabled; }
        // [MenuItem(disableAttributes, true, 5557)] static bool dadsaddssdaasadsadadsdasadsas() { Menu.SetChecked(disableAttributes, attributesDisabled); return !pluginDisabled; }




        [MenuItem(disablePlugin, false, 100001)] static void dadsadsdsdasadasdasdsadadsas() { pluginDisabled = !pluginDisabled; if (!pluginDisabled) EditorPrefs.SetBool("vInspector-pluginWasReenabled", true); attributesDisabled = pluginDisabled; }
        [MenuItem(disablePlugin, true, 100001)] static bool dadsaddssdsdaasadsadadsdasadsas() { Menu.SetChecked(disablePlugin, pluginDisabled); return true; }



    }
}
#endif