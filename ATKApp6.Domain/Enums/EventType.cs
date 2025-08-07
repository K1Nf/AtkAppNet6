using System.Runtime.Serialization;

namespace ATKApp6.Domain.Enums
{
    public enum EventType
    {
        [EnumMember(Value = "Лекция")] 
        Lecture,
        [EnumMember(Value = "Квиз")] 
        Quiz, 
        [EnumMember(Value = "Акция")] 
        Action, 
        [EnumMember(Value = "Игра")] 
        Game,
        [EnumMember(Value = "Квест")]
        Quest,
        [EnumMember(Value = "Организация выставки")]
        Exhibition,
        [EnumMember(Value = "Проведение экскурсии")]
        Excursion,
        [EnumMember(Value = "Другое")] 
        Other
    }
}
