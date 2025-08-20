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
        BeloyarskiyRayon_dep_Culture = 23,

        [EnumMember(Value = "Департамент молодежной политики Белоярского района")]
        BeloyarskiyRayon_dep_Young = 24,

        [EnumMember(Value = "Департамент образования и науки Белоярского района")]
        BeloyarskiyRayon_dep_Education = 25,

        [EnumMember(Value = "Департамент физической культуры и спорта Белоярского района")]
        BeloyarskiyRayon_dep_Sport = 26,

        [EnumMember(Value = "Департамент социального развития Белоярского района")]
        BeloyarskiyRayon_dep_Social = 27,



        // Березовский район Департаменты
        [EnumMember(Value = "Департамент культуры Березовского района")]
        BerezovskiyRayon_dep_Culture = 28,

        [EnumMember(Value = "Департамент молодежной политики Березовского района")]
        BerezovskiyRayon_dep_Young = 29,

        [EnumMember(Value = "Департамент образования и науки Березовского района")]
        BerezovskiyRayon_dep_Education = 30,

        [EnumMember(Value = "Департамент физической культуры и спорта Березовского района")]
        BerezovskiyRayon_dep_Sport = 31,

        [EnumMember(Value = "Департамент социального развития Березовского района")]
        BerezovskiyRayon_dep_Social = 32,



        // Кондинский район Департаменты
        [EnumMember(Value = "Департамент культуры Кондинского района")]
        CondinskiyRayon_dep_Culture = 33,

        [EnumMember(Value = "Департамент молодежной политики Кондинского района")]
        CondinskiyRayon_dep_Young = 34,

        [EnumMember(Value = "Департамент образования и науки Кондинского района")]
        CondinskiyRayon_dep_Education = 35,

        [EnumMember(Value = "Департамент физической культуры и спорта Кондинского района")]
        CondinskiyRayon_dep_Sport = 36,

        [EnumMember(Value = "Департамент социального развития Кондинского района")]
        CondinskiyRayon_dep_Social = 37,



        // Нефтеюганский район Департаменты
        [EnumMember(Value = "Департамент культуры Нефтеюганского района")]
        NefteyuganskiyRayon_dep_Culture = 38,

        [EnumMember(Value = "Департамент молодежной политики Нефтеюганского района")]
        NefteyuganskiyRayon_dep_Young = 39,

        [EnumMember(Value = "Департамент образования и науки Нефтеюганского района")]
        NefteyuganskiyRayon_dep_Education = 40,

        [EnumMember(Value = "Департамент физической культуры и спорта Нефтеюганского района")]
        NefteyuganskiyRayon_dep_Sport = 41,

        [EnumMember(Value = "Департамент социального развития Нефтеюганского района")]
        NefteyuganskiyRayon_dep_Social = 42,



        // Нижневартовский район Департаменты
        [EnumMember(Value = "Департамент культуры Нижневартовского района")]
        NizhnevartovskiyRayon_dep_Culture = 43,

        [EnumMember(Value = "Департамент молодежной политики Нижневартовского района")]
        NizhnevartovskiyRayon_dep_Young = 44,

        [EnumMember(Value = "Департамент образования и науки Нижневартовского района")]
        NizhnevartovskiyRayon_dep_Education = 45,

        [EnumMember(Value = "Департамент физической культуры и спорта Нижневартовского района")]
        NizhnevartovskiyRayon_dep_Sport = 46,

        [EnumMember(Value = "Департамент социального развития Нижневартовского района")]
        NizhnevartovskiyRayon_dep_Social = 47,



        // Октябрьский район Департаменты
        [EnumMember(Value = "Департамент культуры Октябрьского района")]
        OktyabrskiyRayon_dep_Culture = 48,

        [EnumMember(Value = "Департамент молодежной политики Октябрьского района")]
        OktyabrskiyRayon_dep_Young = 49,

        [EnumMember(Value = "Департамент образования и науки Октябрьского района")]
        OktyabrskiyRayon_dep_Education = 50,

        [EnumMember(Value = "Департамент физической культуры и спорта Октябрьского района")]
        OktyabrskiyRayon_dep_Sport = 51,

        [EnumMember(Value = "Департамент социального развития Октябрьского района")]
        OktyabrskiyRayon_dep_Social = 52,



        // Советский район Департаменты
        [EnumMember(Value = "Департамент культуры Советского района")]
        SovietskiyRayon_dep_Culture = 53,

        [EnumMember(Value = "Департамент молодежной политики Советского района")]
        SovietskiyRayon_dep_Young = 54,

        [EnumMember(Value = "Департамент образования и науки Советского района")]
        SovietskiyRayon_dep_Education = 55,

        [EnumMember(Value = "Департамент физической культуры и спорта Советского района")]
        SovietskiyRayon_dep_Sport = 56,

        [EnumMember(Value = "Департамент социального развития Советского района")]
        SovietskiyRayon_dep_Social = 57,



        // Сургутский район Департаменты
        [EnumMember(Value = "Департамент культуры Сургутского района")]
        SurgutskiyRayon_dep_Culture = 58,

        [EnumMember(Value = "Департамент молодежной политики Сургутского района")]
        SurgutskiyRayon_dep_Young = 59,

        [EnumMember(Value = "Департамент образования и науки Сургутского района")]
        SurgutskiyRayon_dep_Education = 60,

        [EnumMember(Value = "Департамент физической культуры и спорта Сургутского района")]
        SurgutskiyRayon_dep_Sport = 61,

        [EnumMember(Value = "Департамент социального развития Сургутского района")]
        SurgutskiyRayon_dep_Social = 62,



        // Ханты-Мансийский район Департаменты
        [EnumMember(Value = "Департамент культуры Ханты-Мансийского района")]
        KhantyMansiyskiyRayon_dep_Culture = 63,

        [EnumMember(Value = "Департамент молодежной политики Ханты-Мансийского района")]
        KhantyMansiyskiyRayon_dep_Young = 64,

        [EnumMember(Value = "Департамент образования и науки Ханты-Мансийского района")]
        KhantyMansiyskiyRayon_dep_Education = 65,

        [EnumMember(Value = "Департамент физической культуры и спорта Ханты-Мансийского района")]
        KhantyMansiyskiyRayon_dep_Sport = 66,

        [EnumMember(Value = "Департамент социального развития Ханты-Мансийского района")]
        KhantyMansiyskiyRayon_dep_Social = 67,



        // Когалым Департаменты
        [EnumMember(Value = "Департамент культуры г. Когалым")]
        Kogalym_dep_Culture = 68,

        [EnumMember(Value = "Департамент молодежной политики г. Когалым")]
        Kogalym_dep_Young = 69,

        [EnumMember(Value = "Департамент образования и науки г. Когалым")]
        Kogalym_dep_Education = 70,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Когалым")]
        Kogalym_dep_Sport = 71,

        [EnumMember(Value = "Департамент социального развития г. Когалым")]
        Kogalym_dep_Social = 72,



        // Лангепас Департаменты
        [EnumMember(Value = "Департамент культуры г. Лангепас")]
        Langepas_dep_Culture = 73,

        [EnumMember(Value = "Департамент молодежной политики г. Лангепас")]
        Langepas_dep_Young = 74,

        [EnumMember(Value = "Департамент образования и науки г. Лангепас")]
        Langepas_dep_Education = 75,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Лангепас")]
        Langepas_dep_Sport = 76,

        [EnumMember(Value = "Департамент социального развития г. Лангепас")]
        Langepas_dep_Social = 77,



        // Мегион Департаменты
        [EnumMember(Value = "Департамент культуры г. Мегион")]
        Megion_dep_Culture = 78,

        [EnumMember(Value = "Департамент молодежной политики г. Мегион")]
        Megion_dep_Young = 79,

        [EnumMember(Value = "Департамент образования и науки г. Мегион")]
        Megion_dep_Education = 80,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Мегион")]
        Megion_dep_Sport = 81,

        [EnumMember(Value = "Департамент социального развития г. Мегион")]
        Megion_dep_Social = 82,



        // Нефтеюганск Департаменты
        [EnumMember(Value = "Департамент культуры г. Нефтеюганск")]
        Nefteyugansk_dep_Culture = 83,

        [EnumMember(Value = "Департамент молодежной политики г. Нефтеюганск")]
        Nefteyugansk_dep_Young = 84,

        [EnumMember(Value = "Департамент образования и науки г. Нефтеюганск")]
        Nefteyugansk_dep_Education = 85,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Нефтеюганск")]
        Nefteyugansk_dep_Sport = 86,

        [EnumMember(Value = "Департамент социального развития г. Нефтеюганск")]
        Nefteyugansk_dep_Social = 87,



        // Нижневартовск Департаменты
        [EnumMember(Value = "Департамент культуры г. Нижневартовск")]
        Nizhnevartovsk_dep_Culture = 88,

        [EnumMember(Value = "Департамент молодежной политики г. Нижневартовск")]
        Nizhnevartovsk_dep_Young = 89,

        [EnumMember(Value = "Департамент образования и науки г. Нижневартовск")]
        Nizhnevartovsk_dep_Education = 90,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Нижневартовск")]
        Nizhnevartovsk_dep_Sport = 91,

        [EnumMember(Value = "Департамент социального развития г. Нижневартовск")]
        Nizhnevartovsk_dep_Social = 92,



        // Нягань Департаменты
        [EnumMember(Value = "Департамент культуры г. Нягань")]
        Nyagan_dep_Culture = 93,

        [EnumMember(Value = "Департамент молодежной политики г. Нягань")]
        Nyagan_dep_Young = 94,

        [EnumMember(Value = "Департамент образования и науки г. Нягань")]
        Nyagan_dep_Education = 95,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Нягань")]
        Nyagan_dep_Sport = 96,

        [EnumMember(Value = "Департамент социального развития г. Нягань")]
        Nyagan_dep_Social = 97,



        // Покачи Департаменты
        [EnumMember(Value = "Департамент культуры г. Покачи")]
        Pokachi_dep_Culture = 98,

        [EnumMember(Value = "Департамент молодежной политики г. Покачи")]
        Pokachi_dep_Young = 99,

        [EnumMember(Value = "Департамент образования и науки г. Покачи")]
        Pokachi_dep_Education = 100,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Покачи")]
        Pokachi_dep_Sport = 101,

        [EnumMember(Value = "Департамент социального развития г. Покачи")]
        Pokachi_dep_Social = 102,



        // Пыть-Ях Департаменты
        [EnumMember(Value = "Департамент культуры г. Пыть-Ях")]
        PythYach_dep_Culture = 103,
        
        [EnumMember(Value = "Департамент молодежной политики г. Пыть-Ях")]
        PythYach_dep_Young = 104,

        [EnumMember(Value = "Департамент образования и науки г. Пыть-Ях")]
        PythYach_dep_Education = 105,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Пыть-Ях")]
        PythYach_dep_Sport = 106,

        [EnumMember(Value = "Департамент социального развития г. Пыть-Ях")]
        PythYach_dep_Social = 107,



        // Радужный Департаменты
        [EnumMember(Value = "Департамент культуры г. Радужный")]
        Raduzhnyi_dep_Culture = 108,

        [EnumMember(Value = "Департамент молодежной политики г. Радужный")]
        Raduzhnyi_dep_Young = 109,

        [EnumMember(Value = "Департамент образования и науки г. Радужный")]
        Raduzhnyi_dep_Education = 110,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Радужный")]
        Raduzhnyi_dep_Sport = 111,

        [EnumMember(Value = "Департамент социального развития г. Радужный")]
        Raduzhnyi_dep_Social = 112,



        // Сургут Департаменты
        [EnumMember(Value = "Департамент культуры г. Сургут")]
        Surgut_dep_Culture = 113,

        [EnumMember(Value = "Департамент молодежной политики г. Сургут")]
        Surgut_dep_Young = 114,

        [EnumMember(Value = "Департамент образования и науки  г. Сургут")]
        Surgut_dep_Education = 115,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Сургут")]
        Surgut_dep_Sport = 116,

        [EnumMember(Value = "Департамент социального развития г. Сургут")]
        Surgut_dep_Social = 117,



        // Урай Департаменты
        [EnumMember(Value = "Департамент культуры г. Урай")]
        Urai_dep_Culture = 118,

        [EnumMember(Value = "Департамент молодежной политики г. Урай")]
        Urai_dep_Young = 119,

        [EnumMember(Value = "Департамент образования и науки г. Урай")]
        Urai_dep_Education = 120,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Урай")]
        Urai_dep_Sport = 121,

        [EnumMember(Value = "Департамент социального развития г. Урай")]
        Urai_dep_Social = 122,



        // Ханты-Мансийск Департаменты
        [EnumMember(Value = "Департамент культуры г. Ханты-Мансийск")]
        KhantyMansiysk_dep_Culture = 123,

        [EnumMember(Value = "Департамент молодежной политики г. Ханты-Мансийск")]
        KhantyMansiysk_dep_Young = 124,

        [EnumMember(Value = "Департамент образования и науки г. Ханты-Мансийск")]
        KhantyMansiysk_dep_Education = 125,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Ханты-Мансийск")]
        KhantyMansiysk_dep_Sport = 126,

        [EnumMember(Value = "Департамент социального развития г. Ханты-Мансийск")]
        KhantyMansiysk_dep_Social = 127,



        // Югорск Департаменты
        [EnumMember(Value = "Департамент культуры г. Югорск")]
        Yugorsk_dep_Culture = 128,

        [EnumMember(Value = "Департамент молодежной политики г. Югорск")]
        Yugorsk_dep_Young = 129,

        [EnumMember(Value = "Департамент образования и науки г. Югорск")]
        Yugorsk_dep_Education = 130,

        [EnumMember(Value = "Департамент физической культуры и спорта г. Югорск")]
        Yugorsk_dep_Sport = 131,

        [EnumMember(Value = "Департамент социального развития г. Югорск")]
        Yugorsk_dep_Social = 132,
    }
}