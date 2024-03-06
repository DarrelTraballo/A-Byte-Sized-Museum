using TMPro;
using UnityEngine;

namespace KaChow.DebugUtils
{
    public class FrameRateCounter : MonoBehaviour
    {
        [SerializeField] private GameObject debugConsole;
        private Canvas debugConsoleCanvas;

        [SerializeField] private TMP_Text display;

        public enum DisplayMode { FPS, MS }

        [SerializeField] private DisplayMode displayMode = DisplayMode.FPS;

        [SerializeField, Range(0.1f, 2f)] private float sampleDuration = 1f;

        private int frames;
        private float duration, bestDuration = float.MaxValue, worstDuration;

        private bool isActive = false;

        private void Start()
        {
            debugConsoleCanvas = debugConsole.GetComponentInChildren<Canvas>();
            debugConsoleCanvas.enabled = isActive;
        }

        private void Update()
        {
            float frameDuration = Time.unscaledDeltaTime;

            frames += 1;
            duration += frameDuration;

            if (frameDuration < bestDuration)
            {
                bestDuration = frameDuration;
            }

            if (frameDuration > worstDuration)
            {
                worstDuration = frameDuration;
            }

            if (duration >= sampleDuration)
            {
                if (displayMode == DisplayMode.FPS)
                {
                    display.SetText(
                        "\n{0:0}\n{1:0}\n{2:0}",
                        1f / bestDuration,
                        frames / duration,
                        1f / worstDuration
                    );
                }
                else
                {
                    display.SetText(
                        "MS\n{0:1}\n{1:1}\n{2:1}",
                        1000f * bestDuration,
                        1000f * duration / frames,
                        1000f * worstDuration
                    );
                }
                frames = 0;
                duration = 0f;
                bestDuration = float.MaxValue;
                worstDuration = 0f;
            }

            ToggleDebugConsole();
        }

        private void ToggleDebugConsole()
        {
            if (Input.GetKeyDown(KeyCode.F3))
            {
                isActive = !isActive;

                debugConsoleCanvas.enabled = isActive;
            }
        }
    }
}
