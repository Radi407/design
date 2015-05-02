using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FluentTask
{
    class Behavior
    {
        private List<Action> FinalActions;
        private List<Action> Actions; 

        public Behavior()
        {
            FinalActions = new List<Action>();
            Actions = new List<Action>();
        }

        public Behavior Say(string replic)
        {
            Actions.Add(
                () =>
                Console.WriteLine(replic)
                );
            
            return this;
        }

        public Behavior UntilKeyPressed(Func<Behavior, Behavior> function)
        {
            Actions.Add(
                () =>
                {
                    while (!Console.KeyAvailable)
                    {
                        function(this);
                        Thread.Sleep(500);
                    }
                    Console.ReadKey();
                }
                );
            return this;
        }


        public Behavior Delay(TimeSpan time)
        {
            Actions.Add(
                () =>
                    Thread.Sleep(time)
                );
            return this;
        }

        public Behavior Jump(JumpHeight hight)
        {
            if (hight == JumpHeight.High) return Say("High");
            else return Say("Low");
        }

        public void Execute()
        {
            FinalActions = new List<Action> (Actions);
            Actions = new List<Action>();
            foreach (var currentAction in FinalActions)
                currentAction();
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
		    Console.WriteLine("start");
			behaviour.Execute();
		    behaviour.Execute();

		}
	}
}