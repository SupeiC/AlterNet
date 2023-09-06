using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    public class CustomTextWriter : TextWriter
    {
        private Action<string> _action = null;

        public CustomTextWriter()
        {

        }

        public void SetAction(Action<string> action)
        {
            _action = action;
        }

        public void write(String str)
        {
            if (str.Equals("\n"))
                return;

            Write(str);
        }

        public void writelines(String[] str)
        {
            foreach (String line in str)
            {
                WriteLine(str);
            }
        }

        public void flush()
        {
            Flush();
        }

        public void close()
        {
            Close();
        }

        public override void Write(string value)
        {
            base.Write(value);
            _action?.Invoke(value);
        }

        public override void WriteLine(string value)
        {
            base.WriteLine(value + "\n");
            _action?.Invoke(value + "\n");
        }

        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
