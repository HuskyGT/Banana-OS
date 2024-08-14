# Banana-OS
## Discord Server: 
### https://discord.gg/mDxtRwr3je


## For Developers
### Displaying Notifications
There are multiple ways you can choose to display notifications using the `DisplayNotification` method here is an example of a basic implementation of `DisplayNotification`
```cs
BananaNotifications.DisplayNotification("This is a basic example of displaying a notification");
```
this notifcation will have the default parameters which is a yellow background color, white text color, and the duration of 3 or the implementation of this
<br>
<br>
Here is an example of what the example above looks like in full form
```cs
BananaNotifications.DisplayNotification("This is a basic example of displaying a notification", Color.yellow, Color.white, 3);
```
This is an example of an implementation that changes these parameters so the background color would be red, the text color would be green, the notification duration would be 6, and the text that would be displayed would be "This is a example of displaying a notification"
```cs
BananaNotifications.DisplayNotification("This is a example of displaying a notification", Color.red, Color.green, 6);
```
There is also two other methods that are for basic warning and error messages without changing the paramters `DisplayErrorNotification()` and `DisplayWarningNotification()`

<br><br>

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

### This product is not affiliated with Gorilla Tag or Another Axiom LLC and is not endorsed or otherwise sponsored by Another Axiom LLC. Portions of the materials contained herein are property of Another Axiom LLC. ©2021 Another Axiom LLC.
