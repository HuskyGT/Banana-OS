using BepInEx;
using BananaOS.Patches;
using UnityEngine;
using System.Reflection;
using System;
using BananaOS.Pages;
using System.Linq;
using UnityEngine.InputSystem;

namespace BananaOS
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        void Awake()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        internal static void OnGameInitialized()
        {

            /*var watchPageTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly =>
{
    try
    {
        return assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(WatchPage)));
    }
    catch (ReflectionTypeLoadException ex)
    {
        return ex.Types.Where(t => t != null && t.IsSubclassOf(typeof(WatchPage)));
    }
    catch (Exception e)
    {
        Debug.Log(e);
        return null;
    }
}).ForEach(MonkeWatch.RegisterPage);*/
            MonkeWatch.RegisterPage(typeof(Scoreboard));
            MonkeWatch.RegisterPage(typeof(Details));
            MonkeWatch.RegisterPage(typeof(Disconnect));
            MonkeWatch.RegisterPage(typeof(SettingsPage));
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var types = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(WatchPage)));
                    foreach (var type in types)
                    {
                        MonkeWatch.RegisterPage(type);
                    }
                }
                catch(Exception e)
                {
                    Debug.LogError($"There was an error while retreiving Page types from {assembly.FullName}: " + e);
                }
            }

            var str = Assembly.GetExecutingAssembly().GetManifestResourceStream("BananaOS.Resources.bananaosassets");
            var bundle = AssetBundle.LoadFromStream(str);
            var bananaOSAssets = Instantiate(bundle.LoadAsset<GameObject>("BananaOS"));
            MonkeWatch.watchPrefab = Instantiate(bananaOSAssets.transform.GetChild(1).gameObject);
            MonkeWatch.watchPrefab.SetActive(false);
            var watch = bananaOSAssets.transform.GetChild(1).GetChild(0).transform;
            watch.AddComponent<WatchButton>().buttonType = WatchButtonType.Watch;
            MonkeWatch.watch = watch.gameObject;
            var pos = watch.localPosition;
            var rot = watch.localRotation;
            var scale = watch.localScale;
            watch.SetParent(GorillaTagger.Instance.offlineVRRig.leftHandTransform.parent, false);
            watch.localPosition = pos;
            watch.localRotation = rot;
            watch.localScale = scale;
            bananaOSAssets.transform.GetChild(0).AddComponent<MonkeWatch>();
            bundle.Unload(false);

            
#if DEBUG
            //Place and rotate so the camera can see without hopping in vr for testing
            //bananaOSAssets.transform.rotation = Quaternion.Euler(0, 180, 0);
            //bananaOSAssets.transform.position = new Vector3(-67.845f, 12.6146f, -83.0447f);
#endif
        }

#if DEBUG
        bool guiOpen;
        void OnGUI()
        {
            if (!guiOpen)
                return;

            GUILayout.Label(MonkeWatch.Instance.screenText.text);
        }
        void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                MonkeWatch.Instance.PressButton(WatchButtonType.Watch);
                guiOpen = MonkeWatch.Instance.watchActive;
            }

            if (!guiOpen)
                return;

            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                MonkeWatch.Instance.PressButton(WatchButtonType.Enter);
            }
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                MonkeWatch.Instance.PressButton(WatchButtonType.Up);
            }
            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                MonkeWatch.Instance.PressButton(WatchButtonType.Down);
            }
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                MonkeWatch.Instance.PressButton(WatchButtonType.Right);
            }
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                MonkeWatch.Instance.PressButton(WatchButtonType.Left);
            }
            if (Keyboard.current.backspaceKey.wasPressedThisFrame)
            {
                MonkeWatch.Instance.PressButton(WatchButtonType.Back);
            }
        }
#endif
    }
}
