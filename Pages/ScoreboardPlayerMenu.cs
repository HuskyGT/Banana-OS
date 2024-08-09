using GorillaNetworking;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BananaOS.Pages
{
    public class ScoreboardPlayerMenu : WatchPage
    {
        public override string Title => "ScoreboardPlayerMenu";

        public override bool DisplayOnMainMenu => false;

        public static Player viewingPlayer;
        public static RigContainer rigContainer;
        public static GorillaPlayerScoreboardLine scoreboardLine;
        bool muted;

        public override void OnPostModSetup()
        {
            selectionHandler.maxIndex = 1;
        }
        public override void OnPageOpen()
        {
            scoreboardLine = GorillaScoreboardTotalUpdater.allScoreboardLines.FirstOrDefault(line => line.linePlayer.UserId == viewingPlayer?.UserId);
            if (scoreboardLine == null && PhotonNetwork.InRoom)
            {
                SwitchToPage(typeof(Scoreboard));
            }
        }
        public override string OnGetScreenContent()
        {
            muted = scoreboardLine.muteButton.isOn;
            var c = rigContainer.vrrig.playerColor;

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<color=yellow>==</color> Player <color=yellow>==</color>");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Player: " + viewingPlayer.NickName.LimitLength(12));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, muted ? "Muted" : "Mute"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, "Report"));
            stringBuilder.AppendLines(2);
            stringBuilder.AppendLine("Color (0 - 9):");
            stringBuilder.AppendLine($"{Mathf.RoundToInt(c.r * 9)} {Mathf.RoundToInt(c.g * 9)} {Mathf.RoundToInt(c.b * 9)}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Color (0 - 255):");
            stringBuilder.AppendLine($"{Mathf.RoundToInt(c.r * 255)} {Mathf.RoundToInt(c.g * 255)} {Mathf.RoundToInt(c.b * 255)}");
            return stringBuilder.ToString();

            /*var content = "<color=yellow>==</color> Player <color=yellow>==</color>\n\n";
            content += $"Player: {viewingPlayer?.NickName.LimitLength(12)}\n\n";
            content += $"{selectionHandler.GetOriginalBananaOSSelectionText(0, muted ? "Muted" : "Mute")}\n";
            content += $"{selectionHandler.GetOriginalBananaOSSelectionText(1, "Report")}\n\n\n";
            content += "Color (0 - 9):\n";
            var r = rigContainer.vrrig.playerColor.r;
            var g = rigContainer.vrrig.playerColor.g;
            var b = rigContainer.vrrig.playerColor.b;
            content += $"{Mathf.RoundToInt(r * 9)} {Mathf.RoundToInt(g * 9)} {Mathf.RoundToInt(b * 9)}\n\n";
            content += "Color (0 - 255)\n";
            content += $"{Mathf.RoundToInt(r * 255)} {Mathf.RoundToInt(g * 255)} {Mathf.RoundToInt(b * 255)}\n\n";
            return content*/
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            if (ScoreboardPlayerMenu.viewingPlayer == null)
            {
                SwitchToPage(typeof(Scoreboard));
                return;
            }
            switch (buttonType)
            {
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;
                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;
                case WatchButtonType.Enter:
                    /*if (selectionHandler.currentIndex == 0)
                    {
                        muted = !muted;
                        scoreboardLine.muteButton.isOn = muted;
                        scoreboardLine.PressButton(muted, GorillaPlayerLineButton.ButtonType.Mute);
                        scoreboardLine.parentScoreboard.RedrawPlayerLines();
                        return;
                    }
                    scoreboardLine.canPressNextReportButton = true;
                    scoreboardLine.PressButton(muted, GorillaPlayerLineButton.ButtonType.Report);
                    SwitchToScreen(typeof(ScoreboardReportMenu));
                    scoreboardLine.parentScoreboard.RedrawPlayerLines();*/

                    if (selectionHandler.currentIndex == 0)
                    {
                        muted = !muted;
                        scoreboardLine.muteButton.isOn = muted;
                        scoreboardLine.PressButton(muted, GorillaPlayerLineButton.ButtonType.Mute);
                        return;
                    }
                    SwitchToPage(typeof(ScoreboardReportMenu));
                    break;
                case WatchButtonType.Back:
                    SwitchToPage(typeof(Scoreboard));
                    break;
            }
        }
    }
}
