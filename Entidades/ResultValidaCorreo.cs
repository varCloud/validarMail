using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [DataContract]
    public class ResultValidaCorreo
    {
        [DataMember]
        public string result { get; set; }
        [DataMember]
        public string reason { get; set; }
        [DataMember]
        public string disposable { get; set; }
        [DataMember]
        public string accept_all { get; set; }
        [DataMember]
        public string role { get; set; }
        [DataMember]
        public string free { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string user { get; set; }
        [DataMember]
        public string domain { get; set; }
        [DataMember]
        public string mx_record { get; set; }
        [DataMember]
        public string mx_domain { get; set; }
        [DataMember]
        public string safe_to_send { get; set; }
        [DataMember]
        public string did_you_mean { get; set; }
        [DataMember]
        public string success { get; set; }
        [DataMember]
        public string message { get; set; }
    }
}
