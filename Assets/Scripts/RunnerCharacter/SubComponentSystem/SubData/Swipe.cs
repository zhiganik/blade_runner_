using UnityEngine;

namespace BladeRunner
{
    public class Swipe
    {
        public Vector2 StartPosition;
        public Vector2 EndPosition;

        public float Distance => Vector2.Distance(StartPosition, EndPosition);
    }

    public enum SwipeDirection
    {
        None,
        Left,
        Right,
        Up,
        Down
    }
}