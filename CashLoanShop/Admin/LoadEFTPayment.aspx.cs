using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop.Admin
{
    public partial class LoadEFTPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ReadCsvFile();
                Session["Dt"] = dt;
                dgvFileData.DataSource = dt;
                dgvFileData.DataBind();
                if (dt.Rows.Count > 0)
                {
                    btnSubmit.Visible = true;
                }
                else
                {
                    btnSubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblerror.Text = ex.Message;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (TermLoanService ts = new TermLoanService())
                {
                    if (Session["Dt"] != null)
                    {
                        DataTable dt = (DataTable)Session["Dt"];
                        ts.LoadEFTPayment(dt);
                        lblerror.Text = "File data uploaded Successfully";
                        dt = null;
                        Session["Dt"] = null;
                        dgvFileData.DataSource = dt;
                        dgvFileData.DataBind();
                        btnSubmit.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblerror.Text = ex.Message;
            }

        }
        public DataTable ReadCsvFile()
        {

            DataTable dtCsv = new DataTable();
            string Fulltext;
            if (fupFile.HasFile)
            {
                string FileSaveWithPath = Server.MapPath("\\EFTFiles\\Import_" + fupFile.FileName + "_" + System.DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".csv");
                fupFile.SaveAs(FileSaveWithPath);
                using (StreamReader sr = new StreamReader(FileSaveWithPath))
                {
                    while (!sr.EndOfStream)
                    {
                        Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                        string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                        for (int i = 0; i <= rows.Count() - 1; i++)
                        {
                            string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                            {
                                if (string.IsNullOrEmpty(rowValues[0])) break;
                                if (i == 0)
                                {
                                    for (int j = 0; j < rowValues.Count(); j++)
                                    {
                                        dtCsv.Columns.Add(rowValues[j].Trim()); //add headers  
                                    }
                                }
                                else
                                {
                                    DataRow dr = dtCsv.NewRow();
                                    for (int k = 0; k < rowValues.Count(); k++)
                                    {
                                        dr[k] = rowValues[k].ToString().Trim();
                                    }
                                    dtCsv.Rows.Add(dr); //add other rows  
                                }
                            }
                        }
                    }
                }
            }
            return dtCsv;
        }
    }
}