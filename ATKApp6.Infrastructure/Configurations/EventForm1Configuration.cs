﻿using ATKApp6.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATKApp6.Infrastructure.Configurations
{
    public class EventForm1Configuration : IEntityTypeConfiguration<EventForm1>
    {
        public void Configure(EntityTypeBuilder<EventForm1> builder)
        {
            builder.HasOne(e => e.FeedBack)
                .WithOne(f => f.Event)
                .HasForeignKey(typeof(FeedBack))
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(e => e.Finance)
                .WithOne(f => f.Event)
                .HasForeignKey(typeof(Finance))
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(e => e.Supports)
                .WithOne(s => s.Event)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(e => e.InterAgencyCooperations)
                .WithOne(c => c.Event)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
