using KaChow.AByteSizedMuseum;
using TMPro;
using UnityEngine;

namespace KaChow.DebugUtils
{
    public class FrameRateCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text display;

        public enum DisplayMode { FPS, MS }

        [SerializeField] private DisplayMode displayMode = DisplayMode.FPS;

        [SerializeField, Range(0.1f, 2f)] private float sampleDuration = 1f;

        private int frames;
        private float duration, bestDuration, worstDuration;

        private void Start()
        {
            ResetFrameData();
        }

        private void Update()
        {
            float frameDuration = Time.unscaledDeltaTime;

            AccumulateFrameData(frameDuration);

            if (duration >= sampleDuration)
            {
                UpdateDisplay();
                ResetFrameData();
            }
        }

        private void AccumulateFrameData(float frameDuration)
        {
            frames++;
            duration += frameDuration;
            bestDuration = Mathf.Min(bestDuration, frameDuration);
            worstDuration = Mathf.Max(worstDuration, frameDuration);
        }

        private void UpdateDisplay()
        {
            string format = displayMode == DisplayMode.FPS ? "\n{0:0}\n{1:0}\n{2:0}" : "\n{0:1}\n{1:1}\n{2:1}";
            display.SetText(format,
                            1f / bestDuration,
                            frames / duration,
                            1f / worstDuration);
        }

        private void ResetFrameData()
        {
            frames = 0;
            duration = 0f;
            bestDuration = float.MaxValue; // Initialize to MaxValue for FPS calculation
            worstDuration = 0f;
        }
    }
}
