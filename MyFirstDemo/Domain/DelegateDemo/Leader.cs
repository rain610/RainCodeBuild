using System;
using System.Collections.Generic;
using System.Text;

namespace Context.DelegateDemo
{
    public class Leader
    {
        /// <summary>
        /// 首领A举杯委托
        /// </summary>
        /// <param name="hand">手：左、右</param>
        public delegate void RaiseEventHandler(string hand);

        /// <summary>
        /// 首领A摔杯委托
        /// </summary>
        public delegate void FallEventHandler();

        public event RaiseEventHandler RaiseEvent;

        public event FallEventHandler FallEvent;

        public void Raise(string hand)
        {
            Console.WriteLine($"首领A{hand}手举杯");
            RaiseEvent?.Invoke(hand);
        }

        public void Fall()
        {
            Console.WriteLine("首领A摔杯");
            FallEvent?.Invoke();
        }
    }
}
