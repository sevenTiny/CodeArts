using Syncfusion.PdfToImageConverter;
using Tesseract;

class Program
{
    static void Main(string[] args)
    {
        var path = Environment.CurrentDirectory;
        string pdfPath = Path.Combine(path, "test_invoice.pdf");

        // 语言包可以从这里下载
        // https://github.com/tesseract-ocr/tessdata_best?tab=readme-ov-file
        string tessDataPath = Path.Combine(path, @"tessdata"); // 包含eng.traineddata等语言包

        try
        {
            //Initialize PDF to Image converter.
            PdfToImageConverter imageConverter = new();
            //Load the PDF document as a stream
            FileStream inputStream = new(pdfPath, FileMode.Open, FileAccess.ReadWrite);
            imageConverter.Load(inputStream);
            //Convert PDF to Image.
            Stream outputStream = imageConverter.Convert(0, false, false);
            MemoryStream stream = outputStream as MemoryStream;
            byte[] bytes = stream.ToArray();

            // 这个生成出来的图片有水印
            // https://help.syncfusion.com/document-processing/pdf/conversions/pdf-to-image/net/convert-pdf-file-to-image-in-asp-net-core?tabcontent=visual-studio
            //using (FileStream output = new FileStream("output.png", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            //{
            //    output.Write(bytes, 0, bytes.Length);
            //}

            // 使用 Tesseract OCR 从 MemoryStream 中读取图像
            // 这里csi_sim要和语言包一致
            using var engine = new TesseractEngine(tessDataPath, "chi_sim", EngineMode.Default);
            using var img = Pix.LoadFromMemory(bytes);
            using var pageOcr = engine.Process(img);
            string text = pageOcr.GetText();
            Console.WriteLine($"识别结果：");
            Console.WriteLine(text);
            // 这里可以添加代码来解析text，提取所需信息
            // 例如，使用正则表达式查找发票号码、日期等
        }
        catch (Exception e)
        {
            Console.WriteLine("发生异常：" + e.Message);
        }
    }
}