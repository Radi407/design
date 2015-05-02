using System;
using System.Linq;
using DIContainer.Commands;
using Ninject;
namespace DIContainer
{
    public class Program
    {
        private readonly CommandLineArgs arguments;
        private readonly ICommand[] commands;

        public Program(CommandLineArgs arguments, params ICommand[] commands)
        {
            this.arguments = arguments;
            this.commands = commands;
        
        }

        static void Main(string[] args)
        {
            var container = new StandardKernel();
            container.Bind<ICommand>().To<TimerCommand>();
            container.Bind<ICommand>().To<PrintTimeCommand>();
            container.Bind<ICommand>().ToConstant<HelpCommand
            container.Bind<CommandLineArgs>().ToConstant(new CommandLineArgs(args));

            //var arguments = new CommandLineArgs(args);
            //var printTime = new PrintTimeCommand();
            //var timer = new TimerCommand(arguments);
            //var commands = new ICommand[] { printTime, timer };
            var program = container.Get<Program>();
            //new Program(arguments, commands).Run();
            program.Run();
        }

        public void Run()
        {
            if (arguments.Command == null)
            {
                Console.WriteLine("Please specify <command> as the first command line argument");
                return;
            }
            var command = commands.FirstOrDefault(c => c.Name.Equals(arguments.Command, StringComparison.InvariantCultureIgnoreCase));
            if (command == null)
                Console.WriteLine("Sorry. Unknown command {0}", arguments.Command);
            else
                command.Execute();
        }
    }
}
