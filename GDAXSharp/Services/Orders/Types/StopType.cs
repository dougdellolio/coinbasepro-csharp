using System.Runtime.Serialization;

namespace GDAXSharp.Services.Orders.Types
{
    public enum StopType
    {
        [EnumMember(Value = "loss")]
        Loss,
        [EnumMember(Value = "entry")]
        Entry
    }
}
