using SSTU.GrammaticsCreator;
using SSTU.PatternsCreator;
using System;
using System.IO;
using System.Windows.Forms;

namespace OntologyExtender
{
	public partial class Form1 : Form
	{
		public string ontologyFileName;
		public string sourceTextFileName;

		public Form1()
		{
			InitializeComponent();
		}

		private void LoadOntology_Click(object sender, EventArgs e)
		{
			if (loadOntologyDialog.ShowDialog() == DialogResult.Cancel)
			{
				return;
			}

			ontologyFileName = loadOntologyDialog.FileName;

		}

		private void GenerateRowOlsplPatterns_Click(object sender, EventArgs e)
		{
			var patternCreator = new PatternsCreator();
			var result = patternCreator.CreateOlspls(ontologyFileName);
		}

		private void LoadGrammatics_Click(object sender, EventArgs e)
		{
			if (sourceTextDialog.ShowDialog() == DialogResult.Cancel)
			{
				return;
			}

			var sourceText = File.ReadAllText(sourceTextDialog.FileName);

			var grammaticCreator = new GrammaticCreator();
			var jsonData = grammaticCreator.GetJsonData(sourceText);

		}
	}
}
