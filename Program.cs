using System;
using System.IO;
using System.Text;

class WasmGenerator
{
    static void Main()
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("\nCriando ficheiro main.wasm...\n");

        using (FileStream fs = new FileStream("main.wasm", FileMode.Create, FileAccess.Write))
        using (BinaryWriter writer = new BinaryWriter(fs))
        {
            // Cabeçalho
            writer.Write(new byte[] { 0x00, (byte)'a', (byte)'s', (byte)'m' });
            writer.Write(new byte[] { 0x01, 0x00, 0x00, 0x00 });

            // Seção de tipos
            writer.Write((byte)0x01);         // section id: Type
            writer.Write((byte)0x07);         // section size
            writer.Write((byte)0x01);         // one type
            writer.Write((byte)0x60);         // func type
            writer.Write((byte)0x02);         // two parameters
            writer.Write((byte)0x7F);         // i32
            writer.Write((byte)0x7F);         // i32
            writer.Write((byte)0x01);         // one result
            writer.Write((byte)0x7F);         // i32

            // Seção de funções
            writer.Write((byte)0x03);         // section id: Function
            writer.Write((byte)0x02);         // section size
            writer.Write((byte)0x01);         // one function
            writer.Write((byte)0x00);         // type index 0

            // Seção de exportações
            writer.Write((byte)0x07);         // section id: Export
            writer.Write((byte)0x07);         // section size
            writer.Write((byte)0x01);         // one export
            writer.Write((byte)0x03);         // string length
            writer.Write(Encoding.ASCII.GetBytes("sum")); // export name
            writer.Write((byte)0x00);         // export kind: function
            writer.Write((byte)0x00);         // function index

            // Seção de código
            writer.Write((byte)0x0A);         // section id: Code
            writer.Write((byte)0x09);         // section size
            writer.Write((byte)0x01);         // one function
            writer.Write((byte)0x07);         // body size
            writer.Write((byte)0x00);         // local decl count
            writer.Write(new byte[] {
                0x20, 0x00,       // local.get 0
                0x20, 0x01,       // local.get 1
                0x6A,             // i32.add
                0x0B              // end
            });
        }

        Console.WriteLine("Ficheiro main.wasm criado com sucesso!");
    }
}
