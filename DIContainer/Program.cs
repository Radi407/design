using System;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using DIContainer.Commands;
using Ninject;
namespace DIContainer
{
    public class HelpCommand : BaseCommand
    {
        private Lazy<ICommand[]> comands;
        private TextWriter writer;

        public HelpCommand(Lazy<ICommand[]> comands,TextWriter writer)
        {
            this.comands = comands;
            this.writter = writer;
        }

        public override void Execute()
        {
            foreach (var comand in comands.Value)
            {
                Console.WriteLine(comand.Name);
            }
        }
    }

    public class Program 
    {
        private readonly CommandLineArgs arguments;
        private readonly ICommand[] commands;
        private TextWriter writter;

        public Program(CommandLineArgs arguments,TextWriter writter , params ICommand[] commands)
        {
            this.arguments = arguments;
            this.commands = commands;
            this.writter = writter;

        }

        static void Main(string[] args)
        {
            var container = new StandardKernel();
            container.Bind<ICommand>().To<TimerCommand>();
            container.Bind<ICommand>().To<PrintTimeCommand>();
            container.Bind<ICommand>().To<HelpCommand>();
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
