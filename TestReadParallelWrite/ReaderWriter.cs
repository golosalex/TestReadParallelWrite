using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReadParallelWrite
{
    public class ReaderWriter
    {
        public static Random rnd = new Random();
        public readonly int ReadTimeSec;
        public readonly int WriteTimeSec;

        public readonly int QuestChunks;
        private readonly ConsoleColor _color;
        public ReaderWriter(int readTimeSec, int writeTimeSec, int questChunks, int color)
        {
            ReadTimeSec = readTimeSec;
            WriteTimeSec = writeTimeSec;
            QuestChunks = questChunks;
            _color = (ConsoleColor)color;
        }

        internal async Task Read(int chunkNum)
        {

            ConsoleExt.WriteLine($"start reading chunk # {chunkNum}",_color);
            await Task.Delay((int)(ReadTimeSec * 1000 * rnd.NextDouble()));// имитация долгой работы
            ConsoleExt.WriteLine($"end reading chunk # {chunkNum}",_color);

        }
        internal async Task Write(int chunkNum)
        {
            ConsoleExt.WriteLine($"start writing chunk # {chunkNum}", _color);
            await Task.Delay((int)(WriteTimeSec * 1000 * rnd.NextDouble()));// имитация долгой работы
            ConsoleExt.WriteLine($"end writing chunk # {chunkNum}", _color);
        }

        internal async Task Quest() // метод ответсвенен за управление вызовами read/write с сохранением порядка.
        {
            await Read(0);
            for (int i = 1; i < QuestChunks; i++)
            {
                var readTask = Read(i);         //запускаем таску чтения очередного чанка
                var writeTask = Write(i);       //запускаем таску записи прошлого чанка
                await Task.WhenAll(readTask, writeTask);// не даем ни читать ни писать, пока не завершится запись прошлого чанка и чтение текущего
            }
            await Write(QuestChunks-1);
        }
    }
}
