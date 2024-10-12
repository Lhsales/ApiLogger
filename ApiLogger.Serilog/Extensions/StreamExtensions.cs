namespace ApiLogger.Serilog
{
    internal static class StreamExtensions
    {
        public static string? ReadAsString(this Stream stream)
        {
            int bufferLenght = 4096;

            stream.Seek(0, SeekOrigin.Begin);
            string result;
            using (var textWriter = new StringWriter())
            using (var reader = new StreamReader(stream))
            {
                var readChunk = new char[bufferLenght];
                int readChunkLength;

                do
                {
                    readChunkLength = reader.ReadBlock(readChunk, 0, bufferLenght);
                    textWriter.Write(readChunk, 0, readChunkLength);
                } while (readChunkLength > 0);

                result = textWriter.ToString();
            }

            return result.FormatJson();
        }
    }
}
