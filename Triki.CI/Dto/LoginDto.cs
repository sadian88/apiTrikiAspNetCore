using System;
using System.Collections.Generic;
using System.Text;

namespace Triki.CI.Dto
{
    public class LoginDto
    {
        /// <summary>
        ///     Tipo de token a utilizarse
        /// </summary>
        public string AuthenticationType { get; set; }

        /// <summary>
        ///     Valor del token para validar
        /// </summary>
        public string Token { get; set; }
    }
}
