﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FluentTask
{
    class Behavior
    {
        private List<Action> finalActions;
        private List<Action> actions; 

        public Behavior()
        {
            finalActions = new List<Action>();
            actions = new List<Action>();
        }

        public Behavior Say(string replic)
        {
            if (finalActions.Count != 0)
                Console.WriteLine(replic);
            else
            {
                actions.Add(
                    () =>
                        Console.WriteLine(replic)
                    );
            }
            return this;
        }

        public Behavior UntilKeyPressed(Func<Behavior, Behavior> function)
        {
            if (finalActions.Count == 0)
            {
                actions.Add(
                    () =>
                    {
                        while (!Console.KeyAvailable)
                        {
                            function(this);
                            Thread.Sleep(1000);
                        }
                        Console.ReadKey();
                        Console.WriteLine("I finished");
                        Thread.Sleep(2000);
                    }
                    );
            }
            else
            {
                while (!Console.KeyAvailable)
                {
                    function(this);
                    Thread.Sleep(1000);
                }
                Console.ReadKey();
                Console.WriteLine("I finished");
                Thread.Sleep(2000);
            }
            return this;
        }


        public Behavior Delay(TimeSpan time)
        {
            if (finalActions.Count != 0) Thread.Sleep(time);
            else
            {
                actions.Add(
                    () =>
                        Thread.Sleep(time)
                    );
            }
            return this;
        }

        public Behavior Jump(JumpHeight hight)
        {
            if (hight == JumpHeight.High) return Say("High");
            else return Say("Low");
        }

        public void Execute()
        {
            finalActions = new List<Action> (actions);
            actions = new List<Action>();
            
            foreach (var currentAction in finalActions)
                currentAction();
            
            actions = new List<Action>(finalActions);
            finalActions = new List<Action>();
        }
    }

    internal class Program
    {
        private static void Main()
        {

            var behaviour = new Behavior()
                .Say("Привет мир!")
                .UntilKeyPressed(b => b
                    .Say("Ля-ля-ля!")
                    .Say("Тру-лю-лю"))
                .Jump(JumpHeight.High)
                .UntilKeyPressed(b => b
                    .Say("Aa-a-a-a-aaaaaa!!!")
                    .Say("[набирает воздух в легкие]"))
                .Say("Ой!")
                .Delay(TimeSpan.FromSeconds(1))
                .Say("Кто здесь?!")
                .Delay(TimeSpan.FromMilliseconds(2000));
            behaviour.UntilKeyPressed(c => c.UntilKeyPressed(b => b.Say("Recursive until")));
            Console.WriteLine("start");
            behaviour.Execute();
            behaviour.Execute();
        }

    }
}