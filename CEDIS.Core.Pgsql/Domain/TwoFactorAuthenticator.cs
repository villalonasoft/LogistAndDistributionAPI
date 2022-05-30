using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Picking.Core.Domain
{
    public class TwoFactorAuthenticator
    {
        [Key]
        public int Id { get; set; }
        public byte[] Generator { get; set; }
        public string SecretKey { get; set; }
        public byte[] Secret { get; set; }
    }
}
