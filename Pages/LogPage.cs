#if DEBUG

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

namespace BananaOS.Pages
{
    public class LogPage : WatchPage
    {
        public override string Title => "Log";

        public override bool DisplayOnMainMenu => false;

        public List<string> logList = new List<string>();

        public override void OnPostModSetup()
        {
            selectionHandler.maxIndex = 2;
        }
        private void OnLogReceived(string message, LogType type = LogType.Error)
        {
            Debug.Log($"{message} {type}");
            switch(type)
            {
                case LogType.Log:
                    break;

                case LogType.Warning:
                    break;

                default:
                    break;
            }
        }

        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<color=yellow>==</color> Settings <color=yellow>==</color>");
            stringBuilder.AppendLines(1);
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "Mod Status"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, "Watch Skins"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(2, "Background Skins"));
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
                            SwitchToPage(typeof(ModStatusPage));
                            break;

                        case 1:
                            SwitchToPage(typeof(WatchSkinPage));
                            break;

                        case 2:
                            SwitchToPage(typeof(BackgroundSkinPage));
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