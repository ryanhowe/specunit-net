using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using SpecUnit.Report;
using SpecUnit.Specs.AssemblyUnderTest;

namespace SpecUnit.Specs
{
	[TestFixture]
	[Concern(typeof(ReportGenerator))]
	public class when_rendering_the_report_title : ContextSpecification
	{
		private SpecificationDataset _specificationDataset;

		protected override void Context()
		{
			Assembly assemblyUnderTest = typeof(A_fixture).Assembly;
			_specificationDataset = SpecificationDataset.Build(assemblyUnderTest);
		}

		[Test]
		[Observation]
		public void should_include_the_specification_dataset_name()
		{
			ReportGenerator.RenderTitle(_specificationDataset).ShouldContain(_specificationDataset.Name);
		}

		[Test]
		[Observation]
		public void should_inlude_the_counts_of_the_concerns()
		{
			ReportGenerator.RenderTitle(_specificationDataset).ShouldContain("2 concerns");
		}

		[Test]
		[Observation]
		public void should_inlude_the_counts_of_the_contexts()
		{
			ReportGenerator.RenderTitle(_specificationDataset).ShouldContain("5 contexts");
		}

		[Test]
		[Observation]
		public void should_inlude_the_counts_of_the_specifications()
		{
			ReportGenerator.RenderTitle(_specificationDataset).ShouldContain("4 specifications");
		}

		[Test]
		[Observation]
		public void should_render_the_name_in_an_H1()
		{
			ReportGenerator.RenderTitle(_specificationDataset).ShouldBeSurroundedWith("<h1", "</h1>");
		}
	}

	[TestFixture]
	[Concern(typeof(ReportGenerator))]
	public class when_rendering_a_concern_header : ContextSpecification
	{
		private Concern _concern;

		protected override void Context()
		{
			_concern = new Concern(typeof(SomeConcern));
			_concern.AddContextFor(typeof(Context_with_concern));
			_concern.AddContextFor(typeof(Context_with_same_concern));
		}

		[Test]
		[Observation]
		public void should_include_the_counts_of_the_contexts()
		{
			ReportGenerator.RenderConcernHeader(_concern).ShouldContain("2 contexts");
		}

		[Test]
		[Observation]
		public void should_include_the_counts_of_the_specifications()
		{
			ReportGenerator.RenderConcernHeader(_concern).ShouldContain("4 specifications");
		}

		[Test]
		[Observation]
		public void should_render_the_name_in_an_H2()
		{
			ReportGenerator.RenderConcernHeader(_concern).ShouldBeSurroundedWith("<h2", "</h2>");
		}
	}

	[TestFixture]
	[Concern(typeof(ReportGenerator))]
	public class when_rendering_a_concern : ContextSpecification
	{
		private Concern _concern;

		protected override void Context()
		{
			_concern = new Concern(typeof(SomeConcern));
			_concern.AddContextFor(typeof(Context_with_concern));
			_concern.AddContextFor(typeof(Context_with_same_concern));
		}

		[Test]
		[Observation]
		public void should_render_its_contexts()
		{
			ReportGenerator.RenderConcern(_concern).ShouldContain("Context with concern");
		}
	}

	[TestFixture]
	[Concern(typeof(ReportGenerator))]
	public class when_rendering_a_context_header : ContextSpecification
	{
		private Context _context;

		protected override void Context()
		{
			_context = SpecUnit.Report.Context.Build(typeof(Context_with_concern));
		}

		[Test]
		[Observation]
		public void should_render_the_count_of_specifications()
		{
			ReportGenerator.RenderContextHeader(_context).ShouldContain("3 specifications");
		}

		[Observation]
		public void should_render_the_name_in_an_H3()
		{
			ReportGenerator.RenderContextHeader(_context).ShouldBeSurroundedWith("<h3", "</h3>");
		}
	}

	[TestFixture]
	[Concern(typeof(ReportGenerator))]
	public class when_rendering_a_context_that_behaves_like_another_context : ContextSpecification
	{
		private Context _context;

		protected override void Context()
		{
			_context = SpecUnit.Report.Context.Build(typeof(Context_with_behaves_like));
		}

		[Test]
		[Observation]
		public void should_indicate_that_it_behaves_like_another_context()
		{
			ReportGenerator.RenderContext(_context).ShouldContain("<p class=\"behaves_like\">behaves like: a common context</p>");
		}
	}

	[TestFixture]
	[Concern(typeof(ReportGenerator))]
	public class when_rendering_a_context_that_does_not_behaves_like_another_context : ContextSpecification
	{
		private Context _context;

		protected override void Context()
		{
			_context = SpecUnit.Report.Context.Build(typeof(Context_with_concern));
		}

		[Test]
		[Observation]
		public void should_indicate_that_it_behaves_like_another_context()
		{
			string contextRendering = ReportGenerator.RenderContext(_context);
			
			if (contextRendering.Contains("Behaves like") == true)
			{
				Assert.Fail("Should not contain \"behaves like\"");	
			}
		}
	}

	[TestFixture]
	[Concern(typeof(ReportGenerator))]
	public class when_rendering_a_context : ContextSpecification
	{
		private Context _context;

		protected override void Context()
		{
			_context = SpecUnit.Report.Context.Build(typeof(Context_with_concern));
		}

		[Test]
		[Observation]
		public void should_render_each_specification()
		{
			ReportGenerator.RenderSpecificationList(_context.Specifications).ShouldContain("should jump when I say how hi");
			ReportGenerator.RenderSpecificationList(_context.Specifications).ShouldContain("should jump if I say how hi");
		}

		[Test]
		[Observation]
		public void should_render_specifications_as_a_bulletted_list()
		{
			ReportGenerator.RenderSpecificationList(_context.Specifications).ShouldBeSurroundedWith("<ul>\n\t<li>", "</li>\n</ul>");
		}
	}

	[TestFixture]
	[Concern(typeof(ReportGenerator))]
	public class when_writing_the_spec_report_for_an_assembly : ContextSpecification
	{
		private ReportGenerator _reportGenerator;
		private Assembly _assembly;
		private string _reportFilePath;

		protected override void Context()
		{
			_reportGenerator = new ReportGenerator();

			_assembly = Assembly.GetExecutingAssembly();
			_reportFilePath = _assembly.GetName().Name + ".html";
		}

		protected override void Because()
		{
			_reportGenerator.WriteReport(_assembly);
		}

		[Test]
		[Observation]
		public void should_write_an_html_file_named_for_the_assembly_that_the_report_is_created_for()
		{
			File.Exists(_reportFilePath).ShouldBeTrue();
		}

		protected override void CleanUpContext()
		{
			if (File.Exists(_reportFilePath))
			{
				File.Delete(_reportFilePath);
			}
		}
	}

	[TestFixture]
	[Concern(typeof(ReportGenerator))]
	public class when_pluralizing_a_caption_for_a_count_where_the_count_is_greater_than_one : ContextSpecification
	{
		private string _caption;

		protected override void Context()
		{
			_caption = ReportGenerator.Pluralize("caption", 2);
		}

		[Test]
		[Observation]
		public void should_append__s__to_the_caption_to_indicate_the_plurality()
		{
			_caption.ShouldEqual("captions");
		}
	}

	[TestFixture]
	[Concern(typeof(ReportGenerator))]
	public class when_pluralizing_a_caption_for_a_count_where_the_count_is_zero : ContextSpecification
	{
		private string _caption;

		protected override void Context()
		{
			_caption = ReportGenerator.Pluralize("caption", 0);
		}

		[Test]
		[Observation]
		public void should_append__s__to_the_caption_to_indicate_the_plurality()
		{
			_caption.ShouldEqual("captions");
		}
	}

	[TestFixture]
	[Concern(typeof(ReportGenerator))]
	public class when_pluralizing_a_caption_for_a_count_where_the_count_is_equal_to_one : ContextSpecification
	{
		private string _caption;

		protected override void Context()
		{
			_caption = ReportGenerator.Pluralize("caption", 1);
		}

		[Test]
		[Observation]
		public void should_not_append__s__to_the_caption()
		{
			_caption.ShouldEqual("caption");
		}
	}
}