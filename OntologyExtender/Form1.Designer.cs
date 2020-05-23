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
			this.LoadFacts = new System.Windows.Forms.Button();
			this.ExtendAndSave = new System.Windows.Forms.Button();
			this.loadOntologyDialog = new System.Windows.Forms.OpenFileDialog();
			this.factsDialog = new System.Windows.Forms.OpenFileDialog();
			this.textBoxOntologyPath = new System.Windows.Forms.RichTextBox();
			this.textBoxFactsPath = new System.Windows.Forms.RichTextBox();
			this.saveOntologyDialog = new System.Windows.Forms.SaveFileDialog();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addAdditionalPatterns = new System.Windows.Forms.Button();
			this.loadAdditionalPatternsDialog = new System.Windows.Forms.OpenFileDialog();
			this.textBoxPatternsPath = new System.Windows.Forms.RichTextBox();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// LoadOntology
			// 
			this.LoadOntology.Location = new System.Drawing.Point(25, 63);
			this.LoadOntology.Name = "LoadOntology";
			this.LoadOntology.Size = new System.Drawing.Size(202, 70);
			this.LoadOntology.TabIndex = 0;
			this.LoadOntology.Text = "Выбрать онтологию";
			this.LoadOntology.UseVisualStyleBackColor = true;
			this.LoadOntology.Click += new System.EventHandler(this.LoadOntology_Click);
			// 
			// LoadFacts
			// 
			this.LoadFacts.Location = new System.Drawing.Point(25, 158);
			this.LoadFacts.Name = "LoadFacts";
			this.LoadFacts.Size = new System.Drawing.Size(202, 67);
			this.LoadFacts.TabIndex = 2;
			this.LoadFacts.Text = "Выбрать факты";
			this.LoadFacts.UseVisualStyleBackColor = true;
			this.LoadFacts.Click += new System.EventHandler(this.LoadFacts_Click);
			// 
			// ExtendAndSave
			// 
			this.ExtendAndSave.Location = new System.Drawing.Point(256, 344);
			this.ExtendAndSave.Name = "ExtendAndSave";
			this.ExtendAndSave.Size = new System.Drawing.Size(202, 67);
			this.ExtendAndSave.TabIndex = 12;
			this.ExtendAndSave.Text = "Заполнить онтологию данными и сохранить";
			this.ExtendAndSave.UseVisualStyleBackColor = true;
			this.ExtendAndSave.Click += new System.EventHandler(this.ExtendAndSave_Click);
			// 
			// loadOntologyDialog
			// 
			this.loadOntologyDialog.Filter = "(*.owl)|*.owl|(*.xml)|*.xml";
			this.loadOntologyDialog.InitialDirectory = "C:\\Users\\Alexey\\source\\repos\\OntologyExtender";
			// 
			// factsDialog
			// 
			this.factsDialog.InitialDirectory = "C:\\Users\\Alexey\\source\\repos\\OntologyExtender";
			// 
			// textBoxOntologyPath
			// 
			this.textBoxOntologyPath.Enabled = false;
			this.textBoxOntologyPath.Location = new System.Drawing.Point(256, 63);
			this.textBoxOntologyPath.Name = "textBoxOntologyPath";
			this.textBoxOntologyPath.Size = new System.Drawing.Size(443, 70);
			this.textBoxOntologyPath.TabIndex = 13;
			this.textBoxOntologyPath.Text = "";
			// 
			// textBoxFactsPath
			// 
			this.textBoxFactsPath.Enabled = false;
			this.textBoxFactsPath.Location = new System.Drawing.Point(256, 155);
			this.textBoxFactsPath.Name = "textBoxFactsPath";
			this.textBoxFactsPath.Size = new System.Drawing.Size(443, 70);
			this.textBoxFactsPath.TabIndex = 14;
			this.textBoxFactsPath.Text = "";
			// 
			// saveOntologyDialog
			// 
			this.saveOntologyDialog.Filter = "(*.owl)|*.owl|(*.xml)|*.xml|All files(*.*)|*.*";
			this.saveOntologyDialog.ShowHelp = true;
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(744, 28);
			this.menuStrip1.TabIndex = 15;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
			// 
			// addAdditionalPatterns
			// 
			this.addAdditionalPatterns.Enabled = false;
			this.addAdditionalPatterns.Location = new System.Drawing.Point(25, 253);
			this.addAdditionalPatterns.Name = "addAdditionalPatterns";
			this.addAdditionalPatterns.Size = new System.Drawing.Size(202, 67);
			this.addAdditionalPatterns.TabIndex = 16;
			this.addAdditionalPatterns.Text = "Выбрать дополнительные OLSPL-паттерны";
			this.addAdditionalPatterns.UseVisualStyleBackColor = true;
			this.addAdditionalPatterns.Click += new System.EventHandler(this.AddOlsplPatterns_Click);
			// 
			// loadAdditionalPatternsDialog
			// 
			this.loadAdditionalPatternsDialog.FileName = "openFileDialog1";
			// 
			// textBoxPatternsPath
			// 
			this.textBoxPatternsPath.Enabled = false;
			this.textBoxPatternsPath.Location = new System.Drawing.Point(256, 253);
			this.textBoxPatternsPath.Name = "textBoxPatternsPath";
			this.textBoxPatternsPath.Size = new System.Drawing.Size(443, 70);
			this.textBoxPatternsPath.TabIndex = 17;
			this.textBoxPatternsPath.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(744, 438);
			this.Controls.Add(this.textBoxPatternsPath);
			this.Controls.Add(this.addAdditionalPatterns);
			this.Controls.Add(this.textBoxFactsPath);
			this.Controls.Add(this.textBoxOntologyPath);
			this.Controls.Add(this.ExtendAndSave);
			this.Controls.Add(this.LoadFacts);
			this.Controls.Add(this.LoadOntology);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = " OntologyExtender v1.1";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button LoadOntology;
		private System.Windows.Forms.Button LoadFacts;
		private System.Windows.Forms.Button ExtendAndSave;
		private System.Windows.Forms.OpenFileDialog loadOntologyDialog;
		private System.Windows.Forms.OpenFileDialog factsDialog;
		private System.Windows.Forms.RichTextBox textBoxOntologyPath;
		private System.Windows.Forms.RichTextBox textBoxFactsPath;
		private System.Windows.Forms.SaveFileDialog saveOntologyDialog;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.Button addAdditionalPatterns;
		private System.Windows.Forms.OpenFileDialog loadAdditionalPatternsDialog;
		private System.Windows.Forms.RichTextBox textBoxPatternsPath;
	}
}

