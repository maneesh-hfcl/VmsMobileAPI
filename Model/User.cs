
namespace Vms.Model
{
    public partial class TUser
    {
        public int  ID {get; set;}
        public int? GROUPID {get; set;}
        public int? RIGHTSID{get; set;}
        public string? LOGIN{get; set;}
        public string? PWD{get; set;}
        public string? USRNAME{get; set;}
        public string? USRDSC {get; set;}
        public DateTime? LASTPWDCHG{get;set;}
        public string? USREML{get; set;}
        public string? USRTEL{get; set;}
        public string? USRMOB{get; set;}
        public short USRSTS{get; set;}
    }
}