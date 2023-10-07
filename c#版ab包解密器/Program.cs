using System;
using System.IO;

namespace GF2Decrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            // 批量解密
            Console.WriteLine("请输入资源文件目录:");
            foreach (string file in Directory.GetFiles(Console.ReadLine().Replace("\"", ""), "*.bundle"))
            {
                string name = Path.GetFileName(file);
                Console.WriteLine("正在处理 " + name);
                File.WriteAllBytes("output\\" + name, Decrypt(File.ReadAllBytes(file)));
            }

            return;

            // 单个解密
            while (true)
            {
                Console.WriteLine("请输入文件路径:");
                string file = Console.ReadLine().Replace("\"", "");
                File.WriteAllBytes("output\\" + Path.GetFileName(file), Decrypt(File.ReadAllBytes(file)));
                Console.WriteLine("解密成功,按下任意键继续解密!");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static byte[] xor(byte[] a, byte[] b)
        {
            int size = Math.Min(a.Length, b.Length);
            byte[] result = new byte[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = (byte)(a[i] ^ b[i]);
            }
            return result;
        }

        static byte[] Decrypt(byte[] data)
        {
            byte[] key = xor(data, new byte[] { 0x55, 0x6E, 0x69, 0x74, 0x79, 0x46, 0x53, 0x00, 0x00, 0x00, 0x00, 0x07, 0x35, 0x2E, 0x78, 0x2E });
            for (int i = 0; i < Math.Min(0x1000 * 8, data.Length); i++)
            {
                data[i] = (byte)(data[i] ^ key[i % key.Length]);
            }
            return data;
        }

    }
}
