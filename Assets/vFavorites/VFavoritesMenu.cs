#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static VFavorites.Libs.VUtils;
using static VFavorites.Libs.VGUI;



namespace VFavorites
{
    public class VFavoritesMenu
    {

        public static bool changePagesWithArrowsEnabled { get => EditorPrefs.GetBool("vFavorites-changePagesWithArrowsEnabled", true); set => EditorPrefs.SetBool("vFavorites-changePagesWithArrowsEnabled", value); }
        public static bool changeSelectionWithArrowsEnabled { get => EditorPrefs.GetBool("vFavorites-changeSelectionWithArrowsEnabled", true); set => EditorPrefs.SetBool("vFavorites-changeSelectionWithArrowsEnabled", value); }
        public static bool setSelectionWithNumberKeysEnabled { get => EditorPrefs.GetBool("vFavorites-setSelectionWithNumberKeysEnabled", true); set => EditorPrefs.SetBool("vFavorites-setSelectionWithNumberKeysEnabled", value); }

        public static bool fadeAnimationsEnabled { get => EditorPrefs.GetBool("vFavorites-fadeAnimationsEnabled", true); set => EditorPrefs.SetBool("vFavorites-fadeAnimationsEnabled", value); }
        public static bool pageScrollAnimationEnabled { get => EditorPrefs.GetBool("vFavorites-pageScrollAnimationEnabled", true); set => EditorPrefs.SetBool("vFavorites-pageScrollAnimationEnabled", value); }

        public static int activeOnKeyCombination { get => EditorPrefs.GetInt("vFavorites-activeOnKeyCombination", 0); set => EditorPrefs.SetInt("vFavorites-activeOnKeyCombination", value); }
        public static bool activeOnAltEnabled { get => activeOnKeyCombination == 0; set => activeOnKeyCombination = 0; }
        public static bool activeOnAltShiftEnabled { get => activeOnKeyCombination == 1; set => activeOnKeyCombination = 1; }
        public static bool activeOnCtrlAltEnabled { get => activeOnKeyCombination == 2; set => activeOnKeyCombination = 2; }

        public static bool pluginDisabled { get => EditorPrefs.GetBool("vFavorites-pluginDisabled", false); set => EditorPrefs.SetBool("vFavorites-pluginDisabled", value); }




        const string dir = "Tools/vFavorites/";

        const string changePagesWithArrows = dir + "Change pages with Left or Right arrow keys";
        const string changeSelectionWithArrows = dir + "Change selection with Up or Down arrow keys";
        const string setSelectionWithNumberKeys = dir + "Set selection with Number keys";

        const string fadeAnimations = dir + "Fade animations";
        const string pageScrollAnimation = dir + "Page scroll animation";

        const string activeOnAlt = dir + "Holding Alt";
        const string activeOnAltShift = dir + "Holding Alt and Shift";
#if UNITY_EDITOR_OSX
        const string activeOnCtrlAlt = dir + "Holding Cmd and Alt";
#else
        const string activeOnCtrlAlt = dir + "Holding Ctrl and Alt";

#endif

        const string disablePlugin = dir + "Disable vFavorites";






        [MenuItem(dir + "Features", false, 1)] static void dadsas() { }
        [MenuItem(dir + "Features", true, 1)] static bool dadsas123() => false;

        [MenuItem(changePagesWithArrows, false, 2)] static void dadsadadsas() => changePagesWithArrowsEnabled = !changePagesWithArrowsEnabled;
        [MenuItem(changePagesWithArrows, true, 2)] static bool dadsaddasadsas() { Menu.SetChecked(changePagesWithArrows, changePagesWithArrowsEnabled); return !pluginDisabled; }

        [MenuItem(changeSelectionWithArrows, false, 3)] static void dadsadaddassas() => changeSelectionWithArrowsEnabled = !changeSelectionWithArrowsEnabled;
        [MenuItem(changeSelectionWithArrows, true, 2)] static bool dadadssaddasadsas() { Menu.SetChecked(changeSelectionWithArrows, changeSelectionWithArrowsEnabled); return !pluginDisabled; }

        [MenuItem(setSelectionWithNumberKeys, false, 4)] static void dadsadasdadsas() => setSelectionWithNumberKeysEnabled = !setSelectionWithNumberKeysEnabled;
        [MenuItem(setSelectionWithNumberKeys, true, 4)] static bool dadsadadsdasadsas() { Menu.SetChecked(setSelectionWithNumberKeys, setSelectionWithNumberKeysEnabled); return !pluginDisabled; }




        [MenuItem(dir + "Animations", false, 101)] static void dadsadsas() { }
        [MenuItem(dir + "Animations", true, 101)] static bool dadadssas123() => false;

        [MenuItem(fadeAnimations, false, 102)] static void dadsdasadadsas() => fadeAnimationsEnabled = !fadeAnimationsEnabled;
        [MenuItem(fadeAnimations, true, 102)] static bool dadsadadsadsdasadsas() { Menu.SetChecked(fadeAnimations, fadeAnimationsEnabled); return !pluginDisabled; }

        [MenuItem(pageScrollAnimation, false, 103)] static void dadsdasdasadadsas() => pageScrollAnimationEnabled = !pageScrollAnimationEnabled;
        [MenuItem(pageScrollAnimation, true, 103)] static bool dadsadaddassadsdasadsas() { Menu.SetChecked(pageScrollAnimation, pageScrollAnimationEnabled); return !pluginDisabled; }




        [MenuItem(dir + "Open when", false, 1001)] static void dadsaddssas() { }
        [MenuItem(dir + "Open when", true, 1001)] static bool dadadsssas123() => false;

        [MenuItem(activeOnAlt, false, 1002)] static void dadsdasasdadsas() => activeOnAltEnabled = !activeOnAltEnabled;
        [MenuItem(activeOnAlt, true, 1002)] static bool dadsadadssdadsdasadsas() { Menu.SetChecked(activeOnAlt, activeOnAltEnabled); return !pluginDisabled; }

        [MenuItem(activeOnAltShift, false, 1003)] static void dadsdasasdadsadsas() => activeOnAltShiftEnabled = !activeOnAltShiftEnabled;
        [MenuItem(activeOnAltShift, true, 1003)] static bool dadsadadssdasdadsdasadsas() { Menu.SetChecked(activeOnAltShift, activeOnAltShiftEnabled); return !pluginDisabled; }

        [MenuItem(activeOnCtrlAlt, false, 1004)] static void dadsdasadasadssdadsas() => activeOnCtrlAltEnabled = !activeOnCtrlAltEnabled;
        [MenuItem(activeOnCtrlAlt, true, 1004)] static bool dadsadadsadssdadsdasadsas() { Menu.SetChecked(activeOnCtrlAlt, activeOnCtrlAltEnabled); return !pluginDisabled; }




        [MenuItem(dir + "More", false, 10001)] static void daasadsddsas() { }
        [MenuItem(dir + "More", true, 10001)] static bool dadsadsdasas123() => false;

        [MenuItem(dir + "Join our Discord", false, 10002)]
        static void dadasdsas() => Application.OpenURL("https://discord.gg/4dG9KsbspG");

        [MenuItem(dir + "Check out vInspector 2", false, 10003)]
        static void dadadssadsas() => Application.OpenURL("https://assetstore.unity.com/packages/slug/252297?aid=1100lGLBn&pubref=checkoutvfav");

        // [MenuItem(dir + "Get more Editor Enhancers/Get vHierarchy 2", false, 10003)]
        // static void dadadssadsas() => Application.OpenURL("https://assetstore.unity.com/packages/slug/251320?aid=1100lGLBn&pubref=menucheckout");

        // [MenuItem(dir + "Get more Editor Enhancers/Get vFolders 2", false, 10004)]
        // static void dadadssaasddsas() => Application.OpenURL("https://assetstore.unity.com/packages/slug/263644?aid=1100lGLBn&pubref=menucheckout");

        // [MenuItem(dir + "Get more Editor Enhancers/Get vTabs 2", false, 10005)]
        // static void dadadsadssaasddsas() => Application.OpenURL("https://assetstore.unity.com/packages/slug/263645?aid=1100lGLBn&pubref=menucheckout");








        [MenuItem(disablePlugin, false, 100001)] static void dadsadsdasadasdasdsadadsas() { pluginDisabled = !pluginDisabled; UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation(); }
        [MenuItem(disablePlugin, true, 100001)] static bool dadsaddssdaasadsadadsdasadsas() { Menu.SetChecked(disablePlugin, pluginDisabled); return true; }

    }
}
#endif