using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastZep3
{
    public class BinaryObject
    {
        private string bin1 = "";
        private string bin2 = "";

        public BinaryObject(string bin1, string bin2)
        {
            this.bin1 = bin1;
            this.bin2 = bin2;
        }
        public string Id
        {
            get
            {
                return this.bin1;
            }
        }
        public string Value
        {
            get
            {
                return this.bin2;
            }
        }
        public string toString()
        {
            return this.Value;
        }
        public override string ToString()
        {
            return this.Id;
        }
    }
}
