using System.Runtime.Serialization;

namespace ATKApp6.Domain.Enums
{
    public enum Municipalities
    {
        [EnumMember(Value = "ATK-ХМАО")]
        NoMunicipality = 0,                             // for atk_khmao

        [EnumMember(Value = "Белоярский район")]
        BeloyarskiyRayon = 1,

        [EnumMember(Value = "Березовский район")]
        BerezovskiyRayon = 2,

        [EnumMember(Value = "Кондинский район")]
        CondinskiyRayon = 3,

        [EnumMember(Value = "Нефтеюганский район")]
        NefteyuganskiyRayon = 4,

        [EnumMember(Value = "Нижневартовский район")]
        NizhnevartovskiyRayon = 5,

        [EnumMember(Value = "Октябрьский район")]
        OktyabrskiyRayon = 6,

        [EnumMember(Value = "Советский район")]
        SovietskiyRayon = 7,

        [EnumMember(Value = "Сургутский район")]
        SurgutskiyRayon = 8,

        [EnumMember(Value = "Ханты-Мансийский район")]
        KhantyMansiyskiyRayon = 9,

        [EnumMember(Value = "г. Когалым")]
        Kogalym = 10,

        [EnumMember(Value = "г. Лангепас")]
        Langepas = 11,

        [EnumMember(Value = "г. Мегион")]
        Megion = 12,

        [EnumMember(Value = "г. Нефтеюганск")]
        Nefteyugansk = 13,

        [EnumMember(Value = "г. Нижневартовск")]
        Nizhnevartovsk = 14,

        [EnumMember(Value = "г. Нягань")]
        Nyagan = 15,

        [EnumMember(Value = "г. Покачи")]
        Pokachi = 16,

        [EnumMember(Value = "г. Пыть-Ях")]
        PythYach = 17,

        [EnumMember(Value = "г. Радужный")]
        Raduzhnyi = 18,

        [EnumMember(Value = "г. Сургут")]
        Surgut = 19,

        [EnumMember(Value = "г. Урай")]
        Urai = 20,

        [EnumMember(Value = "г. Ханты-Мансийск")]
        KhantyMansiysk = 21,

        [EnumMember(Value = "г. Югорск")]
        Yugorsk = 22,
    }
}