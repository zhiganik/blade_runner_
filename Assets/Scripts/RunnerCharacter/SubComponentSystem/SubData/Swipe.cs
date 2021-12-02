using UnityEngine;

namespace BladeRunner
{
    public class Swipe
    {
        public Vector2 StartPosition;
        public Vector2 EndPosition;

        public SwipeDirection SwipeDirection { get; private set; }
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