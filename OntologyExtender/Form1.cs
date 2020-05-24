using SSTU.FactsProcesser;
using SSTU.PatternsCreator;
using SSTU.PatternsCreator.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OntologyExtender
{
	public partial class Form1 : Form
	{
		public string OntologyFileName { get; set; }
		public string FactsFileName { get; set; }
		public string AdditionalPatternsFileName { get; set; }

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

			OntologyFileName = loadOntologyDialog.FileName;
			textBoxOntologyPath.Text = OntologyFileName;
			addAdditionalPatterns.Enabled = true;

			if(!string.IsNullOrEmpty(textBoxOntologyPath.Text) && !string.IsNullOrEmpty(textBoxFactsPath.Text))
			{
				ExtendAndSave.Enabled = true;
				checkBoxSavePatterns.Enabled = true;
			}
		}

		private void LoadFacts_Click(object sender, EventArgs e)
		{
			if (factsDialog.ShowDialog() == DialogResult.Cancel)
			{
				return;
			}

			FactsFileName = factsDialog.FileName;
			textBoxFactsPath.Text = FactsFileName;

			if (!string.IsNullOrEmpty(textBoxOntologyPath.Text) && !string.IsNullOrEmpty(textBoxFactsPath.Text))
			{
				ExtendAndSave.Enabled = true;
				checkBoxSavePatterns.Enabled = true;
			}
		}

		private void ExtendAndSave_Click(object sender, EventArgs e)
		{
			try
			{
				var patternCreator = new PatternsCreator();
				var olpslPatternsFromOntology = patternCreator.CreateOlsplsFromOntology(OntologyFileName);
				var additionalOlsplPatterns = patternCreator.ReadOlsplsFromFile(AdditionalPatternsFileName);
				var totalPatterns = olpslPatternsFromOntology.Union(additionalOlsplPatterns).ToList();
				
				var factReader = new FactsProcesser();
				var facts = factReader.GetFactsFromFile(FactsFileName);
				var filledPatterns = factReader.GetTripletsFromFacts(totalPatterns, facts);

				var ontologyFiller = new OntologyFiller();
				var resultOntology = ontologyFiller.GetExtendedOntologyByPatterns(OntologyFileName, filledPatterns);
				SaveOntology(resultOntology);

				if (checkBoxSavePatterns.Checked)
				{
					var resultPatternsForSave = patternCreator.GetTextPatterns(totalPatterns);
					SavePatterns(resultPatternsForSave);
				}
			}
			catch (FormatException ex)
			{
				var infoMessage = $"{ex.Message}{Environment.NewLine}{Environment.NewLine}Пожалуйста, приведите данные к корректному формату и попробуйте снова";
				MessageBox.Show(infoMessage, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void SaveOntology(string resultOntology)
		{
			if (saveOntologyDialog.ShowDialog() == DialogResult.Cancel)
			{
				return;
			}

			string fileName = saveOntologyDialog.FileName;
			File.WriteAllText(fileName, resultOntology);

			MessageBox.Show("Онтология сохранена!");
		}

		private void SavePatterns(string resultPatterns)
		{
			if (savePatternsDialog.ShowDialog() == DialogResult.Cancel)
			{
				return;
			}

			string fileName = savePatternsDialog.FileName;
			File.WriteAllText(fileName, resultPatterns);

			MessageBox.Show("OLSPL-паттерны сохранены!");
		}

		private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var infoForm = new Form();
			infoForm.Size = new System.Drawing.Size(550, 300);
			infoForm.Text = "About";

			var labelTextStrings = new List<string>
			{
				"Ontology extender - средство автоматического наполнения онтологий.",
				Environment.NewLine,
				"Для корректной работы необходима исходная онтология и факты. В процессе работы с онтологией анализируются домены и диапазоны объектных свойств, а также аксиомы классов. Следовательно, чем их больше тем более полной будет результирующая онтология",
				Environment.NewLine,
				"Разработано в качестве практической части дипломного проекта под руководством Шульги Татьяны Эриковны",
				"Разработчики: ",
				"    -Дмитриев Алексей Олегович",
				"    -Паневин Денис Игоревич",
			};

			var label = new RichTextBox();
			label.Size = new System.Drawing.Size(infoForm.Size.Width - 40, infoForm.Size.Height - 55);
			label.Enabled = false;
			label.Left = 10;
			label.Top = 10;
			label.Text = string.Join(Environment.NewLine, labelTextStrings);

			infoForm.Controls.Add(label);
			infoForm.Show();
		}

		private void AddOlsplPatterns_Click(object sender, EventArgs e)
		{
			if (loadAdditionalPatternsDialog.ShowDialog() == DialogResult.Cancel)
			{
				return;
			}

			AdditionalPatternsFileName = loadAdditionalPatternsDialog.FileName;
			textBoxPatternsPath.Text = AdditionalPatternsFileName;
		}
	}
}
