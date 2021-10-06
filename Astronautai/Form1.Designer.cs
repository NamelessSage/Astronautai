
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
            this.JoinGameButton = new System.Windows.Forms.Button();
            this.StartGameButton = new System.Windows.Forms.Button();
            this.PlayerUsernameTextBox = new System.Windows.Forms.TextBox();
            this.PlayerNameInputLabel = new System.Windows.Forms.Label();
            this.playerFocus = new System.Windows.Forms.TextBox();
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
            // JoinGameButton
            // 
            this.JoinGameButton.Location = new System.Drawing.Point(12, 42);
            this.JoinGameButton.Name = "JoinGameButton";
            this.JoinGameButton.Size = new System.Drawing.Size(75, 23);
            this.JoinGameButton.TabIndex = 1;
            this.JoinGameButton.Text = "Join";
            this.JoinGameButton.UseVisualStyleBackColor = true;
            this.JoinGameButton.Click += new System.EventHandler(this.JoinGameButton_Click);
            // 
            // StartGameButton
            // 
            this.StartGameButton.Location = new System.Drawing.Point(12, 42);
            this.StartGameButton.Name = "StartGameButton";
            this.StartGameButton.Size = new System.Drawing.Size(75, 23);
            this.StartGameButton.TabIndex = 2;
            this.StartGameButton.Text = "Start";
            this.StartGameButton.UseVisualStyleBackColor = true;
            this.StartGameButton.Visible = false;
            this.StartGameButton.Click += new System.EventHandler(this.StartGameButton_Click);
            // 
            // PlayerUsernameTextBox
            // 
            this.PlayerUsernameTextBox.Location = new System.Drawing.Point(12, 16);
            this.PlayerUsernameTextBox.MaxLength = 3;
            this.PlayerUsernameTextBox.Name = "PlayerUsernameTextBox";
            this.PlayerUsernameTextBox.Size = new System.Drawing.Size(156, 20);
            this.PlayerUsernameTextBox.TabIndex = 3;
            // 
            // PlayerNameInputLabel
            // 
            this.PlayerNameInputLabel.AutoSize = true;
            this.PlayerNameInputLabel.Location = new System.Drawing.Point(12, 0);
            this.PlayerNameInputLabel.Name = "PlayerNameInputLabel";
            this.PlayerNameInputLabel.Size = new System.Drawing.Size(88, 13);
            this.PlayerNameInputLabel.TabIndex = 4;
            this.PlayerNameInputLabel.Text = "Player username:";
            // 
            // playerFocus
            // 
            this.playerFocus.Location = new System.Drawing.Point(-1000, -1000);
            this.playerFocus.Name = "playerFocus";
            this.playerFocus.ReadOnly = true;
            this.playerFocus.Size = new System.Drawing.Size(0, 20);
            this.playerFocus.TabIndex = 5;
            this.playerFocus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.playerFocus_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(804, 601);
            this.Controls.Add(this.playerFocus);
            this.Controls.Add(this.PlayerNameInputLabel);
            this.Controls.Add(this.PlayerUsernameTextBox);
            this.Controls.Add(this.StartGameButton);
            this.Controls.Add(this.JoinGameButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.astroDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.astroDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource astroDataSetBindingSource;
        private AstroDataSet astroDataSet;
        private System.Windows.Forms.Button JoinGameButton;
        private System.Windows.Forms.Button StartGameButton;
        private System.Windows.Forms.TextBox PlayerUsernameTextBox;
        private System.Windows.Forms.Label PlayerNameInputLabel;
        private System.Windows.Forms.TextBox playerFocus;
    }
}

