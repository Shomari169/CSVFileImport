using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CSVFileImport
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populateDatabaseData();
                lblMessage.Text = "Current Database Data.";
            }
        }

        private void populateDatabaseData()
        {
            using (MuDatabaseEntities dc = new MuDatabaseEntities())
            {
                gvData.DataSource = dc.CustomerMasters.ToList();
                gvData.DataBind();
            }
        }


        protected void btnImportFromCSV_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile.ContentType == "text/csv" || FileUpload1.PostedFile.ContentType == "application/vnd.ms-excel")
            {
                string fileName = Path.Combine(Server.MapPath("~/UploadDocuments"), Guid.NewGuid().ToString() + ".csv");
                try
                {
                    FileUpload1.PostedFile.SaveAs(fileName);

                    string[] Lines = File.ReadAllLines(fileName);
                    string[] Fields;

                    //Remove Header line
                    Lines = Lines.Skip(1).ToArray();
                    List<CustomerMaster> emList = new List<CustomerMaster>();
                    foreach (var line in Lines)
                    {
                        Fields = line.Split(new char[] { ',' });
                        emList.Add(
                            new CustomerMaster
                            {
                                AccountID = Fields[0].Replace("\"", ""), // removed "" 
                                CustomerName = Fields[1].Replace("\"", ""),
                                AccountNumber = Convert.ToInt32(Fields[2].Replace("\"", "")),
                                DateOfPurchase = Convert.ToDateTime(Fields[3].Replace("\"", "")),
                                CustomerAddress = Fields[4].Replace("\"", ""),
                                TransactionValues = Fields[5].Replace("\"", ""),
                            });
                    }

                    // Update database data
                    using (MuDatabaseEntities dc = new MuDatabaseEntities())
                    {
                        foreach (var i in emList)
                        {
                            var v = dc.CustomerMasters.Where(a => a.AccountID.Equals(i.AccountID)).FirstOrDefault();
                            if (v != null)
                            {
                                v.AccountID = i.AccountID;
                                v.CustomerName = i.CustomerName;
                                v.AccountNumber = i.AccountNumber;
                                v.DateOfPurchase = i.DateOfPurchase;
                                v.CustomerAddress = i.CustomerAddress;
                                v.TransactionValues = i.TransactionValues;
                            }
                            else
                            {
                                dc.CustomerMasters.Add(i);
                            }
                        }

                        dc.SaveChanges();

                        // populate updated data 
                        populateDatabaseData();
                        lblMessage.Text = "Successfully Done. Now upto data is following.....";
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        protected void btnExportToCSV_Click(object sender, EventArgs e)
        {
            List<CustomerMaster> emList = new List<CustomerMaster>();
            using (MuDatabaseEntities dc = new MuDatabaseEntities())
            {
                emList = dc.CustomerMasters.ToList();
            }

            if (emList.Count > 0)
            {
                string header = @"""Account ID"",""CCustomer Name"",""Account Number"",""Date Of Purchase"",""Customer Address"",""Transaction Values""";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(header);

                foreach (var i in emList)
                {
                    sb.AppendLine(string.Join(",",
                        string.Format(@"""{0}""", i.AccountID),
                        string.Format(@"""{0}""", i.CustomerName),
                        string.Format(@"""{0}""", i.AccountNumber),
                        string.Format(@"""{0}""", i.DateOfPurchase),
                        string.Format(@"""{0}""", i.CustomerAddress),
                        string.Format(@"""{0}""", i.TransactionValues)));
                }

                // Download Here

                HttpContext context = HttpContext.Current;
                context.Response.Write(sb.ToString());
                context.Response.ContentType = "text/csv";
                context.Response.AddHeader("Content-Disposition", "attachment; filename=CustomerData.csv");
                context.Response.End();
            }
            else
            {
                lblMessage.Text = "Data not Found!";
            }
        }

        
        

        
    }
    
}