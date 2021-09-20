
namespace Astronautai
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.astroDataSet = new Astronautai.AstroDataSet();
            this.astroDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.player1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.player2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.astroDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.astroDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2)).BeginInit();
            this.SuspendLayout();
            // 
            // astroDataSet
            // 
            this.astroDataSet.DataSetName = "AstroDataSet";
            this.astroDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // astroDataSetBindingSource
            // 
            this.astroDataSetBindingSource.DataSource = this.astroDataSet;
            this.astroDataSetBindingSource.Position = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(138, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // player1
            // 
            this.player1.Image = ((System.Drawing.Image)(resources.GetObject("player1.Image")));
            this.player1.Location = new System.Drawing.Point(138, 355);
            this.player1.Name = "player1";
            this.player1.Size = new System.Drawing.Size(26, 29);
            this.player1.TabIndex = 3;
            this.player1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(272, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // player2
            // 
            this.player2.Image = ((System.Drawing.Image)(resources.GetObject("player2.Image")));
            this.player2.Location = new System.Drawing.Point(251, 355);
            this.player2.Name = "player2";
            this.player2.Size = new System.Drawing.Size(26, 29);
            this.player2.TabIndex = 5;
            this.player2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.player2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.player1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.astroDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.astroDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource astroDataSetBindingSource;
        private AstroDataSet astroDataSet;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox player1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox player2;
    }
}

