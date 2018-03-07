using System.Runtime.Serialization;

namespace GDAXSharp.Services.Orders.Models
{
    public enum OrderStatus
    {
        [EnumMember(Value = "all")]
        All,
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "active")]
        Active
    }
}
