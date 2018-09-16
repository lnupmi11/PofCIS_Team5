using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpError
{
    public class HttpError : IComparable
    {
        public int ErrorCode { get; set; }
        public string ErrorDescription { get; set; }

        public int CompareTo(object obj)
        {
            HttpError ohterError = obj as HttpError;
            if (ohterError != null)
            {
                return this.ErrorCode.CompareTo(ohterError.ErrorCode);
            }
            else
            {
                throw new ArgumentException("Object is not an HttpError");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            HttpError otherError = obj as HttpError;
            if( otherError == null)
            {
                return false;
            }
            if (ErrorCode == otherError.ErrorCode)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return ErrorCode.ToString() + " " + ErrorDescription;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
