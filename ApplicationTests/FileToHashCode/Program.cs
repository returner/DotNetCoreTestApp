using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileToHashCode
{
    class Program
    {
        static void Main(string[] args)
        {
            // images
            var files = Directory.GetFiles(@".\files\");
            var videoFiles = Directory.GetFiles(@".\video\");
            Console.WriteLine("SHA256 FileStream Images");
            for (int i = 0; i < files.OrderBy(d => d).Count(); i++)
            {
                var fileName = Path.GetFileName(files[i]);
                var sha256 = GetSha256FileStream(files[i]);
                Console.WriteLine($"{fileName} ({sha256.Size}kb / {sha256.ElapsedTimeMs}ms) : {sha256.HashCode}");
            }
            Console.WriteLine();
            Console.WriteLine("SHA256 BufferedStream Images");
            for (int i = 0; i < files.OrderBy(d => d).Count(); i++)
            {
                var fileName = Path.GetFileName(files[i]);
                var sha256 = GetSha256BufferedStream(files[i]);
                Console.WriteLine($"{fileName} ({sha256.Size}kb / {sha256.ElapsedTimeMs}ms) : {sha256.HashCode}");
            }
            Console.WriteLine();
            Console.WriteLine("MD5 FileStream Images");
            for (int i = 0; i < files.OrderBy(d => d).Count(); i++)
            {
                var fileName = Path.GetFileName(files[i]);
                var sha256 = GetMD5FileStream(files[i]);
                Console.WriteLine($"{fileName} ({sha256.Size}kb / {sha256.ElapsedTimeMs}ms) : {sha256.HashCode}");
            }
            Console.WriteLine();
            Console.WriteLine("MD5 BufferedStream Images");
            for (int i = 0; i < files.OrderBy(d => d).Count(); i++)
            {
                var fileName = Path.GetFileName(files[i]);
                var sha256 = GetMD5BufferedStream(files[i]);
                Console.WriteLine($"{fileName} ({sha256.Size}kb / {sha256.ElapsedTimeMs}ms) : {sha256.HashCode}");
            }
            
            Console.ReadKey();
        }

        private static HashResult GetSha256FileStream(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            var fileLength = fileInfo.Length / 1024;
            var watch = new Stopwatch();
            watch.Start();

            using var sha256 = SHA256Managed.Create();
            using var stream = new FileStream(filePath, FileMode.Open);
            stream.Position = 0;
            var hashValue = sha256.ComputeHash(stream);
            var text = BitConverter.ToString(hashValue).Replace("-",string.Empty);
            stream.Close();
            
            watch.Stop();
            return new HashResult { Size = fileLength, HashCode = text, ElapsedTimeMs = watch.ElapsedMilliseconds};
        }
        private static HashResult GetSha256BufferedStream(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            var fileLength = fileInfo.Length / 1024;

            var watch = new Stopwatch();
            watch.Start();

            using var sha256 = SHA256Managed.Create();
            using var stream = new BufferedStream(File.OpenRead(filePath), 1200000);
            stream.Position = 0;
            var hashValue = sha256.ComputeHash(stream);
            var text = BitConverter.ToString(hashValue).Replace("-", string.Empty);
            stream.Close();
            watch.Stop();
            return new HashResult { Size = fileLength, HashCode = text, ElapsedTimeMs = watch.ElapsedMilliseconds };
        }
        private static HashResult GetMD5FileStream(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            var fileLength = fileInfo.Length / 1024;

            var watch = new Stopwatch();
            watch.Start();

            using var md5 = MD5.Create();
            using var stream = new FileStream(filePath, FileMode.Open);
            var hash = md5.ComputeHash(stream);

            watch.Stop();
            return new HashResult { Size = fileLength, HashCode = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant(), ElapsedTimeMs = watch.ElapsedMilliseconds };
        }
        private static HashResult GetMD5BufferedStream(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            var fileLength = fileInfo.Length / 1024;

            var watch = new Stopwatch();
            watch.Start();

            using var md5 = MD5.Create();
            using var stream = new BufferedStream(File.OpenRead(filePath), 1200000);
            var hash = md5.ComputeHash(stream);
            
            watch.Stop();
            return new HashResult { Size = fileLength, HashCode = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant(), ElapsedTimeMs = watch.ElapsedMilliseconds };
        }
    }

    public class HashResult
    { 
        public long Size { get; set; }
        public string HashCode { get; set; }
        public long ElapsedTimeMs { get; set; }
    }
}
