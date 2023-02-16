
namespace Vms.Model
{
    public class TEvtLog
    {
        public long ID{get; set;}
        public DateTime? EVTIME{get; set;}
        public short? EVTYPE{get; set;}
        public short? EVSTATE{get; set;}
        public short? DEVCAT{get; set;}
        public short? DEVTYPE{get; set;}
        public string? OBJIDS{get; set;}
        public int? USRASG{get; set;}
        public int? OBJIDN{get; set;}
        public int? MMCID{get; set;}
        public string? MSGSPC{get; set;}
        public int? OPRUID{get; set;}
        public int? OPRSID{get; set;}
        public int? ALMPRC{get; set;}
        public DateTime? TMOPR{get; set;}
        public int? PRCSTS{get; set;}
        public int? EVPRTY{get;set;}
    }
}