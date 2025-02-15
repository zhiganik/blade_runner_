using Assets.RunnerCharacter.SubComponentSystem.SubData;
using UnityEngine;

namespace Assets.RunnerCharacter
{
    public static class Utils
    {
        
        private const float Threshold = 5f;

        public static SwipeDirection CalculateDirection(Vector2 start, Vector2 end)
        {
            var delta = start - end;
            var deltaY = delta.y;
            var deltaX = delta.x;

            if (Mathf.Abs(deltaY) > Threshold && Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
            {
                return deltaY > 0 ? SwipeDirection.Down : SwipeDirection.Up;
            }
            else if(Mathf.Abs(deltaX) > Threshold && Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                return deltaX > 0 ? SwipeDirection.Left : SwipeDirection.Right;
            }
            else
            {
                return SwipeDirection.None;
            }
        }
        
    }
}