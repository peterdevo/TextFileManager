using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManagerLibrary
{
    public class FileManager
    {
        public string Text { get; set; }
        public List<string> Words { get; set; }

        public void ReadFile(string input)
        {
            bool exists = File.Exists(input);

            if (Path.GetExtension(input) != ".txt")
            {
                throw new InvalidOperationException("Incorrect file type");
            }

            if (exists)
            {
                Text = File.ReadAllText(input);
                SplitText(Text);
            }
        }

        private void SplitText(string text)
        {
            Words = text.Split(' ').Select(x => x.Trim(',', '.', '-', '?', '!')).ToList();
        }
    }
}
