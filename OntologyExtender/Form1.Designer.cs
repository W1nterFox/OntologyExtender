namespace OntologyExtender
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.LoadOntology = new System.Windows.Forms.Button();
			this.GenerateRowOlsplPatterns = new System.Windows.Forms.Button();
			this.LoadGrammatics = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.NewtOlspl = new System.Windows.Forms.Button();
			this.PreviousOLSPL = new System.Windows.Forms.Button();
			this.SetAccordanceOlsplAndGrammatics = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.loadOntologyDialog = new System.Windows.Forms.OpenFileDialog();
			this.sourceTextDialog = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// LoadOntology
			// 
			this.LoadOntology.Location = new System.Drawing.Point(25, 63);
			this.LoadOntology.Name = "LoadOntology";
			this.LoadOntology.Size = new System.Drawing.Size(202, 70);
			this.LoadOntology.TabIndex = 0;
			this.LoadOntology.Text = "LoadOntology";
			this.LoadOntology.UseVisualStyleBackColor = true;
			this.LoadOntology.Click += new System.EventHandler(this.LoadOntology_Click);
			// 
			// GenerateRowOlsplPatterns
			// 
			this.GenerateRowOlsplPatterns.Location = new System.Drawing.Point(351, 63);
			this.GenerateRowOlsplPatterns.Name = "GenerateRowOlsplPatterns";
			this.GenerateRowOlsplPatterns.Size = new System.Drawing.Size(199, 70);
			this.GenerateRowOlsplPatterns.TabIndex = 1;
			this.GenerateRowOlsplPatterns.Text = "GenerateRowOlsplPatterns";
			this.GenerateRowOlsplPatterns.UseVisualStyleBackColor = true;
			this.GenerateRowOlsplPatterns.Click += new System.EventHandler(this.GenerateRowOlsplPatterns_Click);
			// 
			// LoadGrammatics
			// 
			this.LoadGrammatics.Location = new System.Drawing.Point(684, 63);
			this.LoadGrammatics.Name = "LoadGrammatics";
			this.LoadGrammatics.Size = new System.Drawing.Size(165, 70);
			this.LoadGrammatics.TabIndex = 2;
			this.LoadGrammatics.Text = "LoadGrammatics";
			this.LoadGrammatics.UseVisualStyleBackColor = true;
			this.LoadGrammatics.Click += new System.EventHandler(this.LoadGrammatics_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(44, 200);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(805, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "ТУТ БУДУТ две колонки с понятиями из грамматик для установления соответствия межд" +
    "у грамматиками и паттернами";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(96, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 17);
			this.label2.TabIndex = 4;
			this.label2.Text = "1 шаг";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(431, 31);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 17);
			this.label3.TabIndex = 5;
			this.label3.Text = "2 шаг";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(735, 31);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 17);
			this.label4.TabIndex = 6;
			this.label4.Text = "3 шаг";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(96, 233);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(663, 17);
			this.label5.TabIndex = 7;
			this.label5.Text = "Для каждого паттерна нужно будет установить соответствие между Var из паттернов и" +
    " грамматик";
			// 
			// NewtOlspl
			// 
			this.NewtOlspl.Location = new System.Drawing.Point(520, 276);
			this.NewtOlspl.Name = "NewtOlspl";
			this.NewtOlspl.Size = new System.Drawing.Size(185, 50);
			this.NewtOlspl.TabIndex = 8;
			this.NewtOlspl.Text = "Следующий OLSPL паттерн";
			this.NewtOlspl.UseVisualStyleBackColor = true;
			// 
			// PreviousOLSPL
			// 
			this.PreviousOLSPL.Location = new System.Drawing.Point(202, 276);
			this.PreviousOLSPL.Name = "PreviousOLSPL";
			this.PreviousOLSPL.Size = new System.Drawing.Size(181, 50);
			this.PreviousOLSPL.TabIndex = 9;
			this.PreviousOLSPL.Text = "Предыдущий OLSPL паттерн";
			this.PreviousOLSPL.UseVisualStyleBackColor = true;
			// 
			// SetAccordanceOlsplAndGrammatics
			// 
			this.SetAccordanceOlsplAndGrammatics.Location = new System.Drawing.Point(389, 276);
			this.SetAccordanceOlsplAndGrammatics.Name = "SetAccordanceOlsplAndGrammatics";
			this.SetAccordanceOlsplAndGrammatics.Size = new System.Drawing.Size(125, 50);
			this.SetAccordanceOlsplAndGrammatics.TabIndex = 10;
			this.SetAccordanceOlsplAndGrammatics.Text = "Установить соответствие";
			this.SetAccordanceOlsplAndGrammatics.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(431, 166);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 17);
			this.label6.TabIndex = 11;
			this.label6.Text = "4 шаг";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(306, 372);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(284, 48);
			this.button1.TabIndex = 12;
			this.button1.Text = "Сохранить заполненную онтологию";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(428, 348);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(44, 17);
			this.label8.TabIndex = 14;
			this.label8.Text = "5 шаг";
			// 
			// loadOntologyDialog
			// 
			this.loadOntologyDialog.Filter = "(*.owl)|*.owl|(*.xml)|*.xml";
			this.loadOntologyDialog.InitialDirectory = "C:\\Users\\Alexey\\source\\repos\\OntologyExtender";
			// 
			// sourceTextDialog
			// 
			this.sourceTextDialog.Filter = "(*.txt)|*.txt";
			this.sourceTextDialog.InitialDirectory = "C:\\Users\\Alexey\\source\\repos\\OntologyExtender";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(901, 450);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.SetAccordanceOlsplAndGrammatics);
			this.Controls.Add(this.PreviousOLSPL);
			this.Controls.Add(this.NewtOlspl);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.LoadGrammatics);
			this.Controls.Add(this.GenerateRowOlsplPatterns);
			this.Controls.Add(this.LoadOntology);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button LoadOntology;
		private System.Windows.Forms.Button GenerateRowOlsplPatterns;
		private System.Windows.Forms.Button LoadGrammatics;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button NewtOlspl;
		private System.Windows.Forms.Button PreviousOLSPL;
		private System.Windows.Forms.Button SetAccordanceOlsplAndGrammatics;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.OpenFileDialog loadOntologyDialog;
		private System.Windows.Forms.OpenFileDialog sourceTextDialog;
	}
}

