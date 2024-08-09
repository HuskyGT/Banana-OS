using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using UnityEngine;

namespace BananaOS.Networking
{
    /*[Serializable]
    internal class NetworkedProps
    {
        internal string watchSkin;
        //internal string type;
        //internal bool panelOpen;
    }*/

    internal class NetworkManager : MonoBehaviourPunCallbacks
    {
        /*public static Vector3 defaultWatchPosition, defaultWatchScale;
        public static Quaternion defaultWatchRotation;

        public static Dictionary<VRRig, Renderer> vrrigWatchDict = new Dictionary<VRRig, Renderer>();
        public static GameObject watchPrefab;

        public static void CreateWatch(VRRig rig, bool IsLocal)
        {
            if (vrrigWatchDict.ContainsKey(rig))
                return;

            var watch = Instantiate(watchPrefab).transform;
            watch.SetParent(rig.leftHandTransform.parent, false);
            if (IsLocal)
            {
                watch.AddComponent<WatchButton>().buttonType = WatchButtonType.Watch;
            }
            watch.localPosition = defaultWatchPosition;
            watch.localRotation = defaultWatchRotation;
            watch.localScale = defaultWatchScale;
            vrrigWatchDict.Add(rig, watch.GetComponent<Renderer>());
        }*/
        public override void OnJoinedRoom()
        {
            try
            {
                UpdateProps();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        internal static void UpdateProps()
        {
            var hashtable = new Hashtable();
            //hashtable.Add(PluginInfo.Name, Config.watchSkin.Value.LimitLength(25));
            hashtable.Add(PluginInfo.Name, PluginInfo.Version);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        }

        /*public override async void OnPlayerPropertiesUpdate(Player player, Hashtable changedProps)
        {
            try
            {
                if (player == PhotonNetwork.LocalPlayer)
                    return;

                if (changedProps.TryGetValue(PluginInfo.Name, out var watchSkin))
                {
                    if (VRRigCache.Instance.TryGetVrrig(player, out var rigContainer))
                    {
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }*/
    }
}
