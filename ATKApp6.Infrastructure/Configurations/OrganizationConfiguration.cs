using ATKApp6.Domain.Enums;
using ATKApp6.Infrastructure.Extensions;
using ATKApp6.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace ATKApp6.Infrastructure.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        private readonly PasswordHasher hasher = new();
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name)
                .IsRequired()
                .HasConversion<string>(); // <-- Конвертация enum в строку

            builder.Property(o => o.Municipality)
                .IsRequired()
                .HasConversion<string>(); // <-- Конвертация enum в строку

            builder.HasMany(o => o.Events)
                .WithOne(e => e.Organizer)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasData(GetATKOrgs());
            builder.HasData(GetDepartmentsOrgs());
        }

        public List<Organization> GetATKOrgs() => new()
        {
            Organization.Create(StructuredOrganizations.ATK_KHMAO,
            hasher.HashPassword("N1FMDBhr#c!*".ToCharArray()),
            Municipalities.NoMunicipality),



            Organization.Create(StructuredOrganizations.ATK_BeloyarskiyRayon,
            hasher.HashPassword("mvVJ9n@1tjMc".ToCharArray()),
            Municipalities.BeloyarskiyRayon),

            Organization.Create(StructuredOrganizations.ATK_BerezovskiyRayon,
            hasher.HashPassword("cn9SeJpVnQ$N".ToCharArray()),
            Municipalities.BerezovskiyRayon),

            Organization.Create(StructuredOrganizations.ATK_CondinskiyRayon,
            hasher.HashPassword("8VUqu#FbMf*6".ToCharArray()),
            Municipalities.CondinskiyRayon),

            Organization.Create(StructuredOrganizations.ATK_NefteyuganskiyRayon,
            hasher.HashPassword("8ehinYMU!*Zq".ToCharArray()),
            Municipalities.NefteyuganskiyRayon),

            Organization.Create(StructuredOrganizations.ATK_NizhnevartovskiyRayon,
            hasher.HashPassword("jkf*$Uy11uf%".ToCharArray()),
            Municipalities.NizhnevartovskiyRayon),

            Organization.Create(StructuredOrganizations.ATK_OktyabrskiyRayon,
            hasher.HashPassword("X#&#vhu!v4cW".ToCharArray()),
            Municipalities.OktyabrskiyRayon),

            Organization.Create(StructuredOrganizations.ATK_SovietskiyRayon,
            hasher.HashPassword("xnYG!e!7rkTU".ToCharArray()),
            Municipalities.SovietskiyRayon),

            Organization.Create(StructuredOrganizations.ATK_SurgutskiyRayon,
            hasher.HashPassword("MTlEh!0uFoZg".ToCharArray()),
            Municipalities.SurgutskiyRayon),

            Organization.Create(StructuredOrganizations.ATK_KhantyMansiyskiyRayon,
            hasher.HashPassword("Qfi6%*&!MY$Q".ToCharArray()),
            Municipalities.KhantyMansiyskiyRayon),



            Organization.Create(StructuredOrganizations.ATK_Kogalym,
            hasher.HashPassword("XoQfUL7u!t&C".ToCharArray()),
            Municipalities.Kogalym),

            Organization.Create(StructuredOrganizations.ATK_Langepas,
            hasher.HashPassword("kY0HM7CV*@zY".ToCharArray()),
            Municipalities.Langepas),

            Organization.Create(StructuredOrganizations.ATK_Megion,
            hasher.HashPassword("5d5nhLAoO*IK".ToCharArray()),
            Municipalities.Megion),

            Organization.Create(StructuredOrganizations.ATK_Nefteyugansk,
            hasher.HashPassword("^N&!S*D6qRRv".ToCharArray()),
            Municipalities.Nefteyugansk),
            
            Organization.Create(StructuredOrganizations.ATK_Nizhnevartovsk,
            hasher.HashPassword("N81qB1&NM2!P".ToCharArray()),
            Municipalities.Nizhnevartovsk),

            Organization.Create(StructuredOrganizations.ATK_Nyagan,
            hasher.HashPassword("1eyk0pV^*xzo".ToCharArray()),
            Municipalities.Nyagan),

            Organization.Create(StructuredOrganizations.ATK_Pokachi,
            hasher.HashPassword("atkpok".ToCharArray()),
            Municipalities.Pokachi),

            Organization.Create(StructuredOrganizations.ATK_PythYach,
            hasher.HashPassword("DA^R!m18V1jj".ToCharArray()),
            Municipalities.PythYach),

            Organization.Create(StructuredOrganizations.ATK_Raduzhnyi,
            hasher.HashPassword("oKDv6AhfLiKW".ToCharArray()),
            Municipalities.Raduzhnyi),

            Organization.Create(StructuredOrganizations.ATK_Surgut,
            hasher.HashPassword("jUcHwJmBBxEx".ToCharArray()),
            Municipalities.Surgut),

            Organization.Create(StructuredOrganizations.ATK_Urai,
            hasher.HashPassword("atkurai".ToCharArray()),
            Municipalities.Urai),

            Organization.Create(StructuredOrganizations.ATK_KhantyMansiysk,
            hasher.HashPassword("4c4Ea#VfGyo#".ToCharArray()),
            Municipalities.KhantyMansiysk),

            Organization.Create(StructuredOrganizations.ATK_Yugorsk,
            hasher.HashPassword("E3eOUvB6YBSJ".ToCharArray()),
            Municipalities.Yugorsk)
        };



        public List<Organization> GetDepartmentsOrgs() => new()
        {
            Organization.Create(StructuredOrganizations.BeloyarskiyRayon_Culture,
            hasher.HashPassword("vGNoDuUD6kj3".ToCharArray()),
            Municipalities.BeloyarskiyRayon),

            Organization.Create(StructuredOrganizations.BeloyarskiyRayon_Education,
            hasher.HashPassword("HZwKlg&WgesM".ToCharArray()),
            Municipalities.BeloyarskiyRayon),

            Organization.Create(StructuredOrganizations.BeloyarskiyRayon_Social,
            hasher.HashPassword("uZpcpTf7YVkh".ToCharArray()),
            Municipalities.BeloyarskiyRayon),

            Organization.Create(StructuredOrganizations.BeloyarskiyRayon_Sport,
            hasher.HashPassword("24d75P9TAKu0".ToCharArray()),
            Municipalities.BeloyarskiyRayon),

            Organization.Create(StructuredOrganizations.BeloyarskiyRayon_Young,
            hasher.HashPassword("SYE4WlPcZP4v".ToCharArray()),
            Municipalities.BeloyarskiyRayon),



            Organization.Create(StructuredOrganizations.BerezovskiyRayon_Culture,
            hasher.HashPassword("Ud#!QKpMOZ09".ToCharArray()),
            Municipalities.BerezovskiyRayon),

            Organization.Create(StructuredOrganizations.BerezovskiyRayon_Education,
            hasher.HashPassword("v6DsIIcaBGOe".ToCharArray()),
            Municipalities.BerezovskiyRayon),

            Organization.Create(StructuredOrganizations.BerezovskiyRayon_Social,
            hasher.HashPassword("afBmFmd$tyav".ToCharArray()),
            Municipalities.BerezovskiyRayon),

            Organization.Create(StructuredOrganizations.BerezovskiyRayon_Sport,
            hasher.HashPassword("KhPiifDnwCVe".ToCharArray()),
            Municipalities.BerezovskiyRayon),

            Organization.Create(StructuredOrganizations.BerezovskiyRayon_Young,
            hasher.HashPassword("Z#1z9B4hZyT@".ToCharArray()),
            Municipalities.BerezovskiyRayon),



            Organization.Create(StructuredOrganizations.CondinskiyRayon_Culture,
            hasher.HashPassword("b6^1!yiLW2Dk".ToCharArray()),
            Municipalities.CondinskiyRayon),

            Organization.Create(StructuredOrganizations.CondinskiyRayon_Education,
            hasher.HashPassword("nkBY2MmmO4!a".ToCharArray()),
            Municipalities.CondinskiyRayon),

            Organization.Create(StructuredOrganizations.CondinskiyRayon_Social,
            hasher.HashPassword("dNcB8skS69*m".ToCharArray()),
            Municipalities.CondinskiyRayon),

            Organization.Create(StructuredOrganizations.CondinskiyRayon_Sport,
            hasher.HashPassword("eO9m2EZsDtDN".ToCharArray()),
            Municipalities.CondinskiyRayon),

            Organization.Create(StructuredOrganizations.CondinskiyRayon_Young,
            hasher.HashPassword("1@iNKEl&EncA".ToCharArray()),
            Municipalities.CondinskiyRayon),



            Organization.Create(StructuredOrganizations.NefteyuganskiyRayon_Culture,
            hasher.HashPassword("x5Rxly^%tUc1".ToCharArray()),
            Municipalities.NefteyuganskiyRayon),

            Organization.Create(StructuredOrganizations.NefteyuganskiyRayon_Education,
            hasher.HashPassword("Kr!HXU3QGUG4".ToCharArray()),
            Municipalities.NefteyuganskiyRayon),

            Organization.Create(StructuredOrganizations.NefteyuganskiyRayon_Social,
            hasher.HashPassword("QVuw3ZOsL5L0".ToCharArray()),
            Municipalities.NefteyuganskiyRayon),

            Organization.Create(StructuredOrganizations.NefteyuganskiyRayon_Sport,
            hasher.HashPassword("Tgl5jdofcVmR".ToCharArray()),
            Municipalities.NefteyuganskiyRayon),

            Organization.Create(StructuredOrganizations.NefteyuganskiyRayon_Young,
            hasher.HashPassword("vywb#skeNf$M".ToCharArray()),
            Municipalities.NefteyuganskiyRayon),



            Organization.Create(StructuredOrganizations.NizhnevartovskiyRayon_Culture,
            hasher.HashPassword("HMNI4F56uKS!".ToCharArray()),
            Municipalities.NizhnevartovskiyRayon),

            Organization.Create(StructuredOrganizations.NizhnevartovskiyRayon_Education,
            hasher.HashPassword("0yuO4X$bTk!6".ToCharArray()),
            Municipalities.NizhnevartovskiyRayon),

            Organization.Create(StructuredOrganizations.NizhnevartovskiyRayon_Social,
            hasher.HashPassword("z^T0Rxod2XpB".ToCharArray()),
            Municipalities.NizhnevartovskiyRayon),

            Organization.Create(StructuredOrganizations.NizhnevartovskiyRayon_Sport,
            hasher.HashPassword("epz3PUE&TIgJ".ToCharArray()),
            Municipalities.NizhnevartovskiyRayon),

            Organization.Create(StructuredOrganizations.NizhnevartovskiyRayon_Young,
            hasher.HashPassword("YsCL!TPZX5uH".ToCharArray()),
            Municipalities.NizhnevartovskiyRayon),



            Organization.Create(StructuredOrganizations.OktyabrskiyRayon_Culture,
            hasher.HashPassword("KgMEBBWNyWqH".ToCharArray()),
            Municipalities.OktyabrskiyRayon),

            Organization.Create(StructuredOrganizations.OktyabrskiyRayon_Education,
            hasher.HashPassword("d9CFj%XWYpAT".ToCharArray()),
            Municipalities.OktyabrskiyRayon),

            Organization.Create(StructuredOrganizations.OktyabrskiyRayon_Social,
            hasher.HashPassword("ZdQDuwy7sO7j".ToCharArray()),
            Municipalities.OktyabrskiyRayon),

            Organization.Create(StructuredOrganizations.OktyabrskiyRayon_Sport,
            hasher.HashPassword("W2v5IZogx@iF".ToCharArray()),
            Municipalities.OktyabrskiyRayon),

            Organization.Create(StructuredOrganizations.OktyabrskiyRayon_Young,
            hasher.HashPassword("4I&h0O2FSF&q".ToCharArray()),
            Municipalities.OktyabrskiyRayon),



            Organization.Create(StructuredOrganizations.SovietskiyRayon_Culture,
            hasher.HashPassword("VdH9SXvt@wDC".ToCharArray()),
            Municipalities.SovietskiyRayon),

            Organization.Create(StructuredOrganizations.SovietskiyRayon_Education,
            hasher.HashPassword("tWS#ZvdMqRef".ToCharArray()),
            Municipalities.SovietskiyRayon),

            Organization.Create(StructuredOrganizations.SovietskiyRayon_Social,
            hasher.HashPassword("5Dr9IuTvH@gI".ToCharArray()),
            Municipalities.SovietskiyRayon),

            Organization.Create(StructuredOrganizations.SovietskiyRayon_Sport,
            hasher.HashPassword("W%DRZyb5F$U!".ToCharArray()),
            Municipalities.SovietskiyRayon),

            Organization.Create(StructuredOrganizations.SovietskiyRayon_Young,
            hasher.HashPassword("1il#n@Vnzp0&".ToCharArray()),
            Municipalities.SovietskiyRayon),



            Organization.Create(StructuredOrganizations.SurgutskiyRayon_Culture,
            hasher.HashPassword("n1pG6sqS49cJ".ToCharArray()),
            Municipalities.SurgutskiyRayon),

            Organization.Create(StructuredOrganizations.SurgutskiyRayon_Education,
            hasher.HashPassword("DOBHNu5$lTK7".ToCharArray()),
            Municipalities.SurgutskiyRayon),

            Organization.Create(StructuredOrganizations.SurgutskiyRayon_Social,
            hasher.HashPassword("JoW2TQZMr&i6".ToCharArray()),
            Municipalities.SurgutskiyRayon),

            Organization.Create(StructuredOrganizations.SurgutskiyRayon_Sport,
            hasher.HashPassword("r1ZJjl5Kly1u".ToCharArray()),
            Municipalities.SurgutskiyRayon),

            Organization.Create(StructuredOrganizations.SurgutskiyRayon_Young,
            hasher.HashPassword("9rA3$VDG35UG".ToCharArray()),
            Municipalities.SurgutskiyRayon),



            Organization.Create(StructuredOrganizations.KhantyMansiyskiyRayon_Culture,
            hasher.HashPassword("CFafcd*Y*u8s".ToCharArray()),
            Municipalities.KhantyMansiyskiyRayon),

            Organization.Create(StructuredOrganizations.KhantyMansiyskiyRayon_Education,
            hasher.HashPassword("t7n1^yEn@@JP".ToCharArray()),
            Municipalities.KhantyMansiyskiyRayon),

            Organization.Create(StructuredOrganizations.KhantyMansiyskiyRayon_Social,
            hasher.HashPassword("nvqN*13MTm3h".ToCharArray()),
            Municipalities.KhantyMansiyskiyRayon),

            Organization.Create(StructuredOrganizations.KhantyMansiyskiyRayon_Sport,
            hasher.HashPassword("V&U4#h#TUVna".ToCharArray()),
            Municipalities.KhantyMansiyskiyRayon),

            Organization.Create(StructuredOrganizations.KhantyMansiyskiyRayon_Young,
            hasher.HashPassword("Y8qI^8M$C##K".ToCharArray()),
            Municipalities.KhantyMansiyskiyRayon),



            Organization.Create(StructuredOrganizations.Kogalym_Culture,
            hasher.HashPassword("PZ6Byhp5PO1H".ToCharArray()),
            Municipalities.Kogalym),

            Organization.Create(StructuredOrganizations.Kogalym_Education,
            hasher.HashPassword("v8dtI#cQv*5A".ToCharArray()),
            Municipalities.Kogalym),

            Organization.Create(StructuredOrganizations.Kogalym_Social,
            hasher.HashPassword("0tJAi1G^7c&h".ToCharArray()),
            Municipalities.Kogalym),

            Organization.Create(StructuredOrganizations.Kogalym_Sport,
            hasher.HashPassword("OeFx9I&cqdkb".ToCharArray()),
            Municipalities.Kogalym),

            Organization.Create(StructuredOrganizations.Kogalym_Young,
            hasher.HashPassword("m^Vob!@jc^Py".ToCharArray()),
            Municipalities.Kogalym),



            Organization.Create(StructuredOrganizations.Langepas_Culture,
            hasher.HashPassword("k4WNXdJ2a!Ye".ToCharArray()),
            Municipalities.Langepas),

            Organization.Create(StructuredOrganizations.Langepas_Education,
            hasher.HashPassword("uF&!1sTtWMQd".ToCharArray()),
            Municipalities.Langepas),

            Organization.Create(StructuredOrganizations.Langepas_Social,
            hasher.HashPassword("eeuV4T4l%wXb".ToCharArray()),
            Municipalities.Langepas),

            Organization.Create(StructuredOrganizations.Langepas_Sport,
            hasher.HashPassword("cGud1wNlaF*r".ToCharArray()),
            Municipalities.Langepas),

            Organization.Create(StructuredOrganizations.Langepas_Young,
            hasher.HashPassword("ama8om8L^p*z".ToCharArray()),
            Municipalities.Langepas),



            Organization.Create(StructuredOrganizations.Megion_Culture,
            hasher.HashPassword("mBW7EpGiQ5PP".ToCharArray()),
            Municipalities.Megion),

            Organization.Create(StructuredOrganizations.Megion_Education,
            hasher.HashPassword("aeGEoaHnv$#8".ToCharArray()),
            Municipalities.Megion),

            Organization.Create(StructuredOrganizations.Megion_Social,
            hasher.HashPassword("xkuiKH8oYMD&".ToCharArray()),
            Municipalities.Megion),

            Organization.Create(StructuredOrganizations.Megion_Sport,
            hasher.HashPassword("9XlKI#q2osLx".ToCharArray()),
            Municipalities.Megion),

            Organization.Create(StructuredOrganizations.Megion_Young,
            hasher.HashPassword("n$vYpb5GGY&D".ToCharArray()),
            Municipalities.Megion),



            Organization.Create(StructuredOrganizations.Nefteyugansk_Culture,
            hasher.HashPassword("N5kDAudA%Y0p".ToCharArray()),
            Municipalities.Nefteyugansk),

            Organization.Create(StructuredOrganizations.Nefteyugansk_Education,
            hasher.HashPassword("QKuKc814EO6v".ToCharArray()),
            Municipalities.Nefteyugansk),

            Organization.Create(StructuredOrganizations.Nefteyugansk_Social,
            hasher.HashPassword("aI@UdSVZ3f3a".ToCharArray()),
            Municipalities.Nefteyugansk),

            Organization.Create(StructuredOrganizations.Nefteyugansk_Sport,
            hasher.HashPassword("jmfk5d3*kxqB".ToCharArray()),
            Municipalities.Nefteyugansk),

            Organization.Create(StructuredOrganizations.Nefteyugansk_Young,
            hasher.HashPassword("H^8UaGs7psV2".ToCharArray()),
            Municipalities.Nefteyugansk),



            Organization.Create(StructuredOrganizations.Nizhnevartovsk_Culture,
            hasher.HashPassword("7HDaZptGlD#F".ToCharArray()),
            Municipalities.Nizhnevartovsk),

            Organization.Create(StructuredOrganizations.Nizhnevartovsk_Education,
            hasher.HashPassword("Yf*S1vdiN$rX".ToCharArray()),
            Municipalities.Nizhnevartovsk),

            Organization.Create(StructuredOrganizations.Nizhnevartovsk_Social,
            hasher.HashPassword("y93*&rMxt$U@".ToCharArray()),
            Municipalities.Nizhnevartovsk),

            Organization.Create(StructuredOrganizations.Nizhnevartovsk_Sport,
            hasher.HashPassword("wq7Ni01xcKi4".ToCharArray()),
            Municipalities.Nizhnevartovsk),

            Organization.Create(StructuredOrganizations.Nizhnevartovsk_Young,
            hasher.HashPassword("n0T%1Z6QvGA6".ToCharArray()),
            Municipalities.Nizhnevartovsk),



            Organization.Create(StructuredOrganizations.Nyagan_Culture,
            hasher.HashPassword("5YGWkZhUx%am".ToCharArray()),
            Municipalities.Nyagan),

            Organization.Create(StructuredOrganizations.Nyagan_Education,
            hasher.HashPassword("DZUFCM#byjve".ToCharArray()),
            Municipalities.Nyagan),

            Organization.Create(StructuredOrganizations.Nyagan_Social,
            hasher.HashPassword("v9wMN@FYaOeY".ToCharArray()),
            Municipalities.Nyagan),

            Organization.Create(StructuredOrganizations.Nyagan_Sport,
            hasher.HashPassword("HzzIP#3fEk$x".ToCharArray()),
            Municipalities.Nyagan),

            Organization.Create(StructuredOrganizations.Nyagan_Young,
            hasher.HashPassword("q0Zh0wlO%Qdv".ToCharArray()),
            Municipalities.Nyagan),



            Organization.Create(StructuredOrganizations.Pokachi_Culture,
            hasher.HashPassword("pZzQJDaM@%cq".ToCharArray()),
            Municipalities.Pokachi),

            Organization.Create(StructuredOrganizations.Pokachi_Education,
            hasher.HashPassword("qBL*jcYG3kvC".ToCharArray()),
            Municipalities.Pokachi),

            Organization.Create(StructuredOrganizations.Pokachi_Social,
            hasher.HashPassword("X@$UXbAA#7ri".ToCharArray()),
            Municipalities.Pokachi),

            Organization.Create(StructuredOrganizations.Pokachi_Sport,
            hasher.HashPassword("RGFaHg0oGMWG".ToCharArray()),
            Municipalities.Pokachi),

            Organization.Create(StructuredOrganizations.Pokachi_Young,
            hasher.HashPassword("vCBLK8GqgGVg".ToCharArray()),
            Municipalities.Pokachi),



            Organization.Create(StructuredOrganizations.PythYach_Culture,
            hasher.HashPassword("uVe&&Q8RWS^J".ToCharArray()),
            Municipalities.PythYach),

            Organization.Create(StructuredOrganizations.PythYach_Education,
            hasher.HashPassword("swB2zP^!16Dw".ToCharArray()),
            Municipalities.PythYach),

            Organization.Create(StructuredOrganizations.PythYach_Social,
            hasher.HashPassword("A@i#x@3ZKzIg".ToCharArray()),
            Municipalities.PythYach),

            Organization.Create(StructuredOrganizations.PythYach_Sport,
            hasher.HashPassword("NUbEBe9nI0Ld".ToCharArray()),
            Municipalities.PythYach),

            Organization.Create(StructuredOrganizations.PythYach_Young,
            hasher.HashPassword("UQVu1cLiFoEb".ToCharArray()),
            Municipalities.PythYach),



            Organization.Create(StructuredOrganizations.Raduzhnyi_Culture,
            hasher.HashPassword("q&F86QssK0yH".ToCharArray()),
            Municipalities.Raduzhnyi),

            Organization.Create(StructuredOrganizations.Raduzhnyi_Education,
            hasher.HashPassword("ez#e5*x#qT##".ToCharArray()),
            Municipalities.Raduzhnyi),

            Organization.Create(StructuredOrganizations.Raduzhnyi_Social,
            hasher.HashPassword("eK0eu3mSZ57s".ToCharArray()),
            Municipalities.Raduzhnyi),

            Organization.Create(StructuredOrganizations.Raduzhnyi_Sport,
            hasher.HashPassword("G7UTo2BfGwZB".ToCharArray()),
            Municipalities.Raduzhnyi),

            Organization.Create(StructuredOrganizations.Raduzhnyi_Young,
            hasher.HashPassword("hoH@Jpy6jQGb".ToCharArray()),
            Municipalities.Raduzhnyi),



            Organization.Create(StructuredOrganizations.Surgut_Culture,
            hasher.HashPassword("XlM3^GjtXN3R".ToCharArray()),
            Municipalities.Surgut),

            Organization.Create(StructuredOrganizations.Surgut_Education,
            hasher.HashPassword("fNHHII^B@vt9".ToCharArray()),
            Municipalities.Surgut),

            Organization.Create(StructuredOrganizations.Surgut_Social,
            hasher.HashPassword("m5L^ePZyWA12".ToCharArray()),
            Municipalities.Surgut),

            Organization.Create(StructuredOrganizations.Surgut_Sport,
            hasher.HashPassword("kzBv#iuyUAC%".ToCharArray()),
            Municipalities.Surgut),

            Organization.Create(StructuredOrganizations.Surgut_Young,
            hasher.HashPassword("miqk&0gjcfJb".ToCharArray()),
            Municipalities.Surgut),



            Organization.Create(StructuredOrganizations.Urai_Culture,
            hasher.HashPassword("iCC6ojdqiOpw".ToCharArray()),
            Municipalities.Urai),

            Organization.Create(StructuredOrganizations.Urai_Education,
            hasher.HashPassword("ZVzTtH*96pwq".ToCharArray()),
            Municipalities.Urai),

            Organization.Create(StructuredOrganizations.Urai_Social,
            hasher.HashPassword("3ZACm0peC7%V".ToCharArray()),
            Municipalities.Urai),
            
            Organization.Create(StructuredOrganizations.Urai_Sport,
            hasher.HashPassword("gaHRQyPRNd5e".ToCharArray()),
            Municipalities.Urai),

            Organization.Create(StructuredOrganizations.Urai_Young,
            hasher.HashPassword("AV^^X!5bO!a7".ToCharArray()),
            Municipalities.Urai),



            Organization.Create(StructuredOrganizations.KhantyMansiysk_Culture,
            hasher.HashPassword("Cye@yd4GNL6m".ToCharArray()),
            Municipalities.KhantyMansiysk),

            Organization.Create(StructuredOrganizations.KhantyMansiysk_Education,
            hasher.HashPassword("cwJ05Hrt*oif".ToCharArray()),
            Municipalities.KhantyMansiysk),

            Organization.Create(StructuredOrganizations.KhantyMansiysk_Social,
            hasher.HashPassword("WoEsIdzzjpLG".ToCharArray()),
            Municipalities.KhantyMansiysk),

            Organization.Create(StructuredOrganizations.KhantyMansiysk_Sport,
            hasher.HashPassword("e2#F&Nvgoxiz".ToCharArray()),
            Municipalities.KhantyMansiysk),

            Organization.Create(StructuredOrganizations.KhantyMansiysk_Young,
            hasher.HashPassword("IZ3j4VZZqjkA".ToCharArray()),
            Municipalities.KhantyMansiysk),



            Organization.Create(StructuredOrganizations.Yugorsk_Culture,
            hasher.HashPassword("mT^4V5^rge6r".ToCharArray()),
            Municipalities.Yugorsk),

            Organization.Create(StructuredOrganizations.Yugorsk_Education,
            hasher.HashPassword("L%ndqNFXXUJd".ToCharArray()),
            Municipalities.Yugorsk),

            Organization.Create(StructuredOrganizations.Yugorsk_Social,
            hasher.HashPassword("0A&pHXN7XxRX".ToCharArray()),
            Municipalities.Yugorsk),

            Organization.Create(StructuredOrganizations.Yugorsk_Sport,
            hasher.HashPassword("IwDEY^9r#BFI".ToCharArray()),
            Municipalities.Yugorsk),

            Organization.Create(StructuredOrganizations.Yugorsk_Young,
            hasher.HashPassword("XxU5xToZ0OJD".ToCharArray()),
            Municipalities.Yugorsk),
        };
    }
}
