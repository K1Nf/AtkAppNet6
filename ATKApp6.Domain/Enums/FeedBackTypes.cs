using System.Runtime.Serialization;

namespace ATKApp6.Domain.Enums
{
    public enum FeedBackTypes
    {
        [EnumMember(Value = "Анкетирование")]
        Guestionnaire,
        [EnumMember(Value = "Онлайн-опрос")]
        Internet,
        [EnumMember(Value = "Опрос")]
        Opros,
        [EnumMember(Value = "Интервью")]
        Interview,
        [EnumMember(Value = "Другое")]
        Other,
    }
}
