using BepInEx;
using System;
using System.Text;

namespace BananaOS
{
    public class SelectionHandler
    {
        public int maxIndex = 0;
        public int currentIndex;
        public string selectionText = " <color=#C16F66>></color> ";

        public string GetSelectedText(int index, string text)
        {
            return currentIndex == index ? selectionText + text : text;
        }
        public string GetOriginalBananaOSSelectionText(int index, string text)
        {
            return currentIndex == index ? $" <color=#C16F66>></color> {text}" : $"   {text}";
        }
        public bool IsSelectionIndex(int index)
        {
            return currentIndex == index;
        }

        public int CheckSelection()
        {
            if (currentIndex < 0)
            {
                currentIndex = Config.selectorWrapping.Value ? maxIndex : 0;
            }
            if (currentIndex > maxIndex)
            {
                currentIndex = Config.selectorWrapping.Value ? 0 : maxIndex;
            }
            return currentIndex;
        }
        public int MoveSelectionUp()
        {
            currentIndex--;
            CheckSelection();
            return currentIndex;
        }
        public int MoveSelectionDown()
        {
            currentIndex++;
            CheckSelection();
            return currentIndex;
        }
    }
}
