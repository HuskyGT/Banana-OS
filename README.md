# Banana-OS
# Sneak peaks: https://discord.gg/mDxtRwr3je

## For Developers
These docs are terrible but I will fix them up

### Setting Up A Watch Page
To setup a watch page you first have to create a new class that inherits `WatchPage` bellow is an example of a page using Banana OS which has two buttons that you can click
```cs
using System.Text;
using UnityEngine;

namespace BananaOS.Pages
{
    public class ExamplePage : WatchPage
    {
        //What will be shown on the main menu if DisplayOnMainMenu is set to true
        public override string Title => "Example";

        //Enabling will display your page on the main menu if you're nesting pages you should set this to false
        public override bool DisplayOnMainMenu => true;

        //This method will be ran after the watch is completely setup
        public override void OnPostModSetup()
        {
            //max selection index so the indicator stays on the screen
            selectionHandler.maxIndex = 1;
        }

        //What you return is what is drawn to the watch screen the screen will be updated everytime you press a button
        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<color=yellow>==</color> Example <color=yellow>==</color>");
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "Test Button"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, "Test Button 2"));
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
                        Debug.Log("Test Button Press");
                        return;
                    }
                    Debug.Log("Test Button Press 2");
                    break;

                //It is recommended that you keep this unless you're nesting pages if so you should use the SwitchToPage method
                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
    }
}
```
