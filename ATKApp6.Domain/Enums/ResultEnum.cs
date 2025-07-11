using System.Runtime.Serialization;

namespace ATKApp6.Domain.Enums
{
    public enum ResultEnum
    {
        [EnumMember(Value = "Отказано")]
        Rejected = 0,
        [EnumMember(Value = "Утверждено")]
        Approved = 1,
    }
}
