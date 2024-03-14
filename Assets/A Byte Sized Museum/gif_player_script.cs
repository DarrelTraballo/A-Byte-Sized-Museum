using UnityEngine;
using UnityEngine.UI;

public class AnimatedPlayer : MonoBehaviour
{
    public Texture2D[] gifFrames; // Array to hold the frames of the GIF
    public float framesPerSecond = 10f; // Speed of the animation
    public bool loop = true; // Whether to loop the animation

    public RawImage rawImage;
    private int currentFrame = 0;
    private float timer = 0f;

    private void Start()
    {
        if (gifFrames.Length > 0)
        {
            rawImage.texture = gifFrames[0];
        }
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (gifFrames.Length <= 1)
        {
            // No need to animate if there's only one frame
            return;
        }

        timer += Time.deltaTime;

        if (timer >= 1f / framesPerSecond)
        {
            timer = 0f;

            currentFrame++;
            if (currentFrame >= gifFrames.Length)
            {
                if (loop)
                {
                    currentFrame = 0;
                }
                else
                {
                    // Optionally stop the animation if loop is disabled
                    currentFrame = gifFrames.Length - 1;
                }
            }

            rawImage.texture = gifFrames[currentFrame];
        }
    }
}
