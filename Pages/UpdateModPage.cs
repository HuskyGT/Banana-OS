using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BananaOS.Pages
{
    public class UpdateModPage : WatchPage
    {
        public override string Title => "UPDATE MOD";
        public override bool DisplayOnMainMenu => false;

        public static bool IsNewestVersion = true;
        static string newestVersion;

        async Task GetNewestVersion()
        {
            using HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/HuskyGT/Banana-OS/main/Newest%20Version");
            newestVersion = await response.Content.ReadAsStringAsync();
        }
        public override async void OnPostModSetup()
        {
#if !DEBUG
            await GetNewestVersion();
            if (!newestVersion.Contains(PluginInfo.Version))
            {
                IsNewestVersion = false;
            }
#endif
        }
        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<color=yellow>==</color> Update <color=yellow>==</color>");
            stringBuilder.StartSize(0.50f);
            stringBuilder.AppendLine("There Is A New Version Of The Mod Available");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Your Version: " + PluginInfo.Version);
            stringBuilder.AppendLine("New Version: " + newestVersion);
            stringBuilder.AppendLine("Press Any Key To Return To The Main Menu");
            stringBuilder.EndSize();
            return stringBuilder.ToString();
        }
        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            ReturnToMainMenu();
        }
    }
}