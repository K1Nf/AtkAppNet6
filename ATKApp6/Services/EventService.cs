﻿using ATKApp6.Contracts.Request;
using ATKApp6.Contracts.Response;
using ATKApp6.Infrastructure.DataBase;
using ATKApp6.Domain.Enums;
using ATKApp6.Infrastructure.Extensions;
using ATKApp6.Domain.Models;
using CSharpFunctionalExtensions;
//using Microsoft.Diagnostics.Tracing.Parsers.Clr;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ATKApp6.Services
{
    public class EventService
    {
        private readonly DataBaseContext _dB;
        private readonly ILogger<EventService> _logger;
        
        public EventService(DataBaseContext dB, ILogger<EventService> logger)
        {
            _dB = dB;
            _logger = logger;
        }


        public async Task<object?> Get(Guid eventId, Guid tokenId)
        {
            var eventForm1 = await _dB.EventForm1s
                .WithBaseIncludes()
                .Include(x => x.Finance)
                .Include(x => x.FeedBack)
                .Include(x => x.Concourse)
                .Include(x => x.InterAgencyCooperations)
                .Include(x => x.Supports)
                .Include(x => x.Audiences)
                .FirstOrDefaultAsync(x => x.Id == eventId);

            if (eventForm1 != null)
            {
                return new { @event = eventForm1, canDelete = eventForm1.Organizer!.Id == tokenId };
            }



            var eventForm2 = await _dB.EventForm2s
                .WithBaseIncludes()
                .FirstOrDefaultAsync(x => x.Id == eventId);

            if (eventForm2 != null)
            {
                return new { @event = eventForm2, canDelete = eventForm2.Organizer!.Id == tokenId };
            }



            var eventForm3 = await _dB.EventForm3s
                .WithBaseIncludes()
                .Include(x => x.Violations)
                .FirstOrDefaultAsync(x => x.Id == eventId);

            if (eventForm3 != null)
            {
                return new { @event = eventForm3, canDelete = eventForm3.Organizer!.Id == tokenId };
            }



            var eventForm4 = await _dB.EventForm4s
                .WithBaseIncludes()
                .Include(x => x.Agreements)
                .FirstOrDefaultAsync(x => x.Id == eventId);

            if (eventForm4 != null)
            {
                return new { @event = eventForm4, canDelete = eventForm4.Organizer!.Id == tokenId };
            }



            var eventBase = await _dB.EventsBase
                .WithBaseIncludes()
                .FirstOrDefaultAsync(x => x.Id == eventId);

            if (eventBase != null)
            {
                return new { @event = eventBase, canDelete = eventBase.Organizer!.Id == tokenId };
            }



            return null;
        }



        public async Task<Result<List<ShortEventResponse>>> GetAll()
        {
            var events = await _dB.EventsBase
                .WithBaseIncludes()
                .Select(x => new ShortEventResponse
                {
                    Id = x.Id,
                    ThemeCode = x.Theme!.Code,
                    Name = x.Name,
                    ParticipantsCount = x.Categories!.Sum(x => x.Count),
                    Content = x.Content,
                    OrganizerName = x.Organizer!.Name,

                    Date = x.Date == null ?
                        string.Empty :
                        $"{x.Date.Value.Day} {GetMonth(x.Date.Value.Month)} {x.Date.Value.Year}",

                    Links = x.MediaLinks
                        .Select(x => x.Content)
                        .ToArray(),
                })
                .OrderBy(x => x.ThemeCode)
                .ToListAsync();

            return Result.Success(events);
        }



        public async Task<Result<EventBase>> CreateBase(Guid tokenId, CreateEventBaseRequest createEventBaseRequest)
        {
            var (eventDate, themeId) = GetThemeIdAndDate(createEventBaseRequest.Date, createEventBaseRequest.ThemeCode);


            var newEvent = EventBase.Create(createEventBaseRequest.Name, createEventBaseRequest.Actor,
                createEventBaseRequest.Content, eventDate, tokenId, themeId);


            using var transaction = await _dB.Database.BeginTransactionAsync();

            try
            {
                await CreateMediaLinkAsync(createEventBaseRequest.CreateMediaLinkRequest, newEvent.Id);
                await CreateParticipantsAsync(createEventBaseRequest.CreateParticipantsRequest, newEvent.Id);

                await _dB.EventsBase.AddAsync(newEvent);
                await _dB.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (TransactionAbortedException ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError($"Транзакция по созданию мероприятия типа {typeof(EventBase)} провалилась с сообщением: {ex.Message}");
                return Result.Failure<EventBase>("Не удалось создать мероприятие");
            }

            return Result.Success(newEvent);
        }



        public async Task<Result<EventForm1>> CreateEventForm1(Guid tokenId, CreateEventForm1Request createEventForm1Request)
        {
            var (eventDate, themeId) = GetThemeIdAndDate(createEventForm1Request.Date, createEventForm1Request.ThemeCode);


            var eventForm1 = new EventForm1(createEventForm1Request.Name, createEventForm1Request.Actor,
                createEventForm1Request.Content, eventDate, tokenId, themeId, createEventForm1Request.EqualToEqual,
                createEventForm1Request.Result, createEventForm1Request.Decision, createEventForm1Request.Form, createEventForm1Request.Level,
                createEventForm1Request.IsValuable, createEventForm1Request.IsBestPractice);


            // begin transaction
            using var transaction = await _dB.Database.BeginTransactionAsync();

            try
            {
                // medialinks, participants, finance, feedback, interagency
                await CreateMediaLinkAsync(createEventForm1Request.CreateMediaLinkRequest, eventForm1.Id);
                await CreateParticipantsAsync(createEventForm1Request.CreateParticipantsRequest, eventForm1.Id);

                await CreateFinanceAsync(createEventForm1Request.CreateFinanceRequest, eventForm1.Id);
                await CreateFeedBackAsync(createEventForm1Request.CreateFeedBackRequest, eventForm1.Id);
                await CreateInterAgencyCoopAsync(createEventForm1Request.CreateInterAgencyCooperationRequest, eventForm1.Id);

                await CreateSupportAsync(createEventForm1Request.CreateSupportRequest, eventForm1.Id);
                await CreateAudienceAsync(createEventForm1Request.CreateAudienceRequest, eventForm1.Id);

                await CreateSourceOfDestinationAsync(createEventForm1Request.CreateSourcesOfDistributionRequest, eventForm1.Id);
                await CreateConcourseAsync(createEventForm1Request.CreateConcourseRequest, eventForm1.Id);

                await _dB.EventForm1s.AddAsync(eventForm1);


                await _dB.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (TransactionAbortedException ex)
            {
                await transaction.RollbackAsync();

                _logger.LogError($"ERROR - {DateTime.Now}: Транзакция по созданию мероприятия типа " +
                    $"{typeof(EventForm1)} провалилась с сообщением: {ex.Message}");

                return Result.Failure<EventForm1>("Не удалось создать мероприятие");
            }

            return Result.Success(eventForm1);
        }



        public async Task<Result<EventForm2>> CreateEventForm2(Guid tokenId, CreateEventForm2Request createEventForm2Request)
        {
            var (eventDate, themeId) = GetThemeIdAndDate(createEventForm2Request.Date, createEventForm2Request.ThemeCode);

            var eventForm2 = new EventForm2(createEventForm2Request.Actor, createEventForm2Request.Name,
                                          createEventForm2Request.Content, eventDate, tokenId, themeId,
                                          createEventForm2Request.Request, createEventForm2Request.Description,
                                          createEventForm2Request.ResultDescription, createEventForm2Request.Participant);

            using var transaction = await _dB.Database.BeginTransactionAsync();

            try
            {
                await CreateMediaLinkAsync(createEventForm2Request.CreateMediaLinkRequest, eventForm2.Id);
                await CreateParticipantsAsync(createEventForm2Request.CreateParticipantsRequest, eventForm2.Id);

                await _dB.EventForm2s.AddAsync(eventForm2);


                await _dB.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (TransactionAbortedException ex)
            {
                await transaction.RollbackAsync();

                _logger.LogError($"ERROR - {DateTime.Now}: Транзакция по созданию мероприятия типа " +
                    $"{typeof(EventForm2)} провалилась с сообщением: {ex.Message}");

                return Result.Failure<EventForm2>("Не удалось создать мероприятие");
            }

            return Result.Success(eventForm2);
        }



        public async Task<Result<EventForm3>> CreateEventForm3(Guid tokenId, CreateEventForm3Request createEventForm3Request)
        {
            var (eventDate, themeId) = GetThemeIdAndDate(createEventForm3Request.Date, createEventForm3Request.ThemeCode);

            var createViolationsRequest = createEventForm3Request.CreateViolationsRequest;


            int sendMaterialsTotal = createViolationsRequest.Violations
                .Sum(x => x.Value.SentCount);

            int blockedMaterialsTotal = createViolationsRequest.Violations
                .Sum(x => x.Value.BlockedCount);


            var eventForm3 = EventForm3.Create(createEventForm3Request.Actor, createEventForm3Request.Name,
                                        createEventForm3Request.Content, eventDate, tokenId,
                                        themeId, sendMaterialsTotal, blockedMaterialsTotal);


            if (eventForm3 == null)
                return Result.Failure<EventForm3>("Количество заблокированных материалов не может быть больше отправленных");


            using var transaction = await _dB.Database.BeginTransactionAsync();

            try
            {
                await CreateViolationAsync(createViolationsRequest, eventForm3.Id);
                await _dB.EventForm3s.AddAsync(eventForm3);


                await _dB.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (TransactionAbortedException ex)
            {
                await transaction.RollbackAsync();

                _logger.LogError($"ERROR - {DateTime.Now}: Транзакция по созданию мероприятия типа " +
                    $"{typeof(EventForm3)} провалилась с сообщением: {ex.Message}");

                return Result.Failure<EventForm3>("Не удалось создать мероприятие");
            }

            return Result.Success(eventForm3);
        }



        public async Task<Result<EventForm4>> CreateEventForm4(Guid tokenId, CreateEventForm4Request createEventForm4Request)
        {
            var (eventDate, themeId) = GetThemeIdAndDate(createEventForm4Request.Date, createEventForm4Request.ThemeCode);

            var eventForm4 = new EventForm4(createEventForm4Request.Actor, createEventForm4Request.Name,
                                        createEventForm4Request.Content, eventDate, tokenId, themeId,
                                        createEventForm4Request.DirectToNAC,
                                        createEventForm4Request.SendToSubjects);

            using var transaction = await _dB.Database.BeginTransactionAsync();

            try
            {
                await CreateMediaLinkAsync(createEventForm4Request.CreateMediaLinkRequest, eventForm4.Id);
                await CreateParticipantsAsync(createEventForm4Request.CreateParticipantsRequest, eventForm4.Id);
                await CreateAgreementAsync(createEventForm4Request.CreateAgreementRequest, eventForm4.Id);

                await _dB.EventForm4s.AddAsync(eventForm4);


                await _dB.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (TransactionAbortedException ex)
            {
                await transaction.RollbackAsync();

                _logger.LogError($"ERROR - {DateTime.Now}: Транзакция по созданию мероприятия типа " +
                    $"{typeof(EventForm4)} провалилась с сообщением: {ex.Message}");

                return Result.Failure<EventForm4>("Не удалось создать мероприятие");
            }

            return Result.Success(eventForm4);
        }



        public async Task<Result> Delete(Guid eventId)
        {
            var @event = await _dB.Set<EventBase>()
                .FirstOrDefaultAsync(x => x.Id == eventId);

            if (@event != null)
            {
                _dB.Remove(@event);
                await _dB.SaveChangesAsync();
                return Result.Success("Мероприятие успешно удалено!");
            }

            return Result.Failure("Не найдено такое мероприятие.");
        }



        public async Task<object?> GetSortedAndFiltered(FilterEntity filter, int? page, Guid userId)
        {
            var userOrganization = await _dB.Organizations
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == userId);

            string role = "";

            if (userOrganization == null)
            {
                return null;
            }

            IQueryable<EventBase> eventsQuery = _dB.EventsBase
                .AsQueryable();
                /*.Include(x => x.Finance)
                //.Include(x => x.FeedBack)
                //.Include(x => x.InterAgencyCooperations)
                //.Include(x => x.Theme)
                //.Include(x => x.Organizer)*/

            if(userOrganization.Municipality == Municipalities.noMunicipality) // atk_khmao
            {
                role = "atk_khmao";

            }
            else if (userOrganization.Name.ToString().StartsWith("atk_"))
            {
                eventsQuery = eventsQuery.Where(e => e.Organizer!.Municipality == userOrganization.Municipality);
                role = "atk_municipality";
            }
            else if (userOrganization.Name.ToString().Contains("_dep_"))
            {
                eventsQuery = eventsQuery.Where(e => e.Organizer!.Name == userOrganization.Name);
                role = "department";
            }


            if (filter.Municipality != null)
            {
                Municipalities? municipality = EnumHelper
                                                .GetEnumValueFromEnumMember<Municipalities>(filter.Municipality);

                eventsQuery = eventsQuery.Where(x => x.Organizer!.Municipality == municipality);
            }
            if (filter.Organization != null)
            {
                StructuredOrganizations? organization = EnumHelper
                                                .GetEnumValueFromEnumMember<StructuredOrganizations>(filter.Organization);
                
                eventsQuery = eventsQuery.Where(x => x.Organizer!.Name == organization);
            }
            if (filter.Theme != null)
            {
                eventsQuery = eventsQuery.Where(x => x.Theme!.Code == filter.Theme);
            }





            if (filter.Level != null)
            {
                LevelType? levelType = EnumHelper.GetEnumValueFromEnumMember<LevelType>(filter.Level);

                eventsQuery = eventsQuery
                    .OfType<EventForm1>()
                    .Where(x => x.LevelType == levelType);
            }

            if (filter.BestPractice != null)
            {
                eventsQuery = eventsQuery
                    .OfType<EventForm1>()
                    .Where(x => x.IsBestPractice == filter.BestPractice);
            }

            if (filter.Important != null)
            {
                eventsQuery = eventsQuery
                    .OfType<EventForm1>()
                    .Where(x => x.IsValuable == filter.Important);
            }




            if (filter.PeerFormat != null)
            {
                eventsQuery = eventsQuery
                    .OfType<EventForm1>()
                    .Where(x => !string.IsNullOrWhiteSpace(x.EqualToEqualDescription));
            }

            bool hasDateFrom = DateOnly.TryParse(filter.DateFrom, out DateOnly dateFrom);
            bool hasDateTo = DateOnly.TryParse(filter.DateTo, out DateOnly dateTo);



            if (hasDateFrom)
            {
                eventsQuery = eventsQuery.Where(x => x.Date > dateFrom);
            }
            if (hasDateTo)
            {
                eventsQuery = eventsQuery.Where(x => x.Date < dateTo);
            }



            if (filter.Interagency != null)
            {
                eventsQuery = eventsQuery
                    .OfType<EventForm1>()
                    .Where(x => x.InterAgencyCooperations.Count > 0);
            }

            if (filter.Feedback != null)
            {
                eventsQuery = eventsQuery
                    .OfType<EventForm1>()
                    .Where(x => x.FeedBack != null);
            }
            if (filter.Financing != null)
            {
                eventsQuery = eventsQuery
                    .OfType<EventForm1>()
                    .Where(x => x.Finance != null);
            }



            int pageNumber = page ?? 1;
            int entitiesToSkip = (pageNumber - 1) * 10;


            double totalEntitiesCount = await eventsQuery.CountAsync();


            var result = await eventsQuery
                .OrderBy(x => x.Theme!.Code)
                .Skip(entitiesToSkip)
                .Take(10)
            .Select(x => new ShortEventResponse
            {
                Id = x.Id,
                ThemeCode = x.Theme!.Code,
                Name = x.Name,
                ParticipantsCount = x.Categories!.Sum(x => x.Count),
                Content = x.Content,
                OrganizerName = x.Organizer!.Name,

                Date = x.Date == null ?
                        string.Empty :
                        $"{x.Date.Value.Day} {GetMonth(x.Date.Value.Month)} {x.Date.Value.Year}",

                Links = x.MediaLinks
                        .Select(x => x.Content)
                        .ToArray(),

            })
            .ToListAsync();

            //new { data = events, totalPages = events.Count }
            return new { role, data = result, totalPages = Math.Ceiling(totalEntitiesCount / 10) };
        }



        public async Task<List<Theme>> GetThemes() => await _dB.Themes
            .OrderBy(t => t.Code)
            .ToListAsync();



        //private static IOrderedQueryable<EventBase> ApplyOrdering(IQueryable<EventBase> query, IOrderedQueryable<EventBase>? orderedQuery, Expression<Func<EventBase, dynamic>> expression, bool orderBy, ref bool isFirst)
        //{
        //    if (isFirst)
        //    {
        //        orderedQuery = orderBy ? query.OrderBy(expression) : query.OrderByDescending(expression);
        //        isFirst = false;
        //    }
        //    else
        //    {
        //        orderedQuery = orderBy ? orderedQuery!.ThenBy(expression) : orderedQuery!.ThenByDescending(expression);
        //    }

        //    return orderedQuery!;
        //}



        private (DateOnly?, Guid) GetThemeIdAndDate(string? dateRequest, string themeCode)
        {
            var canParseDate = DateOnly.TryParse(dateRequest, out DateOnly eventDate);

            Guid themeId = _dB.Themes
                .AsNoTracking()
                .SingleOrDefault(t => t.Code == themeCode)!
                .Id;

            if (canParseDate)
                return (eventDate, themeId);

            return (null, themeId);
        }



        private async Task CreateMediaLinkAsync(CreateMediaLinkRequest? createMediaLinkRequest, Guid eventId)
        {
            if (createMediaLinkRequest is not null &&
                createMediaLinkRequest.Content.Count > 0)
            {
                foreach (string link in createMediaLinkRequest?.Content!)
                {
                    var mediaLink = MediaLink.Create(link, null, eventId);

                    if (mediaLink != null)
                        await _dB.MediaLinks.AddAsync(mediaLink);
                }
            }
        }



        private async Task CreateParticipantsAsync(CreateParticipantsRequest? createParticipantsRequest, Guid eventId)
        {
            if (createParticipantsRequest is not null)
            {
                if (createParticipantsRequest.SelectedCategories.Count > 0)                 // Для фиксированных выбранных категорий
                {
                    foreach (SelectedCategory categoryRequest in createParticipantsRequest.SelectedCategories)
                    {
                        //Categories? resultCategory = EnumHelper.GetEnumValueFromEnumMemberValue<Categories>(categoryRequest.Name);
                        string? resultCategory = EnumHelper.GetEnumMemberValueByName<Categories>(categoryRequest.Name);


                        Category? category = Category.Create(resultCategory ?? "undefined", categoryRequest.Count, eventId);
                        
                        if (category != null)
                            await _dB.Categories.AddAsync(category);
                    }
                }
                if (createParticipantsRequest.CustomCategories.Count > 0)              // Для созданных кастомных категорий
                {
                    foreach (CustomCategory customCategory in createParticipantsRequest?.CustomCategories!)
                    {
                        Category? category = Category.Create(customCategory.Label, customCategory.Count, eventId);

                        if (category != null)
                            await _dB.Categories.AddAsync(category);
                    }
                }
                else
                {
                    // add just total row info into db
                    Category? category = Category.Create("ВСЕГО", createParticipantsRequest.Total, eventId);

                    if (category != null)
                        await _dB.Categories.AddAsync(category);
                }
            }
        }



        private async Task CreateFinanceAsync(CreateFinanceRequest? createFinanceRequest, Guid eventId)
        {
            if (createFinanceRequest is not null)
            {
                var finance = new Finance(createFinanceRequest.MunicipalBudget,
                    createFinanceRequest.RegionalBudget,
                    createFinanceRequest.GranteBudget,
                    createFinanceRequest.OtherBudget,
                    createFinanceRequest.Description,
                    eventId);

                if (finance != null)
                    await _dB.Finances.AddAsync(finance);
            }
        }



        private async Task CreateFeedBackAsync(CreateFeedBackRequest? createFeedBackRequest, Guid eventId)
        {
            if (createFeedBackRequest is not null)
            {
                bool hasInterview = createFeedBackRequest
                    .FeedBackTypes.Contains(FeedBackTypes.Interview);

                bool hasGuestionnare = createFeedBackRequest
                    .FeedBackTypes.Contains(FeedBackTypes.Guestionnaire);

                bool hasOnline = createFeedBackRequest
                    .FeedBackTypes.Contains(FeedBackTypes.Internet);

                bool hasOpros = createFeedBackRequest
                    .FeedBackTypes.Contains(FeedBackTypes.Opros);

                bool hasOther = createFeedBackRequest
                    .FeedBackTypes.Contains(FeedBackTypes.Other);

                string? description = createFeedBackRequest.Description;

                var feedBack = FeedBack.Create(hasInterview, hasGuestionnare,
                                               hasOnline, hasOpros,
                                               hasOther, description,
                                               eventId);


                if (feedBack != null)
                    await _dB.FeedBacks.AddAsync(feedBack);
            }
        }



        private async Task CreateInterAgencyCoopAsync(CreateInterAgencyCooperationRequest? createInterAgencyCoopRequest, 
            Guid eventId)
        {
            if (createInterAgencyCoopRequest is not null)
            {
                if (createInterAgencyCoopRequest.CustomOrganizations.Count > 0)             // custom организации
                {
                    foreach (var interAgencyCoopRequest in createInterAgencyCoopRequest.CustomOrganizations)
                    {
                        var interAgencyCoop = InterAgencyCooperation.Create(interAgencyCoopRequest.Name, CoOpOrganiations.Custom,
                            interAgencyCoopRequest.Role, interAgencyCoopRequest.Description, eventId);
                        
                        if(interAgencyCoop != null)
                            await _dB.InterAgencyCooperations.AddAsync(interAgencyCoop);
                    }
                }


                if (createInterAgencyCoopRequest.SelectedOrganizations.Count > 0)           // выбранные фиксированные организации
                {
                    foreach (var interAgencyCoopRequest in createInterAgencyCoopRequest.SelectedOrganizations)
                    {
                        var resultAgency = EnumHelper.GetEnumValueFromEnumMemberValue<CoOpOrganiations>(interAgencyCoopRequest.Key);

                        var interAgencyCoop = InterAgencyCooperation.Create(interAgencyCoopRequest.Key ?? null, 
                            resultAgency ?? CoOpOrganiations.Undefined,
                            interAgencyCoopRequest.Value.Role, 
                            interAgencyCoopRequest.Value.Description, 
                            eventId);

                        if (interAgencyCoop != null)
                            await _dB.InterAgencyCooperations.AddAsync(interAgencyCoop);
                    }
                }
            }
        }



        private async Task CreateAgreementAsync(CreateAgreementRequest? createAgreementRequest, Guid eventId)
        {
            if (createAgreementRequest is not null &&
                createAgreementRequest.Agreements.Count > 0)
            {
                foreach (var agreementRequest in createAgreementRequest.Agreements)
                {
                    string? resultAgreement = EnumHelper.GetEnumMemberValueByName<OrganizationEnum>(agreementRequest.Name);

                    var agreement = Agreement.Create(agreementRequest.Description,
                                                    resultAgreement ?? agreementRequest.Name,
                                                    agreementRequest.Result,
                                                    eventId);

                    if (agreement != null)
                        await _dB.Agreements.AddAsync(agreement);
                }
            }
        }



        private async Task CreateAudienceAsync(CreateAudienceRequest? createAudienceRequest, Guid eventId)
        {
            if (createAudienceRequest is not null)
            {
                foreach (var audienceCategory in createAudienceRequest.Audiences)
                {
                    string? resultAudience = EnumHelper.GetEnumMemberValueByName<Audiences>(audienceCategory);

                    var audience = Audience.Create(resultAudience ?? audienceCategory, eventId);

                    if (audience != null)
                        await _dB.Audiences.AddAsync(audience);
                }

            }
        }



        private async Task CreateSupportAsync(CreateSupportRequest? createSupportRequest, Guid eventId)
        {
            if (createSupportRequest is not null)
            {
                var supportsDTOList = createSupportRequest.Supports;

                foreach (var supportDTO in supportsDTOList)
                {
                    var support = new Support(createSupportRequest.Supported, supportDTO.Key, supportDTO.Description, eventId);
                
                    if(support != null)
                        await _dB.Supports.AddAsync(support);
                }
            }
        }



        private async Task CreateSourceOfDestinationAsync(CreateSourcesOfDistributionRequest? createSourcesOfDistributionRequest, Guid eventId)
        {
            if (createSourcesOfDistributionRequest is not null &&
                createSourcesOfDistributionRequest.Links.Count > 0)
            {
                foreach (var linkDto in createSourcesOfDistributionRequest.Links)
                {
                    var mediaLink = MediaLink.Create(linkDto.Link, linkDto.Name, eventId);

                    if (mediaLink != null)
                        await _dB.MediaLinks.AddAsync(mediaLink);
                }
            }
        }
        

        
        private async Task CreateViolationAsync(CreateViolationsRequest? createViolationsRequest, Guid eventId)
        {
            if (createViolationsRequest is not null)
            {
                var violationsDTOList = createViolationsRequest.Violations;

                foreach (var violationsDTO in violationsDTOList)
                {

                    //string key = 
                    //    violationsDTO.Key == Violations.law_other ? 
                    //    violationsDTO.Value.OtherText ?? "" : 
                    //    violationsDTO.Key.ToString();


                    string? resultViolation = EnumHelper.GetEnumMemberValueByName<Violations>(violationsDTO.Key.ToString());



                    var violation = Violation.Create(resultViolation ?? violationsDTO.Value.OtherText ?? "",
                        violationsDTO.Value.SentCount, violationsDTO.Value.BlockedCount, 
                        violationsDTO.Value.OrderNumber, eventId);


                    if (violation != null)
                        await _dB.Violations.AddAsync(violation);
                }
            }
        }



        private async Task CreateConcourseAsync(CreateConcourseRequest? createConcourseRequest, Guid eventId)
        {
            if (createConcourseRequest is not null)
            {
                var concourse = new Concourse(createConcourseRequest.Description, 
                    createConcourseRequest.Result, 
                    createConcourseRequest.Details,
                    eventId);
                
                if (concourse != null)
                        await _dB.Concourses.AddAsync(concourse);
            }
        }
        


        private static string GetMonth(int month)
        {
            return month switch
            {
                1 => "января",
                2 => "феврая",
                3 => "марта",
                4 => "апреля",
                5 => "мая",
                6 => "июня",
                7 => "июля",
                8 => "августа",
                9 => "сентября",
                10 => "октября",
                11 => "ноября",
                12 => "декабря",
                _ => string.Empty,
            };
        }
    }
}
