using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpError
{
    /// <summary>
    /// Class repersents htttp error object.
    /// </summary>
    /// <remarks>
    /// Implements interface IComparable.
    /// </remarks>
    public class HttpError : IComparable
    {
        /// <summary>
        /// Store http error code.
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Store http error description.
        /// </summary>
        public string ErrorDescription { get; set; }
        /// <summary>
        /// Store http error occurence date.
        /// </summary>
        public DateTime ErrorDate { get; set; }
        /// <summary>
        /// The class default constructor.
        /// </summary>
        public HttpError() { }
        /// <summary>
        /// The class constructor with parameters.
        /// </summary>
        /// <param name="code"> error code </param>
        /// <param name="description"> error description </param>
        /// <param name="date"> error occurence date </param>
        public HttpError(int code, string description, DateTime date)
        {
            ErrorCode = code;
            ErrorDescription = description;
            ErrorDate = date;
        }
        /// <summary>
        /// Compare two objects based on error code.
        /// </summary>
        /// <param name="obj">object to compare against</param>
        /// <returns>
        /// -1 if obj error code is less than this error code
        /// 0 if error codes are equal
        /// 1 if obj error code is greater than this error code
        /// </returns>
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
        /// <summary>
        /// Check properties equality
        /// </summary>
        /// <param name="obj">object to check against</param>
        /// <returns>
        /// true if properties are equal
        /// false if not
        /// </returns>
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
            if (ErrorCode == otherError.ErrorCode
                 && ErrorDescription == otherError.ErrorDescription
                 && ErrorDate == otherError.ErrorDate)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Create string based on object properties
        /// </summary>
        public override string ToString()
        {
            return ErrorCode.ToString() + " " + ErrorDescription + " "
                + ErrorDate.ToString();
        }
        /// <summary>
        /// Generate instance hashcode
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
