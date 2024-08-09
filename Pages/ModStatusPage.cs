using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BananaOS.Pages
{
    public class ModStatusPage : WatchPage
    {
        public override string Title => "Mod Status";

        public override bool DisplayOnMainMenu => false;
        SelectionHandler pageSelection = new SelectionHandler();
        public static Dictionary<int, List<BepInEx.PluginInfo>> modstatusPageDict = new Dictionary<int, List<BepInEx.PluginInfo>>();
        const int maxPageItemCount = 12;
        private static int CurrentPage;
        public static int currentPage
        {
            get
            {
                return CurrentPage + 1;
            }
            set
            {
                CurrentPage = value;
            }
        }
        public override void OnPostModSetup()
        {
            var plugins = new List<BepInEx.PluginInfo>();
            foreach (var plugin in BepInEx.Bootstrap.Chainloader.PluginInfos.Values)
            {
                /*var methods = new MethodInfo[]
                {
                    AccessTools.Method(plugin.GetType(), "OnEnable"),
                    AccessTools.Method(plugin.GetType(), "OnDisable")
                };
                if (methods[0] == null || methods[1] == null)
                {
                }*/
                //Debug.Log(plugin.Metadata.Name);
                plugins.Add(plugin);
                plugin.Instance.enabled = Config.GetModActiveConfigStatus(plugin);
            }
            int pageIndex = 0;
            for (int i = 0; i < plugins.Count; i++)
            {
                if (i % maxPageItemCount == 0)
                {
                    pageIndex++;
                    modstatusPageDict.Add(pageIndex, new List<BepInEx.PluginInfo>());
                }
                modstatusPageDict[pageIndex].Add(plugins[i]);
            }
            selectionHandler.maxIndex = maxPageItemCount - 1;
            pageSelection.maxIndex = modstatusPageDict.Count - 1;
        }

        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            var modStatusPage = modstatusPageDict[currentPage];
            stringBuilder.AppendLine("<color=yellow>==</color> Mod Status <color=yellow>==</color><size=0.65>");
            stringBuilder.AppendLines(1);
            for (int i = 0; i < modStatusPage.Count; i++)
            {
                stringBuilder.AppendLineColor(selectionHandler.GetOriginalBananaOSSelectionText(i, modStatusPage[i].Metadata.Name), modStatusPage[i].Instance.enabled ? Color.white : Color.gray);
            }
            stringBuilder.AppendLines(1);
            stringBuilder.AppendLineColor($"{currentPage}/{modstatusPageDict.Count}", Color.white);
            return stringBuilder.ToString();
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;

                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;

                case WatchButtonType.Left:
                    pageSelection.MoveSelectionUp();
                    currentPage = pageSelection.currentIndex;
                    selectionHandler.maxIndex = modstatusPageDict[currentPage].Count - 1;
                    selectionHandler.currentIndex = 0;
                    break;

                case WatchButtonType.Right:
                    pageSelection.MoveSelectionDown();
                    currentPage = pageSelection.currentIndex;
                    selectionHandler.maxIndex = modstatusPageDict[currentPage].Count - 1;
                    selectionHandler.currentIndex = 0;
                    break;

                case WatchButtonType.Enter:
                    var plugin = modstatusPageDict[currentPage][selectionHandler.currentIndex];
                    if (!plugin.Instance.enabled)
                    {
                        Config.ToggleModStatus(plugin);
                        return;
                    }
                    ModStatusConfirmationPage.plugin = plugin;
                    SwitchToPage(typeof(ModStatusConfirmationPage));
                    break;
                case WatchButtonType.Back:
                    SwitchToPage(typeof(SettingsPage));
                    break;
            }
        }
    }
}
