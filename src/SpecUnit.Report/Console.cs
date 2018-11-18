using System;
using System.IO;
using System.Reflection;

namespace SpecUnit.Report
{
	public class Console
	{
		private static IReportGenerator _reportGenerator = new ReportGenerator();

		public static IReportGenerator ReportGenerator
		{
			set => _reportGenerator = value;
		}

		public static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				System.Console.WriteLine("Usage \"SpecUnit.Report.exe <assembly name>\"");
				return;
			}

			string assemblyName = args[0];

			if (File.Exists(assemblyName) == false)
			{
				System.Console.WriteLine(String.Format("{0} was not found", assemblyName));
				return;
			}

			Assembly assemblyUnderTest = Assembly.LoadFrom(args[0]);

			_reportGenerator.WriteReport(assemblyUnderTest);
		}
	}
}