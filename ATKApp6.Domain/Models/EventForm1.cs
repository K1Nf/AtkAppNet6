﻿using ATKApp6.Domain.Enums;

namespace ATKApp6.Domain.Models
{
    public class EventForm1 : EventBase
    {
        public EventForm1() { }
        public EventForm1(string? name, string? actor, string? content, DateOnly? date, 
                        Guid organizerId, Guid themeId, string? equalToEqualContent,
                        string? result, string? decision,
                        EventType? eventType, LevelType? levelType,
                        bool? isValuable, bool? isBestPractice)
            : base(name, actor, content, date, organizerId, themeId)
        {
            EventType = eventType;
            LevelType = levelType;
            IsValuable = isValuable;
            IsBestPractice = isBestPractice;
            EqualToEqualDescription = equalToEqualContent;
            Result = result;
            Decision = decision;
        }


        public EventType? EventType { get; set; }
        public LevelType? LevelType { get; set; }
        public bool? IsValuable { get; set; }
        public bool? IsBestPractice { get; set; }
        public string? EqualToEqualDescription { get; set; }
        public string? Result { get; set; }
        public string? Decision { get; set; }



        public Finance? Finance { get; set; }
        public FeedBack? FeedBack { get; set; }
        public Concourse? Concourse { get; set; }
        public List<Support> Supports { get; set; } = new List<Support>();
        public List<Audience> Audiences { get; set; } = new List<Audience>();
        public List<InterAgencyCooperation> InterAgencyCooperations { get; set; } = new List<InterAgencyCooperation>();
    }
}
