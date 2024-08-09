using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace BananaOS.Pages
{
    public class MainMenu : WatchPage
    {
        public override string Title => "MainMenu";

        public override bool DisplayOnMainMenu => false;

        static SelectionHandler pageSelection = new SelectionHandler();
        public static Dictionary<int, List<WatchPage>> screenPageDict = new Dictionary<int, List<WatchPage>>();
        public readonly WatchButtonType[] secretCode = new WatchButtonType[] { WatchButtonType.Up, WatchButtonType.Up, WatchButtonType.Down, WatchButtonType.Down, WatchButtonType.Left, WatchButtonType.Right, WatchButtonType.Left, WatchButtonType.Right, WatchButtonType.Back, WatchButtonType.Enter };
        List<WatchButtonType> lastPressedButtons = new List<WatchButtonType>();
        const int maxPageItemCount = 8;
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

        public override void OnPageOpen()
        {
            lastPressedButtons.Clear();
        }
        public override void OnPostModSetup()
        {
            int pageIndex = 0;
            int indexOffset = 0;
            for (int i = 0; i < MonkeWatch.Instance.watchPages.Count; i++)
            {
                if (!MonkeWatch.Instance.watchPages[i].DisplayOnMainMenu)
                {
                    indexOffset++;
                    continue;
                }

                if ((i - indexOffset) % maxPageItemCount == 0)
                {
                    pageIndex++;
                    screenPageDict.Add(pageIndex, new List<WatchPage>());
                }
                screenPageDict[pageIndex].Add(MonkeWatch.Instance.watchPages[i]);
            }
            pageSelection.maxIndex = screenPageDict.Count - 1;
            selectionHandler.maxIndex = screenPageDict[currentPage].Count - 1;
            
        }
        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            var screensPage = screenPageDict[currentPage];

            stringBuilder.StartAlign("center");
            stringBuilder.AppendLineColor("========================", Color.yellow);
            stringBuilder.AppendLineColor("MonkeWatch", Color.green);
            stringBuilder.AppendLine("Original by: <color=red>RedBrumbler</color>");
            stringBuilder.AppendLine("Recreation by: <color=yellow>Husky</color>");
            stringBuilder.AppendLineColor("========================", Color.yellow);
            stringBuilder.EndAlign();

            for (int i = 0; i < screensPage.Count; i++)
            {
                stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(i, $"{screensPage[i].Title}"));
            }
            stringBuilder.AppendLine();
            if (screenPageDict.Count > 1)
            {
                stringBuilder.AppendLine($"{currentPage}/{screenPageDict.Count}");
            }
            return stringBuilder.ToString();
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            lastPressedButtons.Add(buttonType);
            if (lastPressedButtons.Count == 10)
            {
                bool enteredSecretCode = true;
                for (int i = 0; i < lastPressedButtons.Count; i++)
                {
                    if (lastPressedButtons[i] != secretCode[i])
                    {
                        enteredSecretCode = false;
                    }
                }
                if (enteredSecretCode)
                {
                    SwitchToPage(typeof(SnakePage));
                    return;
                }
            }
            switch(buttonType)
            {
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;
                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;
                case WatchButtonType.Left:
                    if (pageSelection.currentIndex == pageSelection.MoveSelectionUp())
                        return;

                    currentPage = pageSelection.currentIndex;
                    selectionHandler.maxIndex = screenPageDict[currentPage].Count - 1;
                    selectionHandler.currentIndex = 0;
                    break;
                case WatchButtonType.Right:
                    if (pageSelection.currentIndex == pageSelection.MoveSelectionDown())
                        return;

                    currentPage = pageSelection.currentIndex;
                    selectionHandler.maxIndex = screenPageDict[currentPage].Count - 1;
                    selectionHandler.currentIndex = 0;
                    break;
                case WatchButtonType.Enter:
                    var screen = screenPageDict[currentPage][selectionHandler.currentIndex];
                    //Makes it easier to replace pages with patches like is done with the gorilla friends integration
                    MonkeWatch.Instance.SwitchToPage(screen.GetType());
                    break;
            }
        }
    }
}
