
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.healthLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.astroDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.astroDataSetBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.JoinGameButton.Location = new System.Drawing.Point(23, 69);
            this.JoinGameButton.Name = "JoinGameButton";
            this.JoinGameButton.Size = new System.Drawing.Size(75, 23);
            this.JoinGameButton.TabIndex = 1;
            this.JoinGameButton.Text = "Join";
            this.JoinGameButton.UseVisualStyleBackColor = true;
            this.JoinGameButton.Click += new System.EventHandler(this.JoinGameButton_Click);
            // 
            // StartGameButton
            // 
            this.StartGameButton.Location = new System.Drawing.Point(23, 69);
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
            this.PlayerUsernameTextBox.Location = new System.Drawing.Point(23, 43);
            this.PlayerUsernameTextBox.MaxLength = 10;
            this.PlayerUsernameTextBox.Name = "PlayerUsernameTextBox";
            this.PlayerUsernameTextBox.Size = new System.Drawing.Size(156, 20);
            this.PlayerUsernameTextBox.TabIndex = 3;
            // 
            // PlayerNameInputLabel
            // 
            this.PlayerNameInputLabel.AutoSize = true;
            this.PlayerNameInputLabel.Location = new System.Drawing.Point(20, 8);
            this.PlayerNameInputLabel.Name = "PlayerNameInputLabel";
            this.PlayerNameInputLabel.Size = new System.Drawing.Size(91, 13);
            this.PlayerNameInputLabel.TabIndex = 4;
            this.PlayerNameInputLabel.Text = "Player username: ";
            // 
            // playerFocus
            // 
            this.playerFocus.Location = new System.Drawing.Point(-1000, -1000);
            this.playerFocus.Name = "playerFocus";
            this.playerFocus.ReadOnly = true;
            this.playerFocus.Size = new System.Drawing.Size(0, 20);
            this.playerFocus.TabIndex = 5;
            this.playerFocus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.playerFocus_KeyDown);
            this.playerFocus.KeyUp += new System.Windows.Forms.KeyEventHandler(this.playerFocus_KeyUp);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.healthLabel);
            this.panel1.Controls.Add(this.StartGameButton);
            this.panel1.Controls.Add(this.JoinGameButton);
            this.panel1.Controls.Add(this.PlayerNameInputLabel);
            this.panel1.Controls.Add(this.PlayerUsernameTextBox);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(20);
            this.panel1.Size = new System.Drawing.Size(804, 603);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // healthLabel
            // 
            this.healthLabel.AutoSize = true;
            this.healthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.healthLabel.Location = new System.Drawing.Point(345, 577);
            this.healthLabel.Name = "healthLabel";
            this.healthLabel.Size = new System.Drawing.Size(107, 25);
            this.healthLabel.TabIndex = 5;
            this.healthLabel.Text = "Health: 0/3";
            this.healthLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(804, 601);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.playerFocus);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.astroDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.astroDataSetBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label healthLabel;
    }
}

