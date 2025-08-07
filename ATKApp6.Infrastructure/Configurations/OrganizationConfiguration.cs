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

            //builder.HasData(GetMunicipalityOrgs());
            //builder.HasData(GetDepartmentsOrgs());
        }

        public List<Organization> GetMunicipalityOrgs() => new()
        {
            Organization.Create(StructuredOrganizations.atk_khmao,
            hasher.HashPassword("atk_khmao".ToCharArray()),
            Municipalities.noMunicipality),


            Organization.Create(StructuredOrganizations.atk_beloyarskiy_rayon,
            hasher.HashPassword("atkbelray".ToCharArray()),
            Municipalities.Beloyarskiy_rayon),

            Organization.Create(StructuredOrganizations.atk_berezovskiy_rayon,
            hasher.HashPassword("atkberray".ToCharArray()),
            Municipalities.Berezovskiy_rayon),

            Organization.Create(StructuredOrganizations.atk_condinskiy_rayon,
            hasher.HashPassword("atkconray".ToCharArray()),
            Municipalities.Condinskiy_rayon),

            Organization.Create(StructuredOrganizations.atk_khanty_mansiysk,
            hasher.HashPassword("atkkhm".ToCharArray()),
            Municipalities.Khanty_mansiysk),

            Organization.Create(StructuredOrganizations.atk_khanty_mansiyskiy_rayon,
            hasher.HashPassword("atkkhmray".ToCharArray()),
            Municipalities.Khanty_mansiyskiy_rayon),

            Organization.Create(StructuredOrganizations.atk_kogalym,
            hasher.HashPassword("atkkog".ToCharArray()),
            Municipalities.Kogalym),

            Organization.Create(StructuredOrganizations.atk_langepas,
            hasher.HashPassword("atklan".ToCharArray()              ),
            Municipalities.Langepas),

            Organization.Create(StructuredOrganizations.atk_megion,
            hasher.HashPassword("atkmeg".ToCharArray()),
            Municipalities.Megion),

            Organization.Create(StructuredOrganizations.atk_nefteyugansk,
            hasher.HashPassword("atkneft".ToCharArray()),
            Municipalities.Nefteyugansk),

            Organization.Create(StructuredOrganizations.atk_nefteyuganskiy_rayon,
            hasher.HashPassword("atkneftray".ToCharArray()),
            Municipalities.Nefteyuganskiy_rayon),

            Organization.Create(StructuredOrganizations.atk_nizhnevartovsk,
            hasher.HashPassword("atknizh".ToCharArray()),
            Municipalities.Nizhnevartovsk),

            Organization.Create(StructuredOrganizations.atk_nizhnevartovskiy_rayon,
            hasher.HashPassword("atknizhray".ToCharArray()),
            Municipalities.Nizhnevartovskiy_rayon),

            Organization.Create(StructuredOrganizations.atk_nyagan,
            hasher.HashPassword("atknya".ToCharArray()),
            Municipalities.Nyagan),

            Organization.Create(StructuredOrganizations.atk_oktyabrskiy_rayon,
            hasher.HashPassword("atkoktray".ToCharArray()),
            Municipalities.Oktyabrskiy_rayon),

            Organization.Create(StructuredOrganizations.atk_pokachi,
            hasher.HashPassword("atkpok".ToCharArray()),
            Municipalities.Pokachi),

            Organization.Create(StructuredOrganizations.atk_pyth_yach,
            hasher.HashPassword("atkpyth".ToCharArray()),
            Municipalities.Pyth_yach),

            Organization.Create(StructuredOrganizations.atk_raduzhnyi,
            hasher.HashPassword("atkrad".ToCharArray()),
            Municipalities.Raduzhnyi),

            Organization.Create(StructuredOrganizations.atk_sovietskiy_rayon,
            hasher.HashPassword("atksovray".ToCharArray()),
            Municipalities.Sovietskiy_rayon),

            Organization.Create(StructuredOrganizations.atk_surgut,
            hasher.HashPassword("atksur".ToCharArray()),
            Municipalities.Surgut),

            Organization.Create(StructuredOrganizations.atk_surgutskiy_rayon,
            hasher.HashPassword("atksurray".ToCharArray()),
            Municipalities.Surgutskiy_rayon),

            Organization.Create(StructuredOrganizations.atk_urai,
            hasher.HashPassword("atkurai".ToCharArray()),
            Municipalities.Urai),

            Organization.Create(StructuredOrganizations.atk_yugorsk,
            hasher.HashPassword("atkyug".ToCharArray()),
            Municipalities.Yugorsk)
        };



        public List<Organization> GetDepartmentsOrgs() => new() {

            Organization.Create(StructuredOrganizations.khanty_mansiysk_dep_culture,
            hasher.HashPassword("khmcul".ToCharArray()),
            Municipalities.Khanty_mansiysk),

            Organization.Create(StructuredOrganizations.khanty_mansiysk_dep_education,
            hasher.HashPassword("khmedu".ToCharArray()),
            Municipalities.Khanty_mansiysk),

            Organization.Create(StructuredOrganizations.khanty_mansiysk_dep_young,
            hasher.HashPassword("khmyou".ToCharArray()),
            Municipalities.Khanty_mansiysk),

            Organization.Create(StructuredOrganizations.khanty_mansiysk_dep_sport,
            hasher.HashPassword("khmspo".ToCharArray()),
            Municipalities.Khanty_mansiysk),





            Organization.Create(StructuredOrganizations.surgut_dep_culture,
            hasher.HashPassword("surcul".ToCharArray()),
            Municipalities.Surgut),

            Organization.Create(StructuredOrganizations.surgut_dep_education,
            hasher.HashPassword("suredu".ToCharArray()),
            Municipalities.Surgut),

            Organization.Create(StructuredOrganizations.surgut_dep_young,
            hasher.HashPassword("suryou".ToCharArray()),
            Municipalities.Surgut),

            Organization.Create(StructuredOrganizations.surgut_dep_sport,
            hasher.HashPassword("surspo".ToCharArray()),
            Municipalities.Surgut),





            Organization.Create(StructuredOrganizations.nefteyugansk_dep_culture,
            hasher.HashPassword("nefcul".ToCharArray()),
            Municipalities.Nefteyugansk),

            Organization.Create(StructuredOrganizations.nefteyugansk_dep_education,
            hasher.HashPassword("nefedu".ToCharArray()),
            Municipalities.Nefteyugansk),

            Organization.Create(StructuredOrganizations.nefteyugansk_dep_young,
            hasher.HashPassword("nefyou".ToCharArray()),
            Municipalities.Nefteyugansk),

            Organization.Create(StructuredOrganizations.nefteyugansk_dep_sport,
            hasher.HashPassword("nefspo".ToCharArray()),
            Municipalities.Nefteyugansk),
        };
    }
}
