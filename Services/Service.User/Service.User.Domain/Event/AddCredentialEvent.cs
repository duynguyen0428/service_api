using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.User.Domain
{
    public class AddCredentialEvent : Infrastructure.Domain.Event
    {
        public string LABEL { get; set; }
        public string MOBILE_NO { get; set; }
        public int PIN { get; set; }
        public string DATA { get; set; }
        public AddCredentialEvent(Guid messageid,string label, string mobile_no, int pin, string data): base(messageid)
        {
            LABEL = label;
            MOBILE_NO = mobile_no;
            PIN = pin;
            DATA = data;
        }
    }
}
