using System;
using System.Collections.Generic;
using System.Text;
using static Context.DelegateDemo.Leader;

namespace Context.DelegateDemo
{
    public class TeamA
    {
        Leader leader;
        public TeamA(Leader leader)
        {
            this.leader = leader;
            leader.RaiseEvent += new RaiseEventHandler(LeaderRaiseEvent);
            leader.FallEvent += new FallEventHandler(LeaderFallEvent);
        }

        void LeaderRaiseEvent(string hand)
        {
            if (hand.Equals("左"))
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
            Console.WriteLine("部下A发起攻击，大喊：燕人张翼德在此！");
        }
    }
}
