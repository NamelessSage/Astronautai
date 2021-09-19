
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
            this.astroDataSet = new Astronautai.AstroDataSet();
            this.astroDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.astroDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.astroDataSetBindingSource)).BeginInit();
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.astroDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.astroDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource astroDataSetBindingSource;
        private AstroDataSet astroDataSet;
    }
}

