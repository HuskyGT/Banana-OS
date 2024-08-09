using System;
using System.Linq;
using UnityEngine;
using static BananaOS.MonkeWatch;

namespace BananaOS.Pages
{
    public abstract class WatchPage : MonoBehaviour
    {
        //https://www.youtube.com/watch?v=zICqmzxeT8Q watch reference NO DELETE PLEASE TOOK LONG TO FIND
        
        public SelectionHandler selectionHandler = new SelectionHandler();
        public abstract string Title { get; }
        public abstract bool DisplayOnMainMenu { get; }
        public abstract string OnGetScreenContent();

        public void ReturnToMainMenu() => SwitchToPage(typeof(MainMenu));
        public void SwitchToPage(WatchPage screen) => MonkeWatch.Instance.SwitchToPage(screen);
        public void SwitchToPage(Type screenType) => MonkeWatch.Instance.SwitchToPage(screenType);

        public virtual void OnButtonPressed(WatchButtonType buttonType) { }
        public virtual void OnButtonReleased(WatchButtonType buttonType) { }
        public virtual void OnPostModSetup() { }
        public virtual void OnPageOpen() { }

        ///<summary>This will trigger when you join/leave a room or when a player joins/leaves the room</summary>
        public virtual void OnRoomStateUpdated() { }
    }
}
