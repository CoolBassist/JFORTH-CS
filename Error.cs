using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForthInterpreter
{
    enum ErrorType { UNKNOWN_SYMBOL, SYNTAX_ERROR};
    class Error
    {
        private ErrorType type;
        private int pos;
        private string info;
        public Error(ErrorType type, int pos)
        {
            this.type = type;
            this.pos = pos;
        }
        public Error(ErrorType type, int pos, string info)
        {
            this.type = type;
            this.pos = pos;
            this.info = info;
        }

        public string prettyPrint()
        {
            switch (type)
            {
                case ErrorType.UNKNOWN_SYMBOL:
                    return $"Unknown symbol '{info}' at column {pos+1}";
                case ErrorType.SYNTAX_ERROR:
                    return $"Syntax error at column {pos+1} (are there enough numbers on the stack?)";
                default:
                    return "Generic error";
            }
        }
    }
}
