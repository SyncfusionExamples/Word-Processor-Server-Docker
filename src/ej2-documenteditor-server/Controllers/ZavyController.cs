using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Syncfusion.EJ2.DocumentEditor;
using System.Text;
using WDocument = Syncfusion.DocIO.DLS.WordDocument;
using WFormatType = Syncfusion.DocIO.FormatType;
using Syncfusion.EJ2.SpellChecker;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;

namespace EJ2DocumentEditorServer.Controllers
{
    [Route("api/[controller]")]
    public class ZavyController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        string path;
        public ZavyController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            path = Startup.path;
        }

        //Converts Metafile to raster image.
        private static void OnMetafileImageParsed(object sender, MetafileImageParsedEventArgs args)
        {
            //You can write your own method definition for converting metafile to raster image using any third-party image converter.
            args.ImageStream = ConvertMetafileToRasterImage(args.MetafileStream);
        }

        private static Stream ConvertMetafileToRasterImage(Stream ImageStream)
        {
            //Here we are loading a default raster image as fallback.
            Stream imgStream = GetManifestResourceStream("ImageNotFound.jpg");
            return imgStream;
            //To do : Write your own logic for converting metafile to raster image using any third-party image converter(Syncfusion doesn't provide any image converter).
        }

        private static Stream ConvertMetafileToPng(Stream ImageStream)
        {
            //Here we are loading a default raster image as fallback.
            Stream imgStream = GetManifestResourceStream("ImageNotFound.jpg");
            return imgStream;
            //To do : Write your own logic for converting metafile to raster image using any third-party image converter(Syncfusion doesn't provide any image converter).
        }

        private static Stream GetManifestResourceStream(string fileName)
        {
            System.Reflection.Assembly execAssembly = typeof(WDocument).Assembly;
            string[] resourceNames = execAssembly.GetManifestResourceNames();
            foreach (string resourceName in resourceNames)
            {
                if (resourceName.EndsWith("." + fileName))
                {
                    fileName = resourceName;
                    break;
                }
            }
            return execAssembly.GetManifestResourceStream(fileName);
        }


        // JSON parameters accepted by the endpoint
        public class ConversionParameters
        {
            // Current format (this can be auto-detected and is not required):
            public string from { get; set; }

            // Export format:
            public string export { get; set; }

            // Value to convert. Will be automatically decoded if base64 encoded:
            public string content { get; set; }
        }

        [AcceptVerbs("Post")]
        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        [Route("conversion")]
        public string Conversion([FromBody]ConversionParameters param)
        {
          if (string.IsNullOrEmpty(param.content))
          {
            throw new NotSupportedException("Content is empty.");
          }
          if (string.IsNullOrEmpty(param.export))
          {
            throw new NotSupportedException("Export format is empty.");
          }


          try
          {
              // If we need to export to sfdt, we need to convert the content to WordDocument (not WDocument from DocIO)
              if (DotPrefix(param.export) == ".sfdt")
              {
                WordDocument document = this.GetDocument(param);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(document);
                document.Dispose();
                return json;
              }
              else if (DotPrefix(param.export) == ".pdf")
              {
                MemoryStream stream = new MemoryStream();
                WDocument document = this.GetWDocument(param);
                document.Save(stream, WFormatType.Docx);
                string base64 = this.ExportPDF(document);
                document.Dispose();
                return base64;
              }
              else
              {
                WDocument document = this.GetWDocument(param);
                MemoryStream stream = new MemoryStream();
                document.Save(stream, GetWFormatType(param.export));
                document.Dispose();
                stream.Position = 0;
                return Convert.ToBase64String(stream.ToArray());
              }
          }
          catch (Exception e)
          {
              return "Oh no! " + e.Message + " format: " + param.from + " export: " + param.export;
          }
        }


        // Helpers:

        // Normalize the format string to start with a dot
        // since that's what Syncfusion expects by default
        internal static string DotPrefix(string format)
        {
          if (format.StartsWith(".")) return format.ToLower();
          return "." + format.ToLower();
        }

        // Decode base64 if it's base64, otherwise just returns the
        // string as it is
        internal static string DecodeIfBase64(string base64)
        {
          if (IsBase64(base64)) {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
          }
          return base64;
        }

        // Check if a string is base64 encoded
        internal static bool IsBase64(string base64)
        {
          Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
          return Convert.TryFromBase64String(base64, buffer , out int bytesParsed);
        }

        // Gets the FormatType from an extension string
        internal static FormatType GetFormatType(string format)
        {
            if (string.IsNullOrEmpty(format))
                throw new NotSupportedException("EJ2 DocumentEditor does not support blank input format");

            // Check if format starts with a ., otherwise add one:
            switch (DotPrefix(format))
            {
                case ".dotx":
                case ".docx":
                case ".docm":
                case ".dotm":
                    return FormatType.Docx;
                case ".dot":
                case ".doc":
                    return FormatType.Doc;
                case ".rtf":
                    return FormatType.Rtf;
                case ".txt":
                    return FormatType.Txt;
                case ".xml":
                    return FormatType.WordML;
                case ".html":
                    return FormatType.Html;
                default:
                    throw new NotSupportedException("EJ2 DocumentEditor does not support this input format (" + format + "). Supported formats: .docx, .doc, .rtf, .txt, .xml, .html and .pdf");
            }
        }
        internal static WFormatType GetWFormatType(string? format)
        {
            if (string.IsNullOrEmpty(format))
                return WFormatType.Automatic;

            // Check if format starts with a ., otherwise add one:
            switch (DotPrefix(format.ToLower()))
            {
                case ".dotx":
                    return WFormatType.Dotx;
                case ".docx":
                    return WFormatType.Docx;
                case ".docm":
                    return WFormatType.Docm;
                case ".dotm":
                    return WFormatType.Dotm;
                case ".dot":
                    return WFormatType.Dot;
                case ".doc":
                    return WFormatType.Doc;
                case ".rtf":
                    return WFormatType.Rtf;
                case ".txt":
                    return WFormatType.Txt;
                case ".xml":
                    return WFormatType.WordML;
                case ".odt":
                    return WFormatType.Odt;
                case ".html":
                    return WFormatType.Html;
                default:
                    throw new NotSupportedException("EJ2 DocumentEditor does not support this export format (" + format + "). Supported formats: .docx, .doc, .rtf, .txt, .xml, .odt, .html and .pdf");
            }
        }

        private WDocument GetWDocument(ConversionParameters param)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(DecodeIfBase64(param.content));
            MemoryStream stream = new MemoryStream(byteArray);

            // Load the HTML content into the WordDocument
            WDocument document = new WDocument(stream, GetWFormatType(param.from));
            stream.Dispose();

            return document;
        }

        private WordDocument GetDocument(ConversionParameters param)
        {
          //Hooks MetafileImageParsed event.
          WordDocument.MetafileImageParsed += OnMetafileImageParsed;
          WordDocument document = WordDocument.LoadString(DecodeIfBase64(param.content), GetFormatType(param.from));
          //Unhooks MetafileImageParsed event.
          WordDocument.MetafileImageParsed -= OnMetafileImageParsed;
          return document;
        }

        

        internal string ExportPDF(WDocument wordDocument)
        {

            // Instantiation of DocIORenderer for Word to PDF conversion.
            DocIORenderer render = new DocIORenderer();

            //Converts Word document into PDF document
            PdfDocument pdfDocument = render.ConvertToPDF(wordDocument);

            //Dispose the resources.
            render.Dispose();
            wordDocument.Dispose();

            //Saves the PDF document to MemoryStream.
            MemoryStream stream = new MemoryStream();
            pdfDocument.Save(stream);
            stream.Position = 0;
            //Dispose the PDF resources.
            pdfDocument.Close(true);
            PdfDocument.ClearFontCache();

            //Return the PDF document.
            return Convert.ToBase64String(stream.ToArray());
        }
    }

}
