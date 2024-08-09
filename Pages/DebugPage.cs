#if DEBUG

using System.Text;
using UnityEngine;

namespace BananaOS.Pages
{
    public class DebugPage : WatchPage
    {
        public override string Title => "Debug";

        public override bool DisplayOnMainMenu => true;

        public override void OnPostModSetup()
        {
            selectionHandler.maxIndex = 0;
        }

        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<color=yellow>==</color> Settings <color=yellow>==</color>");
            stringBuilder.AppendLines(1);
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "Log"));
            //stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(3, "Watch Skins"));
            //stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(4, "Watch Skins"));

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
                    switch (selectionHandler.currentIndex)
                    {
                        case 0:
                            break;

                        case 1:
                            break;

                        case 2:
                            break;

                        case 3:
                            break;

                        case 4:
                            break;
                    }
                    break;

                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
    }
}

#endif