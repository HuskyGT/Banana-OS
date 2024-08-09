using BepInEx;
using System.Text;

namespace BananaOS.Pages
{
    public class ModStatusConfirmationPage : WatchPage
    {
        public override string Title => "Mod Status Confirmation";

        public override bool DisplayOnMainMenu => false;

        public static BepInEx.PluginInfo plugin;

        public override void OnPageOpen()
        {
            selectionHandler.maxIndex = 1;
        }

        public override string OnGetScreenContent()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<color=yellow>==</color> Are you sure? <color=yellow>==</color>");
            stringBuilder.AppendLine($"<size=0.65>Disabling {plugin.Metadata.Name} might cause an instability");
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "No"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, "Yes"));
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

                case WatchButtonType.Enter:
                    if (selectionHandler.currentIndex == 0)
                    {
                        SwitchToPage(typeof(ModStatusPage));
                        break;
                    }
                    Config.ToggleModStatus(plugin);
                    SwitchToPage(typeof(ModStatusPage));
                    break;

                case WatchButtonType.Back:
                    SwitchToPage(typeof(ModStatusPage));
                    break;
            }
            if (MonkeWatch.Instance.displayingPage == this)
                return;

            selectionHandler.currentIndex = 0;
        }
    }
}
