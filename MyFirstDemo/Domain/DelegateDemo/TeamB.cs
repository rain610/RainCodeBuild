using System;
using System.Collections.Generic;
using System.Text;
using static Context.DelegateDemo.Leader;

namespace Context.DelegateDemo
{
    public class TeamB
    {
        Leader leader;
        public TeamB(Leader leader)
        {
            this.leader = leader;
            leader.RaiseEvent += new RaiseEventHandler(LeaderRaiseEvent);
            leader.FallEvent += new FallEventHandler(LeaderFallEvent);
        }

        void LeaderRaiseEvent(string hand)
        {
            if (hand.Equals("右"))
            {
                Attack();
            }
        }

        void LeaderFallEvent()
        {
            Attack();
        }

        public void Attack()
        {
            Console.WriteLine("部下B发起攻击，大喊：看我青龙弯月刀！");
        }
    }
}
