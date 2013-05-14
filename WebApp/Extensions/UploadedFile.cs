namespace OpenCat
{
    using System.IO;

    public class UploadedFile
    {
        public UploadedFile(string fileName, Stream inputStream)
        {
            FileName = fileName;
            InputStream = inputStream;
        }

        public string FileName { get; private set; }
        public Stream InputStream { get; private set; }


        public void CopyTo(Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = InputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public void SaveAs(string path)
        {
            using (var writer = File.OpenWrite(path))
            {
                CopyTo(writer);
            }
        }
    }
}