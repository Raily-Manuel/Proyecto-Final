namespace Sistema_Cuadre_TotalEnergies
{
    partial class Menu_Reportes
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.gasolineraDBDataSet = new Sistema_Cuadre_TotalEnergies.GasolineraDBDataSet();
            this.spReporteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sp_ReporteTableAdapter = new Sistema_Cuadre_TotalEnergies.GasolineraDBDataSetTableAdapters.sp_ReporteTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gasolineraDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.spReporteBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sistema_Cuadre_TotalEnergies.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            //this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(742, 410);
            this.reportViewer1.TabIndex = 0;
            // 
            // gasolineraDBDataSet
            // 
            this.gasolineraDBDataSet.DataSetName = "GasolineraDBDataSet";
            this.gasolineraDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // spReporteBindingSource
            // 
            this.spReporteBindingSource.DataMember = "sp_Reporte";
            this.spReporteBindingSource.DataSource = this.gasolineraDBDataSet;
            // 
            // sp_ReporteTableAdapter
            // 
            this.sp_ReporteTableAdapter.ClearBeforeFill = true;
            // 
            // Menu_Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 410);
            this.Controls.Add(this.reportViewer1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Menu_Reportes";
            this.Text = "Reportes";
            this.Load += new System.EventHandler(this.Menu_Reportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gasolineraDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource spReporteBindingSource;
        private GasolineraDBDataSet gasolineraDBDataSet;
        private GasolineraDBDataSetTableAdapters.sp_ReporteTableAdapter sp_ReporteTableAdapter;
    }
}