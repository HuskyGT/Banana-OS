using BepInEx;
using BananaOS.Patches;
using System;
using UnityEngine;
using static BananaOS.MonkeWatch;

namespace BananaOS
{
    public enum WatchButtonType
    {
        Up = 0,
        Down = 1,
        Right = 2,
        Left = 3,
        Enter = 4,
        Back = 5,
        Watch = 6,
    }

    public class WatchButton : MonoBehaviour
    {
        public WatchButtonType buttonType;
        public Renderer buttonRenderer;
        public float buttonTime;
        public bool isPressed;
        public bool canPress;

        public void Init()
        {
            buttonRenderer = GetComponent<Renderer>();
            UpdateColor();
        }

        void Update()
        {
            if (canPress)
                return;

            buttonTime += Time.deltaTime;
            if (buttonTime > Config.buttonPressCooldown.Value)
            {
                buttonTime = 0;
                canPress = true;
            }
        }
        void OnTriggerEnter(Collider collider)
        {
            if (!canPress)
                return;

            var component = collider.GetComponent<GorillaTriggerColliderHandIndicator>();
            if (component == null)
                return;

            if (component.isLeftHand)
                return;

            canPress = false;
            Active(true);
        }
        void OnTriggerExit()
        {
            Active(false);
            MonkeWatch.Instance?.displayingPage?.OnButtonReleased(buttonType);
        }

        void Active(bool value)
        {
            isPressed = value;
            if (value)
            {
                MonkeWatch.Instance.PressButton(buttonType);
            }
            {
                UpdateColor();
            }
        }
        void UpdateColor()
        {
            if (buttonType == WatchButtonType.Watch)
                return;

            if (!isPressed)
            {
                switch (buttonType)
                {
                    case WatchButtonType.Enter:
                        buttonRenderer.material.color = Config.enterButtonColor.Value;
                        break;
                    case WatchButtonType.Back:
                        buttonRenderer.material.color = Config.backButtonColor.Value;
                        break;
                    default:
                        buttonRenderer.material.color = Config.arrowButtonColor.Value;
                        break;
                }
                return;
            }
            buttonRenderer.material.color = Config.buttonPressColor.Value;
        }
    }
}
