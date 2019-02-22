using System;
using System.Collections.Generic;
using System.Text;

namespace Context.StagePattern
{
    public class DoorPlus
    {
        Dictionary<OperationType, Action> currentOpRule;
        Dictionary<State, Dictionary<OperationType, Action>> rule;
        State state;
        public State State
        {
            get { return state; }
            set
            {
                if (state != value)
                    currentOpRule = rule[value];
                state = value;
            }
        }

        public DoorPlus(State state)
        {
            this.State = state;

            rule = new Dictionary<State, Dictionary<OperationType, Action>>();
            foreach (var e in Enum.GetValues(typeof(State)))
            {
                rule[(State)e] = new Dictionary<OperationType, Action>();
            }

            currentOpRule = rule[State];

            InitOperationRule();
        }

        void InitOperationRule()
        {
            //正常操作
            rule[State.Closed][OperationType.Push] = () => { Console.WriteLine("门被推开了"); State = State.Open; };
            rule[State.Open][OperationType.Pull] = () => { Console.WriteLine("门被拉上了"); State = State.Closed; };

            ////加入几种特殊情况的处理
            //rule[State.Closed][Operation.Pull] = () => Console.WriteLine("门是关上的，拉了也白拉");
            //rule[State.Open][Operation.Push] = () => Console.WriteLine("门是开的，不用推了，直接进去吧");
        }

        public void Process(OperationType op)
        {
            try
            {
                currentOpRule[op]();
            }
            catch (KeyNotFoundException)
            {

                Console.WriteLine(string.Format("门在{0}状态下不允许{1}操作", State, op));
            }
        }
    }
}
