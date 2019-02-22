using System;
using System.Collections.Generic;
using System.Text;

namespace Context.StagePattern
{
    public class Door
    {
        public State State;
        Dictionary<State, Dictionary<OperationType, Action>> rule;
        public Door(State stage)
        {
            this.State = stage;
            rule = new Dictionary<State, Dictionary<OperationType, Action>>();
            foreach (var e in Enum.GetValues(typeof(State)))
            {
                rule[(State)e] = new Dictionary<OperationType, Action>();
            }
        }
        void InitOperationRule()
        {
            //正常操作
            rule[State.Closed][OperationType.Push] = () => { Console.WriteLine("门被推开了"); State = State.Open; };
            rule[State.Open][OperationType.Pull] = () => { Console.WriteLine("门被拉上了"); State = State.Closed; };
        }

    }
}
