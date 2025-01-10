#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using System.Reflection;
using System.Linq;
using UnityEngine.UIElements;
using static VTabs.Libs.VUtils;



namespace VTabs
{
    public static class VTabsMenu
    {

        public static bool dragndropEnabled { get => EditorPrefs.GetBool("vTabs-dragndropEnabled", true); set => EditorPrefs.SetBool("vTabs-dragndropEnabled", value); }
        public static bool switchTabsEnabled { get => EditorPrefs.GetBool("vTabs-switchTabsEnabled", true); set => EditorPrefs.SetBool("vTabs-switchTabsEnabled", value); }
        public static bool moveTabsEnabled { get => EditorPrefs.GetBool("vTabs-moveTabsEnabled", true); set => EditorPrefs.SetBool("vTabs-moveTabsEnabled", value); }

        public static bool addTabEnabled { get => EditorPrefs.GetBool("vTabs-addTabEnabled", true); set => EditorPrefs.SetBool("vTabs-addTabEnabled", value); }
        public static bool closeTabEnabled { get => EditorPrefs.GetBool("vTabs-closeTabEnabled", true); set => EditorPrefs.SetBool("vTabs-closeTabEnabled", value); }
        public static bool reopenTabEnabled { get => EditorPrefs.GetBool("vTabs-reopenTabEnabled", true); set => EditorPrefs.SetBool("vTabs-reopenTabEnabled", value); }

        public static bool sidescrollEnabled { get => EditorPrefs.GetBool("vTabs-sidescrollEnabled", Application.platform == RuntimePlatform.OSXEditor); set => EditorPrefs.SetBool("vTabs-sidescrollEnabled", value); }
        public static bool reverseScrollDirectionEnabled { get => EditorPrefs.GetBool("vTabs-reverseScrollDirectionDirection", false); set => EditorPrefs.SetBool("vTabs-reverseScrollDirectionDirection", value); }
        public static float sidescrollSensitivity { get => EditorPrefs.GetFloat("vTabs-sidescrollSensitivity", 1); set => EditorPrefs.SetFloat("vTabs-sidescrollSensitivity", value); }

        public static bool pluginDisabled { get => EditorPrefs.GetBool("vTabs-pluginDisabled", false); set => EditorPrefs.SetBool("vTabs-pluginDisabled", value); }




        const string dir = "Tools/vTabs/";
#if UNITY_EDITOR_OSX
        const string cmd = "Cmd";
#else
        const string cmd = "Ctrl";
#endif

        const string dragndrop = dir + "Create tabs with Drag-and-Drop";
        const string switchTabs = dir + "Switch tabs with Shift-Scroll";
        const string moveTabs = dir + "Move tabs with " + cmd + "-Shift-Scroll";

        const string addTab = dir + cmd + "-T to add tab";
        const string closeTab = dir + cmd + "-W to close tab";
        const string reopenTab = dir + cmd + "-Shift-T to reopen closed tab";

        const string sidescroll = dir + "Horizontal Scroll as Shift-Scroll";
        const string reverseScrollDirection = dir + "Reverse direction";
        const string increaseSensitivity = dir + "Increase sensitivity";
        const string decreaseSensitivity = dir + "Decrease sensitivity";

        const string disablePlugin = dir + "Disable vTabs";







        [MenuItem(dir + "Features", false, 1)] static void dadsas() { }
        [MenuItem(dir + "Features", true, 1)] static bool dadsas123() => false;

        [MenuItem(dragndrop, false, 2)] static void dadsadsadasdsadadsas() => dragndropEnabled = !dragndropEnabled;
        [MenuItem(dragndrop, true, 2)] static bool dadsaddsasadadsdasadsas() { Menu.SetChecked(dragndrop, dragndropEnabled); return !pluginDisabled; }

        [MenuItem(switchTabs, false, 3)] static void dadsadsadsadsadasdsadadsas() => switchTabsEnabled = !switchTabsEnabled;
        [MenuItem(switchTabs, true, 3)] static bool dadsadasdasddsasadadsdasadsas() { Menu.SetChecked(switchTabs, switchTabsEnabled); return !pluginDisabled; }

        [MenuItem(moveTabs, false, 4)] static void dadsadsadsadasdsadadsas() => moveTabsEnabled = !moveTabsEnabled;
        [MenuItem(moveTabs, true, 4)] static bool dadsadasddsasadadsdasadsas() { Menu.SetChecked(moveTabs, moveTabsEnabled); return !pluginDisabled; }




        [MenuItem(dir + "Shortcuts", false, 101)] static void daaadsas() { }
        [MenuItem(dir + "Shortcuts", true, 101)] static bool daadsdsas123() => false;

        [MenuItem(addTab, false, 102)] static void dadsadadsas() => addTabEnabled = !addTabEnabled;
        [MenuItem(addTab, true, 102)] static bool dadsaddasadsas() { Menu.SetChecked(addTab, addTabEnabled); return !pluginDisabled; }

        [MenuItem(closeTab, false, 103)] static void dadsadasdadsas() => closeTabEnabled = !closeTabEnabled;
        [MenuItem(closeTab, true, 103)] static bool dadsadsaddasadsas() { Menu.SetChecked(closeTab, closeTabEnabled); return !pluginDisabled; }

        [MenuItem(reopenTab, false, 104)] static void dadsadsadasdadsas() => reopenTabEnabled = !reopenTabEnabled;
        [MenuItem(reopenTab, true, 104)] static bool dadsaddsasaddasadsas() { Menu.SetChecked(reopenTab, reopenTabEnabled); return !pluginDisabled; }




        [MenuItem(dir + "Scrolling", false, 1001)] static void daadsdsadsas() { }
        [MenuItem(dir + "Scrolling", true, 1001)] static bool dadsasasdads() => false;

        [MenuItem(sidescroll, false, 1002)] static void dadsadsadsadsadasdadssadadsas() => sidescrollEnabled = !sidescrollEnabled;
        [MenuItem(sidescroll, true, 1002)] static bool dadsadasdasddsadassadadsdasadsas() { Menu.SetChecked(sidescroll, sidescrollEnabled); return !pluginDisabled; }

        [MenuItem(reverseScrollDirection, false, 1003)] static void dadsadadssadsadsadasdadssadadsas() => reverseScrollDirectionEnabled = !reverseScrollDirectionEnabled;
        [MenuItem(reverseScrollDirection, true, 1003)] static bool dadsadasdadsasddsadassadadsdasadsas() { Menu.SetChecked(reverseScrollDirection, reverseScrollDirectionEnabled); return !pluginDisabled; }

        [MenuItem(increaseSensitivity, false, 1004)] static void qdadadsssa() { sidescrollSensitivity += .2f; Debug.Log("vTabs: scrolling sensitivity increased to " + sidescrollSensitivity * 100 + "%"); }
        [MenuItem(increaseSensitivity, true, 1004)] static bool qdaddasadsssa() => !pluginDisabled;

        [MenuItem(decreaseSensitivity, false, 1005)] static void qdasadsssa() { sidescrollSensitivity -= .2f; Debug.Log("vTabs: scrolling sensitivity decreased to " + sidescrollSensitivity * 100 + "%"); }
        [MenuItem(decreaseSensitivity, true, 1005)] static bool qdaddasdsaadsssa() => !pluginDisabled;




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

        // [MenuItem(dir + "Get more Editor Enhancers/Get vFavorites 2", false, 10005)]
        // static void dadadsadssaasddsas() => Application.OpenURL("https://assetstore.unity.com/packages/slug/263643?aid=1100lGLBn&pubref=menucheckout");





        [MenuItem(disablePlugin, false, 100001)] static void dadsadsdasadasdasdsadadsas() { pluginDisabled = !pluginDisabled; UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation(); }
        [MenuItem(disablePlugin, true, 100001)] static bool dadsaddssdaasadsadadsdasadsas() { Menu.SetChecked(disablePlugin, pluginDisabled); return true; }


    }
}
#endif