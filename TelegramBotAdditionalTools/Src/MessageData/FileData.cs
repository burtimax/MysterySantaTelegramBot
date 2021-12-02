using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using File = Telegram.Bot.Types.File;

namespace TelegramBotAdditionalTools.Src.MessageData
{
    public class FileData
    {
        public File Info { get; set; }
        
        private MemoryStream _stream = null;
        public MemoryStream Stream
        {
            get
            {
                //if (_stream == null)
                //{
                //    _stream = new MemoryStream();
                //}
                return _stream;
            }
            private set { _stream = value; }
        }

        public byte[] Data
        {
            set
            {
                this._stream = new MemoryStream(value);
            }
            get { return this.Stream.ToArray(); }
        }

        /// <summary>
        /// Сохранить файл.
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveFile(string filePath)
        {
            if(Stream == null || Stream.Length == 0)
            {
                throw new Exception("File data is NULL");
            }
            
            if (System.IO.Directory.Exists(filePath.Substring(0, filePath.LastIndexOf(Path.DirectorySeparatorChar))) == false)
            {
                throw new Exception("FIle Directory is NOT EXISTS");
            }

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }


            System.IO.File.WriteAllBytesAsync(filePath, this.Data);
        }


    }
}
