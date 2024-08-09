using System;
using UnityEngine;
using TMPro;
using BananaOS.Pages;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.Collections;
using HarmonyLib;
using BananaOS.Networking;
using System.Reflection;
using UnityEngine.UIElements;

namespace BananaOS
{
    public class MonkeWatch : MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// This should be called before the game is initalized it is highly recommended to use this due to a bug that is causing pages to not be automatically registered
        /// </summary>
        /// <param name="pageType"></param>
        public static void RegisterPage(Type type)
        {
            if (pageTypes.Contains(type))
                return;

            pageTypes.Add(type);
            Debug.Log($"Registered {type.FullName}");
        }
        public static GameObject watchPrefab;

        public static List<Type> pageTypes = new List<Type>();
        public static MonkeWatch Instance;
        public TMP_Text screenText;
        public GameObject bananaIcon;
        public static GameObject watch;
        public GameObject background;
        public GameObject watchScreen;
        public Transform handAngleCheckTransform;
        public bool watchActive = false;

        public List<WatchPage> watchPages = new List<WatchPage>();
        public WatchPage displayingPage;
        Vector3 originalScale;

        void Awake()
        {
            Instance = this;
            Config.RefreshSettings();
            watchScreen = gameObject;
            background = watchScreen.transform.Find("Panel").gameObject;
            bananaIcon = background.transform.Find("BananaOS Icon").gameObject;
            var buttonsParent = background.transform.Find("Canvas/Buttons");
            screenText = background.transform.Find("Canvas/Text").GetComponent<TMP_Text>();
            for (int i = 0; i < buttonsParent.childCount; i++)
            {
                var obj = buttonsParent.GetChild(i);
                Enum.TryParse(typeof(WatchButtonType), obj.name, out var type);
                var button = buttonsParent.GetChild(i).AddComponent<WatchButton>();
                button.buttonType = (WatchButtonType)type;
                button.Init();
            }
            handAngleCheckTransform = new GameObject("handAngleCheckTransform").transform;
            handAngleCheckTransform.transform.parent = watch.transform.parent;
            handAngleCheckTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 90));
            originalScale = transform.localScale;
            CreatePages();
            foreach (var page in watchPages)
            {
                try
                {
                    page.OnPostModSetup();
                }
                catch (Exception e)
                {
                    Debug.LogError($"There was an error while calling PostPostModSetup for screen {page.GetType().FullName}: {e}");
                }
            }
            SwitchToPage(typeof(IntroPage));
            gameObject.AddComponent<NetworkManager>();
        }

        bool TiltAngleCheck()
        {
#if DEBUG
            return true;
#endif
            var angle = Vector3.Angle(handAngleCheckTransform.transform.up, GorillaTagger.Instance.offlineVRRig.transform.up);
            if (angle < Config.closeTiltAngle.Value)
            {
                return false;
            }
            return true;
        }

        void Update()
        {
            if (!watchActive)
                return;

            if (!TiltAngleCheck())
            {
                watchActive = false;
                background.gameObject.SetActive(false);
                return;
            }

            transform.localScale = originalScale * GorillaLocomotion.Player.Instance.scale;
            transform.position = watch.transform.position + new Vector3(0, 0.3f * GorillaLocomotion.Player.Instance.scale, 0);
            transform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(transform.position - GorillaTagger.Instance.offlineVRRig.transform.position).eulerAngles.y + 180, 0f);
        }
        void CreatePages()
        {
            foreach (var type in pageTypes)
            {
                try
                {
                    Debug.Log("Creating Page: " + type.Name);
                    var watchPage = (WatchPage)gameObject.AddComponent(type);
                    watchPages.Add(watchPage);
                }
                catch (Exception e)
                {
                    Debug.LogError($"There was an error while setting up {type} please send this error to husky9424 on discord: " + e);
                    continue;
                }
            }
        }

        public void UpdateScreen()
        {
            try
            {
                screenText.text = displayingPage.OnGetScreenContent();
            }
            catch (Exception exception)
            {
                var e = $"There was an error:\n{exception}".WrapColor(Color.red);
                Debug.LogError(e);
                FailurePage.e = e;
                SwitchToPage(typeof(FailurePage));
            }
        }
        public void PressButton(WatchButtonType type)
        {
            if (!TiltAngleCheck())
                return;

            if (GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity.magnitude > Config.velocityCheck.Value)
                return;

            Debug.Log($"Pressed Watch Button: {type}");
            if (Config.buttonVibration.Value)
            {
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);
            }
            if (Config.buttonSound.Value)
            {
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, false, 0.05f);
            }
            if (type == WatchButtonType.Watch)
            {
                watchActive = !watchActive;
                background.gameObject.SetActive(watchActive);
                return;
            }
            displayingPage.OnButtonPressed(type);
            UpdateScreen();
        }
        public void SwitchToPage(WatchPage screen)
        {
            if (screen == null)
                return;


            MonkeWatch.Instance.displayingPage = screen;
            screen.OnPageOpen();
            MonkeWatch.Instance.UpdateScreen();
            Debug.Log($"Switched To Page: " + screen.GetType().Name);
        }
        public void SwitchToPage(Type screenType)
        {
            WatchPage screen = MonkeWatch.Instance.watchPages.First(screen => screen.GetType() == screenType);
            if (screen != null)
            {
                MonkeWatch.Instance.SwitchToPage(screen);
            }
        }
        public override void OnJoinedRoom()
        {
            try
            {
                displayingPage?.OnRoomStateUpdated();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public override void OnLeftRoom()
        {
            try
            {
                displayingPage?.OnRoomStateUpdated();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public override void OnPlayerEnteredRoom(Player player)
        {
            try
            {
                displayingPage?.OnRoomStateUpdated();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public override void OnPlayerLeftRoom(Player player)
        {
            try
            {
                displayingPage?.OnRoomStateUpdated();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}
