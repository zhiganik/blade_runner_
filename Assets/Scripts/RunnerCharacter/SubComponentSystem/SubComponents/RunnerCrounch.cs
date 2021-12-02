// using UnityEngine;
// using UnityEngine.InputSystem;
//
// namespace BladeRunner
// {
//     public class RunnerCrounch : SubComponent
//     {
//         [SerializeField] private InputActionReference crunch;
//
//         private void Start()
//         {
//             subComponentProcessor.ArrSubComponents[(int)SubComponentType.CHARACTER_CRUNCH] = this;
//         }
//         
//         public override void OnUpdate()
//         {
//             // if(crunch.action.triggered)
//                 // RunnerControl.Animator.SetCrunch();
//         }
//
//         public override void OnFixedUpdate()
//         {
//             
//         }
//     }
// }