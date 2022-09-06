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
        public static PortfolioFolderNode RootFolder;
        //It's important we initialize the portfolio as early as possible to creates a blank portfolio that we can attach files to.
        public static Portfolio portfolio = Portfolio.CreatePortfolio();
        public static PortfolioNode RootNode;
        string message;
        string title;
        int clickCount;

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
                    
                    ErrorCode errorCode = Library.Initialize(sn, key);
                    if (errorCode != ErrorCode.e_ErrSuccess)
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
                            if (errorCode != ErrorCode.e_ErrSuccess)
                            {
                                Library.Release();
                            }
                            //The first step in adding files to the porfolio is to get the root of the portoflio to which we then add files
                            RootNode = portfolio.GetRootNode();                            
                            //Next up with the code below we get create a root folder for the portfolio where we house the root note
                            RootFolder = new PortfolioFolderNode(RootNode);
                            //We use the subNodes to have all nodes arranged
                            PortfolioNodeArray subNodes = RootFolder.GetSortedSubNodes();
                            //Below we're then able to add individual files (through their file path) to the root folder of the portfolio.
                            PortfolioFileNode fileNode = RootFolder.AddFile(file);
                            //With fileSpec we can get more context with regards to the nature of the file.
                            FileSpec fileSpec = fileNode.GetFileSpec();
                            //The status below helps us know if we were able to successfully add a file to the node
                            bool status = RootNode.IsEmpty();
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
                //With saveFileDialog below, we can choose the location to which we save the portfolio
                FolderBrowserDialog saveFileDialog = new FolderBrowserDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ErrorCode errorCode = Library.Initialize(sn, key);
                    clickCount++;
                    string path;
                    //As a personal convention I feel it would be nice to have a separate result for each time we click the "Create Portfolio" button. 
                    //I'm using "clickCount" to hold the number of times we click the "Create Portfolio" document.
                    if (clickCount - 1 < 1)
                    {
                        path = $"{saveFileDialog.SelectedPath}\\PortfolioDocument.pdf";
                        PdfPortfolioDocument = portfolio.GetPortfolioPDFDoc();
                        PdfPortfolioDocument.SaveAs(path, (int)PDFDoc.SaveFlags.e_SaveFlagNoOriginal);

                        DisplayFirstPPage(PdfPortfolioDocument);
                        message = $"PDF portfolio saved to {path}";
                        title = "Create PDF Portfolio Status";
                        MessageBox.Show(message, title);
                    }
                    else
                    {
                        path = $"{saveFileDialog.SelectedPath}\\PortfolioDocument({clickCount}).pdf";
                        PDFDoc portfolio_pdf_doc = new PDFDoc(path);
                        Portfolio existed_portfolio = Portfolio.CreatePortfolio(portfolio_pdf_doc);
                        PdfPortfolioDocument = existed_portfolio.GetPortfolioPDFDoc();
                        PdfPortfolioDocument.SaveAs(path, (int)PDFDoc.SaveFlags.e_SaveFlagNoOriginal);

                        DisplayFirstPPage(PdfPortfolioDocument);
                        message = $"PDF portfolio saved to {path}";
                        title = "Create PDF Portfolio Status";
                        MessageBox.Show(message, title);                       
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

        private void DisplayFirstPPage(PDFDoc pdfPortfolioDocument)
        {
            //The purpose of this method is to render the first page of the selected PDF portfolio in the windows form 
            Page = pdfPortfolioDocument.GetPage(0);
            Page.StartParse((int)PDFPage.ParseFlags.e_ParsePageNormal, null, false);
            width = (int)(Page.GetWidth());
            height = (int)(Page.GetHeight());

            Matrix2D matrix = Page.GetDisplayMatrix(0, 0, width, height, Page.GetRotation());

            // Prepare a bitmap for rendering.
            foxit.common.Bitmap bitmap = new foxit.common.Bitmap(width, height, foxit.common.Bitmap.DIBFormat.e_DIBRgb32);
            System.Drawing.Bitmap sbitmap = bitmap.GetSystemBitmap();
            Graphics draw = Graphics.FromImage(sbitmap);
            draw.Clear(System.Drawing.Color.White);

            // Render page
            Renderer render = new Renderer(bitmap, false);
            render.StartRender(Page, matrix, null);

            // Add the bitmap to image and save the image.
            foxit.common.Image image = new foxit.common.Image();
            string imgPath = $"{FilePath.Split('.').First()}IndexPage.jpg";
            image.AddFrame(bitmap);
            image.SaveAs(imgPath);
            DocumentDisplay.Image = System.Drawing.Image.FromFile(imgPath);
        }
    }
}