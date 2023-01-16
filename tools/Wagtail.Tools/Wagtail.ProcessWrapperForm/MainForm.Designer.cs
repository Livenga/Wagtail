namespace Wagtail.ProcessWrapperForm
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.processLayout = new System.Windows.Forms.TableLayoutPanel();
            this.processLabel = new System.Windows.Forms.Label();
            this.processText = new System.Windows.Forms.TextBox();
            this.argsLabel = new System.Windows.Forms.Label();
            this.argsText = new System.Windows.Forms.TextBox();
            this.executeButton = new System.Windows.Forms.Button();
            this.processLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // processLayout
            // 
            this.processLayout.ColumnCount = 2;
            this.processLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.processLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.processLayout.Controls.Add(this.processLabel, 0, 0);
            this.processLayout.Controls.Add(this.processText, 1, 0);
            this.processLayout.Controls.Add(this.argsLabel, 0, 1);
            this.processLayout.Controls.Add(this.argsText, 1, 1);
            this.processLayout.Location = new System.Drawing.Point(12, 12);
            this.processLayout.Name = "processLayout";
            this.processLayout.RowCount = 2;
            this.processLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.processLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.processLayout.Size = new System.Drawing.Size(592, 65);
            this.processLayout.TabIndex = 0;
            // 
            // processLabel
            // 
            this.processLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processLabel.AutoSize = true;
            this.processLabel.Location = new System.Drawing.Point(3, 0);
            this.processLabel.Name = "processLabel";
            this.processLabel.Size = new System.Drawing.Size(101, 32);
            this.processLabel.TabIndex = 0;
            this.processLabel.Text = "プロセスパス(&P)";
            this.processLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // processText
            // 
            this.processText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processText.Location = new System.Drawing.Point(110, 3);
            this.processText.Name = "processText";
            this.processText.Size = new System.Drawing.Size(479, 25);
            this.processText.TabIndex = 1;
            // 
            // argsLabel
            // 
            this.argsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.argsLabel.AutoSize = true;
            this.argsLabel.Location = new System.Drawing.Point(3, 32);
            this.argsLabel.Name = "argsLabel";
            this.argsLabel.Size = new System.Drawing.Size(101, 33);
            this.argsLabel.TabIndex = 2;
            this.argsLabel.Text = "引数(&A)";
            this.argsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // argsText
            // 
            this.argsText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.argsText.Location = new System.Drawing.Point(110, 35);
            this.argsText.Name = "argsText";
            this.argsText.Size = new System.Drawing.Size(479, 25);
            this.argsText.TabIndex = 3;
            // 
            // executeButton
            // 
            this.executeButton.Location = new System.Drawing.Point(504, 83);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(100, 28);
            this.executeButton.TabIndex = 1;
            this.executeButton.Text = "実行(&E)";
            this.executeButton.UseVisualStyleBackColor = true;
            this.executeButton.Click += new System.EventHandler(this.OnExecuteClick);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(614, 119);
            this.Controls.Add(this.executeButton);
            this.Controls.Add(this.processLayout);
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "MainForm";
            this.Text = "Wagtail.ProcessWrapperForm";
            this.Shown += new System.EventHandler(this.OnShown);
            this.processLayout.ResumeLayout(false);
            this.processLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel processLayout;
        private System.Windows.Forms.Label processLabel;
        private System.Windows.Forms.TextBox processText;
        private System.Windows.Forms.Label argsLabel;
        private System.Windows.Forms.TextBox argsText;
        private System.Windows.Forms.Button executeButton;
    }
}

