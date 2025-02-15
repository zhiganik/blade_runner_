using Assets.RunnerCharacter.SubComponentSystem.Base;

namespace Assets.RunnerCharacter.SubComponentSystem.SubComponents
{
    public class Attack : SubComponent
    {
        private void OnValidate()
        {
            type = SubComponentType.Attack;
        }
        
        public override void OnUpdate()
        {                

        }

        public override void OnFixedUpdate()
        {
            AttackProcess();
        }

        private void AttackProcess()
        {
            if(!Input.Tap || !runner) return;
            
            runner.RunnerAnimator.SetAttack();
        }
    }
}