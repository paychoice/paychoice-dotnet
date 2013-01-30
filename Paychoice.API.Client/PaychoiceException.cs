using Paychoice.API.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Paychoice.API.Client
{
    [Serializable]
    public class PaychoiceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaychoiceException" /> class.
        /// </summary>
        public PaychoiceException()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaychoiceException" /> class.
        /// </summary>
        public PaychoiceException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaychoiceException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public PaychoiceException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaychoiceException" /> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        public PaychoiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaychoiceException" /> class.
        /// </summary>
        /// <param name="error">The error.</param>
        internal PaychoiceException(Error error)
            : base(error.Message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaychoiceException" /> class.
        /// </summary>
        /// <param name="error">The error.</param>
        internal PaychoiceException(OAuth2Message error)
            : base(error.ErrorDescription)
        {
        }
    }
}