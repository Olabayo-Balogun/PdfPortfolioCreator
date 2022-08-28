using foxit.common;
using foxit.pdf;
using foxit.pdf.objects;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using foxit.common.fxcrt;
using System;
using System.Reflection;
using Image = foxit.common.Image;
using System.IO;

namespace PdfPortfolioCreator
{
    public partial class PdfPortfolioCreatorForm : Form
    {
        public static PDFDoc PdfPortfolioDocument;
        public string FilePath = "";
        public static PDFPage Page;
        public static int width;
        public static int height;
        public static PortfolioFolderNode root_folder;
        public static Portfolio portfolio = Portfolio.CreatePortfolio();
        public static PortfolioNode root_node;
        string message;
        string title;
        int ClickCount;
        //For the purpose of this demo, the keys are used within the application.
        //In a real world application, they should be treated as secret keys and protected accordingl
        //Also note that the keys I'm using below are trial keys and will expire after a period of time.
        //You're encouraged to register for the trial or paid version of Foxit PDF SDK as you'll get newer keys.
        string sn = "SXC5wsuLUEUO4OurtXCk18JnGcxp6hrx5I1X8TbpPYqwzwQ3vWxOkg==";
        string key = "8f0YFUGMvRkN+ddwhrBxubRv+38Gz2ILMFDAylGsl9gEt83edTI+7gO6BL3Zwz4kg8iFbUBbTA0Fo1AkxlPp2d40Z8iMd8SHfgzBN4RXdnt8UAB+qfAI8OrWhPtzxO+yOivYHoRYeiFttZWqwI4X1fwdUGO37hYJVsD/EdiOZqAjvZBeI95cx5fY0CtsvH+NaMagwb6HwBNDT0dI45XNR9ycMEboU+Ia211DtwOplnZ25dOssAtBPNk21UxQnmZMQEwfr//15ssa7/OkxfZDJY1AlID9Vq8StY3iJCSn4q+t0dSoJkYM/lhZNkkn7OGPTjjx/WJ0yXN1SISFvrHgVbnLhwtVUnJ7wCz91NLIISUTmWRJRcEXu7CQmS4+QjDhBbtKAyG4urX9uNzkkn2Xbk/QN1D5Id6fYOrLxVwmjllIW7JASUgxpBRbt3qxYUuIZGxtu009yTB/NATboZS2FMngZD556sCMsT7Xb1JQwqyQ2WBRSm31qWi8g87e3MewMGaNW5T0P/NEqU619EX0RUiedSCm6ISqCeGXKqw1jJ4K56293J31qvn1DfwePzHg3O+Ga2eqHqERaNbCqtwGHpGo/DAa/r9qhipsO/W8BGfjDzRs9zvKvnjwIkoOoI/4HiQapyFG6F6rDW/C78gubmNjASjK9C9NtJ12F/qohlErie4gd8gik+L12bfrZPXs8j9pMNHFbzT50zYuRv0qt/f38goLxjKZkVS2V5do9n1ehlVf0Sq/EXzz+XX9xIjh6fL/0DykoTOXm8i/J40BOZ1e+0sDkI0vrYw6AvnFz1AKB6p4ROQyFUdmt5pRNlWRTMYxgrFf+jZ6g6LkmTd+d0+MT2MX45R2xKWF3JIiyrtZXxXplMur7dKe9B8B7rrJ8klvOnJzP8Buj4fHjxp8EFbBvscng1H5wdYrxB/PlWmH4ZY0iPn5zSP8Uauvy2osIRk/LadTjSSi3+z1XY7MkGai86eiu2EjQtlnnnEz0XWaguOJueVPErt7xGDgihqOeptG5nMwNi/S98/UX63lW/r9UNR40tN/1gGEropGpfHvDuviLWn8ANieWcxxlyL1U8D02lPoND/kfNBEsQY02ZWB+pZ2Qk3pLBuYL+YvkFabCyBXYVCp6Mjj7DYgg8jRPdtuxK5X9i4LveA9ViQI2tQcMfCsxS0YUf8bLxa4Yp7xJKfosFBACGSUU12XNaOEp9IrTkaXaqi+KTKUy5SI2JN5zD6Wjid43oDpi16eV39dLVlrRWI5MITj8rBrxg0CWyTLOwJ/um2vIqH1K9br27x0L5paRTxc9N7WIZci1gnxC8Jzjs5qwg==";


        public PdfPortfolioCreatorForm()
        {
            InitializeComponent();

        }


        private void AddFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                Form imagesFlowLayoutPanel = new Form();
                openFileDialog.AddExtension = true;
                openFileDialog.Multiselect = true;
                openFileDialog.Title = "Select portfolio files";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    int fileCount = openFileDialog.FileNames.Count();
                    
                    ErrorCode error_code = Library.Initialize(sn, key);
                    if (error_code != ErrorCode.e_ErrSuccess)
                    {
                        message = "Unable to verify your SDK activation key";
                        title = "Select Portfolio Files Status";
                        MessageBox.Show(message, title);
                        Library.Release();
                    }
                    else
                    {
                        // Read the files
                        foreach (String file in openFileDialog.FileNames)
                        {
                            if (error_code != ErrorCode.e_ErrSuccess)
                            {
                                Library.Release();
                            }
                            root_node = portfolio.GetRootNode();                            
                            root_folder = new PortfolioFolderNode(root_node);
                            PortfolioNodeArray sub_nodes = root_folder.GetSortedSubNodes();
                            PortfolioFileNode file_node = root_folder.AddFile(file);
                            FileSpec file_spec = file_node.GetFileSpec();
                            bool status = root_node.IsEmpty();
                            
//                            using (PortfolioNode root_node = portfolio.GetRootNode())
//{
//                                using (PortfolioFolderNode root_folder = new PortfolioFolderNode(root_node))
//                                {
//                                    using (PortfolioNodeArray sub_nodes = root_folder.GetSortedSubNodes())
//                                    {
//                                    for (uint index = 0; index < sub_nodes.GetSize(); index++)
//                                        {
//                                            using (PortfolioNode root_node = sub_nodes.GetAt(index))
//                                            {
//                                                switch (root_node.GetNodeType())
//                                                {
//                                                    case PortfolioNode.Type.e_TypeFolder:
//                                                        {
//                                                            using (PortfolioFolderNode folder_node = new PortfolioFolderNode(root_node))
//                                                            {
//                                                                // Use PortfolioFolderNode's getting method to get some properties.
//                                                             PortfolioNodeArray sub_nodes_2 = folder_node.GetSortedSubNodes();
//                                                             break;
//                                                            }
//                                                        }
//                                                    case PortfolioNode.Type.e_TypeFile:
//                                                        {
//                                                            using (PortfolioFileNode file_node = new PortfolioFileNode(root_node))
//                                                            {
//                                                                // Get file specification object from this file root_node, and then get/set information from/to this file specification object.
//                                                            using (FileSpec file_spec = file_node.GetFileSpec())
//                                                                {
//                                                                    break;
//                                                                }
//                                                            }
//                                                        }
//                                                }
//                                            }
//                                        }
//                                    }
//                                }
//                            }
                        }
                        message = $"{fileCount} files added successfully!";
                        title = "Select Portfolio Files Status";
                        MessageBox.Show(message, title);
                    }
                    
                    
                }
                
            }
            catch (Exception ex)
            {
                message = "An error occured, please contact your tech support";
                title = "Select Portfolio Files Status";
                MessageBox.Show(message, title);
                throw;
            }
        }

        private void CreatePortfolio_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog saveFileDialog = new FolderBrowserDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ErrorCode error_code = Library.Initialize(sn, key);
                    ClickCount++;
                    string path;
                    if (ClickCount - 1 < 1)
                    {
                        path = $"{saveFileDialog.SelectedPath}\\PortfolioDocument.pdf";
                        //if (portfolio.GetPortfolioPDFDoc().GetFileSize() < 1)
                        //{
                        //    message = $"Please add files to your portfolio";
                        //    title = "No Portfolio File Selected";
                        //    MessageBox.Show(message, title);
                        //}
                        //else
                        {
                            PdfPortfolioDocument = portfolio.GetPortfolioPDFDoc();
                            PdfPortfolioDocument.SaveAs(path, (int)PDFDoc.SaveFlags.e_SaveFlagNoOriginal);
                            message = $"PDF portfolio created! ";
                            title = "Create PDF Portfolio Status";
                            MessageBox.Show(message, title);
                        }
                    }
                    else
                    {
                        path = saveFileDialog.SelectedPath;
                        PDFDoc portfolio_pdf_doc = new PDFDoc(path);
                        if (portfolio_pdf_doc.GetFileSize() < 1)
                        {
                            message = $"Please add files to your portfolio";
                            title = "No Portfolio File Selected";
                            MessageBox.Show(message, title);
                        }
                        else if (!portfolio_pdf_doc.IsPortfolio())
                        {
                            message = $"Please add files to your portfolio";
                            title = "No Portfolio File Selected";
                            MessageBox.Show(message, title);
                        }
                        else if (portfolio_pdf_doc.IsPortfolio())
                        {
                            Portfolio existed_portfolio = Portfolio.CreatePortfolio(portfolio_pdf_doc);
                            PdfPortfolioDocument = existed_portfolio.GetPortfolioPDFDoc();
                            ClickCount++;
                            PdfPortfolioDocument.SaveAs(path, (int)PDFDoc.SaveFlags.e_SaveFlagNoOriginal);
                            message = $"PDF portfolio created! ";
                            title = "Create PDF Portfolio Status";
                            MessageBox.Show(message, title);
                        }
                    }
                }
            }                
            catch (Exception ex)
            {
                string message = $"An error occured, please contact your tech support";
                string title = "Create PDF Portfolio Status";
                MessageBox.Show(message, title);
                throw;
            }
            
        }

    }
}