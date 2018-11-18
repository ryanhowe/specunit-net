using System;
using System.IO;
using System.Reflection;
using System.Text;
using Moq;
using SpecUnit.Report;
using Console = SpecUnit.Report.Console;

namespace SpecUnit.Specs
{
    [Concern(typeof(Console))]
    public class when_no_arguments_are_provided : ConsoleOutputFixture
    {
        protected override void Context()
        {
            Capture_Console_Output_To_String();

            string[] consoleArgs = new string[0];

            Console.Main(consoleArgs);
        }

        protected override void CleanUpContext()
        {
            Restore_Console_Output_To_Original_Device();
        }

        [Observation]
        public void should_display_an_error_message()
        {
            ConsoleOutputContent.ShouldContain("Usage \"SpecUnit.Report.exe <assembly name>\"");
        }
    }

    [Concern(typeof(Console))]
    public class when_there_is_no_assembly_that_corresponds_to_the_assembly_name_provided_as_a_console_argument : ConsoleOutputFixture
    {
        private string _assemblyFilePath;

        protected override void Context()
        {
            Capture_Console_Output_To_String();

            _assemblyFilePath = Guid.NewGuid().ToString();

            string[] consoleArgs = new string[] {_assemblyFilePath};

            Console.Main(consoleArgs);
        }

        protected override void CleanUpContext()
        {
            Restore_Console_Output_To_Original_Device();
        }

        [Observation]
        public void should_display_an_error_message()
        {
            ConsoleOutputContent.ShouldContain(String.Format("{0} was not found", _assemblyFilePath));
        }
    }

    [Concern(typeof(Console))]
    public class when_given_a_valid_assembly_file_path : ContextSpecification
    {
        private string _assemblyFilePath;
        private Mock<IReportGenerator> _reportGenerator;

        protected override void Context()
        {
            _reportGenerator = new Mock<IReportGenerator>();
            Console.ReportGenerator = _reportGenerator.Object;
        }

        protected override void Because_After()
        {
            _assemblyFilePath = Assembly.GetExecutingAssembly().Location;
            string[] consoleArgs = new string[] {_assemblyFilePath};

            _reportGenerator.VerifyAll();

            // because
            Console.Main(consoleArgs);

            _reportGenerator.VerifyAll();
        }

        [Observation]
        public void should_write_the_spec_report_for_the_assembly_indicated_by_the_file_path()
        {
            _reportGenerator.Setup(r => r.WriteReport(It.IsAny<Assembly>()));
            _reportGenerator.Object.WriteReport(Assembly.GetExecutingAssembly());
        }
    }

    public abstract class ConsoleOutputFixture : ContextSpecification
    {
        private TextWriter _saveOut;
        private StringBuilder _output;

        protected StringBuilder Output
        {
            get { return _output; }
        }

        protected string ConsoleOutputContent
        {
            get { return _output.ToString(); }
        }

        public void Capture_Console_Output_To_String()
        {
            System.Console.Out.Flush();
            _saveOut = System.Console.Out;

            _output = new StringBuilder();
            System.Console.SetOut(new StringWriter(Output));
        }

        public void Restore_Console_Output_To_Original_Device()
        {
            System.Console.SetOut(_saveOut);
        }
    }
}