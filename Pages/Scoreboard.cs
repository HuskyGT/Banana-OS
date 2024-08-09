using Photon.Pun;
using System.Linq;
using System.Text;

namespace BananaOS.Pages
{
    public class Scoreboard : WatchPage
    {
        public override string Title => "Scoreboard";

        public override bool DisplayOnMainMenu => true;

        public override void OnPageOpen()
        {
            UpdateScoreboard();
        }
        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<color=yellow>==</color> Scoreboard <color=yellow>==</color>");
            if (!PhotonNetwork.InRoom)
            {
                stringBuilder.AppendLine("<size=0.55>     You are not in a room!\n     Please enter a room for\n     the scoreboard to work\n     properly");
                return stringBuilder.ToString();
            }
            stringBuilder.StartSize(0.65f);
            stringBuilder.AppendLine($"Room ID: - {(PhotonNetwork.CurrentRoom.IsVisible ? PhotonNetwork.CurrentRoom.Name : "Private")} -");
            stringBuilder.EndSize();
            stringBuilder.AppendLines(1);

            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if (!VRRigCache.Instance.TryGetVrrig(PhotonNetwork.PlayerList[i], out var rigContainer))
                    continue;

                bool tagged = false;
                if (GorillaGameManager.instance?.GetType() == typeof(GorillaTagManager))
                {
                    tagged = (GorillaGameManager.instance as GorillaTagManager).currentInfected.Contains(PhotonNetwork.PlayerList[i]);
                }

                var pName = PhotonNetwork.PlayerList[i].NickName.LimitLength(12);
                pName = tagged ? pName.WrapColor("c45e79") : pName;
                var pText = $"{pName} {"#".WrapColor(rigContainer.vrrig.playerColor)}";
                stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(i, pText));
            }
            return stringBuilder.ToString();
        }

        void UpdateScoreboard()
        {
            if (MonkeWatch.Instance.displayingPage != this)
                return;

            selectionHandler.maxIndex = PhotonNetwork.PlayerList.Length - 1;
            MonkeWatch.Instance.UpdateScreen();
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
                    if (!PhotonNetwork.InRoom)
                        return;

                    if (PhotonNetwork.PlayerList[selectionHandler.currentIndex].IsLocal)
                        return;

                    ScoreboardPlayerMenu.viewingPlayer = PhotonNetwork.PlayerList[selectionHandler.currentIndex];
                    if (!VRRigCache.Instance.TryGetVrrig(ScoreboardPlayerMenu.viewingPlayer, out var rigContainer))
                        return;

                    ScoreboardPlayerMenu.rigContainer = rigContainer;
                    ScoreboardPlayerMenu.scoreboardLine = GorillaScoreboardTotalUpdater.allScoreboardLines.FirstOrDefault(line => line.linePlayer.UserId == ScoreboardPlayerMenu.viewingPlayer.UserId);
                    if (ScoreboardPlayerMenu.scoreboardLine == null)
                    {
                        return;
                    }
                    SwitchToPage(typeof(ScoreboardPlayerMenu));
                    break;

                case WatchButtonType.Back:
                    selectionHandler.currentIndex = 0;
                    ReturnToMainMenu();
                    break;
            }
        }
        public override void OnRoomStateUpdated() => UpdateScoreboard();
    }
}
