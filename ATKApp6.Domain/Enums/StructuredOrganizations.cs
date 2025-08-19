using System.Runtime.Serialization;

namespace ATKApp6.Domain.Enums
{
    public enum StructuredOrganizations
    {
        [EnumMember(Value = "АТК ХМАО-Югры")]
        ATK_KHMAO = 0,

        [EnumMember(Value = "АТК Белоярский район")]
        ATK_BeloyarskiyRayon = 1,                                               //ATK_khanty_mansiysk

        [EnumMember(Value = "АТК Березовский район")]
        ATK_BerezovskiyRayon = 2,

        [EnumMember(Value = "АТК Кондинский район")]
        ATK_CondinskiyRayon = 3,

        [EnumMember(Value = "АТК Нефтеюганский район")]
        ATK_NefteyuganskiyRayon = 4,

        [EnumMember(Value = "АТК Нижневартовский район")]
        ATK_NizhnevartovskiyRayon = 5,

        [EnumMember(Value = "АТК Октябрьский район")]
        ATK_OktyabrskiyRayon = 6,

        [EnumMember(Value = "АТК Советский район")]
        ATK_SovietskiyRayon = 7,

        [EnumMember(Value = "АТК Сургутский район")]
        ATK_SurgutskiyRayon = 8,

        [EnumMember(Value = "АТК Ханты-Мансийский район")]
        ATK_KhantyMansiyskiyRayon = 9,

        [EnumMember(Value = "АТК Когалым район")]
        ATK_Kogalym = 10,

        [EnumMember(Value = "АТК г. Лангепас")]
        ATK_Langepas = 11,

        [EnumMember(Value = "АТК г. Мегион")]
        ATK_Megion = 12,

        [EnumMember(Value = "АТК г. Нефтеюганск")]
        ATK_Nefteyugansk = 13,

        [EnumMember(Value = "АТК г. Нижневартовск")]
        ATK_Nizhnevartovsk = 14,

        [EnumMember(Value = "АТК г. Нягань")]
        ATK_Nyagan = 15,

        [EnumMember(Value = "АТК г. Покачи")]
        ATK_Pokachi = 16,

        [EnumMember(Value = "АТК г. Пыть-Ях")]
        ATK_PythYach = 17,

        [EnumMember(Value = "АТК г. Радужный")]
        ATK_Raduzhnyi = 18,

        [EnumMember(Value = "АТК г. Сургут")]
        ATK_Surgut = 19,

        [EnumMember(Value = "АТК г. Урай")]
        ATK_Urai = 20,

        [EnumMember(Value = "АТК г. Ханты-Мансийск")]
        ATK_KhantyMansiysk = 21,

        [EnumMember(Value = "АТК г. Югорск")]
        ATK_Yugorsk = 22,



        // Белоярский район Департаменты
        [EnumMember(Value = "Департамент культуры Белоярского района")]
        BeloyarskiyRayon_Culture = 23,

        [EnumMember(Value = "Департамент молодежной политики Белоярского района")]
        BeloyarskiyRayon_Young = 24,

        [EnumMember(Value = "Департамент образования и науки Белоярского района")]
        BeloyarskiyRayon_Education = 25,

        [EnumMember(Value = "Департамент физической культуры и спорта Белоярского района")]
        BeloyarskiyRayon_Sport = 26,

        [EnumMember(Value = "Департамент социального развития Белоярского района")]
        BeloyarskiyRayon_Social = 27,



        // Березовский район Департаменты
        [EnumMember(Value = "Департамент культуры Березовского района")]
        BerezovskiyRayon_Culture = 28,

        [EnumMember(Value = "Департамент молодежной политики Березовского района")]
        BerezovskiyRayon_Young = 29,

        [EnumMember(Value = "Департамент образования и науки Березовского района")]
        BerezovskiyRayon_Education = 30,

        [EnumMember(Value = "Департамент физической культуры и спорта Березовского района")]
        BerezovskiyRayon_Sport = 31,

        [EnumMember(Value = "Департамент социального развития Березовского района")]
        BerezovskiyRayon_Social = 32,



        // Кондинский район Департаменты
        [EnumMember(Value = "Департамент культуры Кондинского района")]
        CondinskiyRayon_Culture = 33,

        [EnumMember(Value = "Департамент молодежной политики Кондинского района")]
        CondinskiyRayon_Young = 34,

        [EnumMember(Value = "Департамент образования и науки Кондинского района")]
        CondinskiyRayon_Education = 35,

        [EnumMember(Value = "Департамент физической культуры и спорта Кондинского района")]
        CondinskiyRayon_Sport = 36,

        [EnumMember(Value = "Департамент социального развития Кондинского района")]
        CondinskiyRayon_Social = 37,



        // Нефтеюганский район Департаменты
        [EnumMember(Value = "Департамент культуры Нефтеюганского района")]
        NefteyuganskiyRayon_Culture = 38,

        [EnumMember(Value = "Департамент молодежной политики Нефтеюганского района")]
        NefteyuganskiyRayon_Young = 39,

        [EnumMember(Value = "Департамент образования и науки Нефтеюганского района")]
        NefteyuganskiyRayon_Education = 40,

        [EnumMember(Value = "Департамент физической культуры и спорта Нефтеюганского района")]
        NefteyuganskiyRayon_Sport = 41,

        [EnumMember(Value = "Департамент социального развития Нефтеюганского района")]
        NefteyuganskiyRayon_Social = 42,



        // Нижневартовский район Департаменты
        [EnumMember(Value = "Департамент культуры Нижневартовского района")]
        NizhnevartovskiyRayon_Culture = 43,

        [EnumMember(Value = "Департамент молодежной политики Нижневартовского района")]
        NizhnevartovskiyRayon_Young = 44,

        [EnumMember(Value = "Департамент образования и науки Нижневартовского района")]
        NizhnevartovskiyRayon_Education = 45,

        [EnumMember(Value = "Департамент физической культуры и спорта Нижневартовского района")]
        NizhnevartovskiyRayon_Sport = 46,

        [EnumMember(Value = "Департамент социального развития Нижневартовского района")]
        NizhnevartovskiyRayon_Social = 47,



        // Октябрьский район Департаменты
        [EnumMember(Value = "Департамент культуры Октябрьского района")]
        OktyabrskiyRayon_Culture = 48,

        [EnumMember(Value = "Департамент молодежной политики Октябрьского района")]
        OktyabrskiyRayon_Young = 49,

        [EnumMember(Value = "Департамент образования и науки Октябрьского района")]
        OktyabrskiyRayon_Education = 50,

        [EnumMember(Value = "Департамент физической культуры и спорта Октябрьского района")]
        OktyabrskiyRayon_Sport = 51,

        [EnumMember(Value = "Департамент социального развития Октябрьского района")]
        OktyabrskiyRayon_Social = 52,



        // Советский район Департаменты
        [EnumMember(Value = "Департамент культуры Советского района")]
        SovietskiyRayon_Culture = 53,

        [EnumMember(Value = "Департамент молодежной политики Советского района")]
        SovietskiyRayon_Young = 54,

        [EnumMember(Value = "Департамент образования и науки Советского района")]
        SovietskiyRayon_Education = 55,

        [EnumMember(Value = "Департамент физической культуры и спорта Советского района")]
        SovietskiyRayon_Sport = 56,

        [EnumMember(Value = "Департамент социального развития Советского района")]
        SovietskiyRayon_Social = 57,



        // Сургутский район Департаменты
        [EnumMember(Value = "Департамент культуры Сургутского района")]
        SurgutskiyRayon_Culture = 58,

        [EnumMember(Value = "Департамент молодежной политики Сургутского района")]
        SurgutskiyRayon_Young = 59,

        [EnumMember(Value = "Департамент образования и науки Сургутского района")]
        SurgutskiyRayon_Education = 60,

        [EnumMember(Value = "Департамент физической культуры и спорта Сургутского района")]
        SurgutskiyRayon_Sport = 61,

        [EnumMember(Value = "Департамент социального развития Сургутского района")]
        SurgutskiyRayon_Social = 62,



        // Ханты-Мансийский район Департаменты
        [EnumMember(Value = "Департамент культуры Ханты-Мансийского района")]
        KhantyMansiyskiyRayon_Culture = 63,

        [EnumMember(Value = "Департамент молодежной политики Ханты-Мансийского района")]
        KhantyMansiyskiyRayon_Young = 64,

        [EnumMember(Value = "Департамент образования и науки Ханты-Мансийского района")]
        KhantyMansiyskiyRayon_Education = 65,

        [EnumMember(Value = "Департамент физической культуры и спорта Ханты-Мансийского района")]
        KhantyMansiyskiyRayon_Sport = 66,

        [EnumMember(Value = "Департамент социального развития Ханты-Мансийского района")]
        KhantyMansiyskiyRayon_Social = 67,



        // Когалым Департаменты
        [EnumMember(Value = "Департамент культуры г. Когалым")]
        Kogalym_Culture = 68,

        [EnumMember(Value = "Департамент молодежной политики г. Когалым")]
        Kogalym_Young = 69,

        [EnumMember(Value = "Департамент образования и науки г. Когалым")]
        Kogalym_Education = 70,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Когалым")]
        Kogalym_Sport = 71,

        [EnumMember(Value = "Департамент социального развития г. Когалым")]
        Kogalym_Social = 72,



        // Лангепас Департаменты
        [EnumMember(Value = "Департамент культуры г. Лангепас")]
        Langepas_Culture = 73,

        [EnumMember(Value = "Департамент молодежной политики г. Лангепас")]
        Langepas_Young = 74,

        [EnumMember(Value = "Департамент образования и науки г. Лангепас")]
        Langepas_Education = 75,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Лангепас")]
        Langepas_Sport = 76,

        [EnumMember(Value = "Департамент социального развития г. Лангепас")]
        Langepas_Social = 77,



        // Мегион Департаменты
        [EnumMember(Value = "Департамент культуры г. Мегион")]
        Megion_Culture = 78,

        [EnumMember(Value = "Департамент молодежной политики г. Мегион")]
        Megion_Young = 79,

        [EnumMember(Value = "Департамент образования и науки г. Мегион")]
        Megion_Education = 80,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Мегион")]
        Megion_Sport = 81,

        [EnumMember(Value = "Департамент социального развития г. Мегион")]
        Megion_Social = 82,



        // Нефтеюганск Департаменты
        [EnumMember(Value = "Департамент культуры г. Нефтеюганск")]
        Nefteyugansk_Culture = 83,

        [EnumMember(Value = "Департамент молодежной политики г. Нефтеюганск")]
        Nefteyugansk_Young = 84,

        [EnumMember(Value = "Департамент образования и науки г. Нефтеюганск")]
        Nefteyugansk_Education = 85,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Нефтеюганск")]
        Nefteyugansk_Sport = 86,

        [EnumMember(Value = "Департамент социального развития г. Нефтеюганск")]
        Nefteyugansk_Social = 87,



        // Нижневартовск Департаменты
        [EnumMember(Value = "Департамент культуры г. Нижневартовск")]
        Nizhnevartovsk_Culture = 88,

        [EnumMember(Value = "Департамент молодежной политики г. Нижневартовск")]
        Nizhnevartovsk_Young = 89,

        [EnumMember(Value = "Департамент образования и науки г. Нижневартовск")]
        Nizhnevartovsk_Education = 90,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Нижневартовск")]
        Nizhnevartovsk_Sport = 91,

        [EnumMember(Value = "Департамент социального развития г. Нижневартовск")]
        Nizhnevartovsk_Social = 92,



        // Нягань Департаменты
        [EnumMember(Value = "Департамент культуры г. Нягань")]
        Nyagan_Culture = 93,

        [EnumMember(Value = "Департамент молодежной политики г. Нягань")]
        Nyagan_Young = 94,

        [EnumMember(Value = "Департамент образования и науки г. Нягань")]
        Nyagan_Education = 95,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Нягань")]
        Nyagan_Sport = 96,

        [EnumMember(Value = "Департамент социального развития г. Нягань")]
        Nyagan_Social = 97,



        // Покачи Департаменты
        [EnumMember(Value = "Департамент культуры г. Покачи")]
        Pokachi_Culture = 98,

        [EnumMember(Value = "Департамент молодежной политики г. Покачи")]
        Pokachi_Young = 99,

        [EnumMember(Value = "Департамент образования и науки г. Покачи")]
        Pokachi_Education = 100,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Покачи")]
        Pokachi_Sport = 101,

        [EnumMember(Value = "Департамент социального развития г. Покачи")]
        Pokachi_Social = 102,



        // Пыть-Ях Департаменты
        [EnumMember(Value = "Департамент культуры г. Пыть-Ях")]
        PythYach_Culture = 103,
        
        [EnumMember(Value = "Департамент молодежной политики г. Пыть-Ях")]
        PythYach_Young = 104,

        [EnumMember(Value = "Департамент образования и науки г. Пыть-Ях")]
        PythYach_Education = 105,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Пыть-Ях")]
        PythYach_Sport = 106,

        [EnumMember(Value = "Департамент социального развития г. Пыть-Ях")]
        PythYach_Social = 107,



        // Радужный Департаменты
        [EnumMember(Value = "Департамент культуры г. Радужный")]
        Raduzhnyi_Culture = 108,

        [EnumMember(Value = "Департамент молодежной политики г. Радужный")]
        Raduzhnyi_Young = 109,

        [EnumMember(Value = "Департамент образования и науки г. Радужный")]
        Raduzhnyi_Education = 110,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Радужный")]
        Raduzhnyi_Sport = 111,

        [EnumMember(Value = "Департамент социального развития г. Радужный")]
        Raduzhnyi_Social = 112,



        // Сургут Департаменты
        [EnumMember(Value = "Департамент культуры г. Сургут")]
        Surgut_Culture = 113,

        [EnumMember(Value = "Департамент молодежной политики г. Сургут")]
        Surgut_Young = 114,

        [EnumMember(Value = "Департамент образования и науки  г. Сургут")]
        Surgut_Education = 115,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Сургут")]
        Surgut_Sport = 116,

        [EnumMember(Value = "Департамент социального развития г. Сургут")]
        Surgut_Social = 117,



        // Урай Департаменты
        [EnumMember(Value = "Департамент культуры г. Урай")]
        Urai_Culture = 118,

        [EnumMember(Value = "Департамент молодежной политики г. Урай")]
        Urai_Young = 119,

        [EnumMember(Value = "Департамент образования и науки г. Урай")]
        Urai_Education = 120,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Урай")]
        Urai_Sport = 121,

        [EnumMember(Value = "Департамент социального развития г. Урай")]
        Urai_Social = 122,



        // Ханты-Мансийск Департаменты
        [EnumMember(Value = "Департамент культуры г. Ханты-Мансийск")]
        KhantyMansiysk_Culture = 123,

        [EnumMember(Value = "Департамент молодежной политики г. Ханты-Мансийск")]
        KhantyMansiysk_Young = 124,

        [EnumMember(Value = "Департамент образования и науки г. Ханты-Мансийск")]
        KhantyMansiysk_Education = 125,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Ханты-Мансийск")]
        KhantyMansiysk_Sport = 126,

        [EnumMember(Value = "Департамент социального развития г. Ханты-Мансийск")]
        KhantyMansiysk_Social = 127,



        // Югорск Департаменты
        [EnumMember(Value = "Департамент культуры г. Югорск")]
        Yugorsk_Culture = 128,

        [EnumMember(Value = "Департамент молодежной политики г. Югорск")]
        Yugorsk_Young = 129,

        [EnumMember(Value = "Департамент образования и науки г. Югорск")]
        Yugorsk_Education = 130,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Югорск")]
        Yugorsk_Sport = 131,

        [EnumMember(Value = "Департамент социального развития г. Югорск")]
        Yugorsk_Social = 132,
    }
}