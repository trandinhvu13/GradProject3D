using Enemies.StationGuard.State;

namespace Enemies.StationGuard
{
    public class StationGuardStateMachine : StateMachine
    {
        public global::Enemies.StationGuard.StationGuard stationGuard;
    
        public StationGuardIdle idleState;
        public StationGuardSuspect suspectState;
        public StationGuardAlert alertState;
        public StationGuardWin winState;


        protected override BaseState GetInitialState()
        {
            return idleState;
        }

        private void Awake()
        {
            idleState = new StationGuardIdle(this);
            alertState = new StationGuardAlert(this);
            suspectState = new StationGuardSuspect(this);
            winState = new StationGuardWin(this);
        }
    }
}