using VolunteerCenterCurse.Data;
using VolunteerCenterCurse.Models;

namespace VolunteerCenterCurse.Services;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (db.Specialists.Any()) return;

        string Hash(string pwd) => AuthService.HashPassword(pwd);

        var spec1 = new Specialist
        {
            FullName = "Наумов Александр Андреевич",
            Position = "Председатель волонтёрского центра",
            Email = "naumov@mirea.ru",
            Phone = "+7 (958) 801-24-02",
            PasswordHash = Hash("curator123")
        };
        var spec2 = new Specialist
        {
            FullName = "Петрова Елена Дмитриевна",
            Position = "Заместитель председателя",
            Email = "petrova@mirea.ru",
            Phone = "+7 (495) 765-43-21",
            PasswordHash = Hash("curator456")
        };
        db.Specialists.AddRange(spec1, spec2);
        await db.SaveChangesAsync();

        var vol1 = new Volunteer
        {
            FullName = "Сидорова Мария Александровна",
            Email = "sidorova@student.mirea.ru",
            Phone = "+7 (800) 555-35-35",
            BirthDate = new DateTime(2005, 3, 15),
            Description = "Активный волонтёр, участвую в мероприятиях с 2023 года. Помогаю пожилым людям и детям.",
            CuratorId = spec1.Id,
            PasswordHash = Hash("vol123")
        };
        var vol2 = new Volunteer
        {
            FullName = "Козлов Артём Дмитриевич",
            Email = "kozlov@student.mirea.ru",
            Phone = "+7 (928) 322-53-38",
            BirthDate = new DateTime(2004, 11, 8),
            Description = "Волонтёр экологических проектов. Участвовал в трёх экспедициях.",
            CuratorId = spec1.Id,
            PasswordHash = Hash("vol456")
        };
        var vol3 = new Volunteer
        {
            FullName = "Морозова Екатерина Павловна",
            Email = "morozova@student.mirea.ru",
            Phone = "+7 (255) 352-25-55",
            BirthDate = new DateTime(2006, 1, 22),
            Description = "Новый волонтёр. Хочу помогать людям и участвовать в городских акциях.",
            CuratorId = spec2.Id,
            PasswordHash = Hash("vol789")
        };
        db.Volunteers.AddRange(vol1, vol2, vol3);
        await db.SaveChangesAsync();

        var beneficiary1 = new Beneficiary
        {
            FullName = "Пожилой центр «Забота»",
            Address = "г. Москва, ул. Профсоюзная, д. 45",
            Description = "Помощь пожилым людям: прогулки, покупки, общение",
            Phone = "+7 (495) 565-89-92"
        };
        var beneficiary2 = new Beneficiary
        {
            FullName = "Детский дом № 17",
            Address = "г. Москва, ул. Зеленоградская, д. 12",
            Description = "Помощь в организации досуга детей",
            Phone = "+7 (495) 672-32-22"
        };
        db.Beneficiaries.AddRange(beneficiary1, beneficiary2);
        await db.SaveChangesAsync();

        db.VolunteerBeneficiaries.AddRange(
            new VolunteerBeneficiary { VolunteerId = vol1.Id, BeneficiaryId = beneficiary1.Id },
            new VolunteerBeneficiary { VolunteerId = vol2.Id, BeneficiaryId = beneficiary1.Id },
            new VolunteerBeneficiary { VolunteerId = vol3.Id, BeneficiaryId = beneficiary2.Id }
        );
        await db.SaveChangesAsync();

        var events = new[]
        {
            new Event
            {
                Title = "День донора в МИРЭА",
                Description = "Акция по сдаче крови совместно со Станцией переливания крови Москвы. Все желающие могут стать донорами.",
                Date = new DateTime(2026, 5, 5, 10, 0, 0),
                Location = "г. Москва, пр. Вернадского, 78, корпус А",
                CuratorId = spec1.Id
            },
            new Event
            {
                Title = "Субботник в парке Горького",
                Description = "Городская экологическая акция по уборке парка. Берём с собой перчатки и хорошее настроение!",
                Date = new DateTime(2026, 5, 18, 9, 0, 0),
                Location = "г. Москва, парк им. Горького",
                CuratorId = spec2.Id
            },
            new Event
            {
                Title = "Помощь детскому дому № 17",
                Description = "Организация праздника для детей: конкурсы, мастер-классы, подарки. Нужны волонтёры с творческими навыками.",
                Date = new DateTime(2026, 5, 25, 12, 0, 0),
                Location = "г. Москва, ул. Зеленоградская, д. 12",
                CuratorId = spec1.Id
            },
            new Event
            {
                Title = "Экологическая акция «Чистый берег»",
                Description = "Уборка берега реки Москвы в районе Коломенского. Снаряжение предоставляется.",
                Date = new DateTime(2026, 5, 28, 10, 0, 0),
                Location = "г. Москва, Коломенское, берег реки",
                CuratorId = spec2.Id
            },
            new Event
            {
                Title = "Праздник для пожилых «Тепло сердец»",
                Description = "Концерт и чаепитие для пожилых людей пансионата «Забота». Нужны волонтёры с музыкальными талантами.",
                Date = new DateTime(2026, 6, 5, 14, 0, 0),
                Location = "г. Москва, ул. Профсоюзная, д. 45",
                CuratorId = spec1.Id
            },
            new Event
            {
                Title = "Помощь в городской библиотеке",
                Description = "Разбор и каталогизация книжного фонда. Работа в команде, вход свободный.",
                Date = new DateTime(2026, 5, 30, 11, 0, 0),
                Location = "г. Москва, ул. Академика Опарина, 4",
                CuratorId = spec2.Id
            },
            new Event
            {
                Title = "Квест «Память поколений»",
                Description = "Городской квест к Дню Победы. Участники узнают об истории Москвы военных лет.",
                Date = new DateTime(2026, 6, 12, 11, 0, 0),
                Location = "г. Москва, Красная площадь (старт)",
                CuratorId = spec1.Id
            }
        };
        db.Events.AddRange(events);
        await db.SaveChangesAsync();

        db.EventRegistrations.AddRange(
            new EventRegistration { VolunteerId = vol1.Id, EventId = events[0].Id },
            new EventRegistration { VolunteerId = vol1.Id, EventId = events[1].Id },
            new EventRegistration { VolunteerId = vol2.Id, EventId = events[1].Id },
            new EventRegistration { VolunteerId = vol1.Id, EventId = events[2].Id },
            new EventRegistration { VolunteerId = vol3.Id, EventId = events[4].Id }
        );
        await db.SaveChangesAsync();

        var expeditions = new[]
        {
            new Expedition
            {
                Title = "Экспедиция «Байкал-2026»",
                Description = "Волонтёрская экспедиция на озеро Байкал. Работы по очистке берегов и изучению экосистемы. Требуется базовая физическая подготовка.",
                StartDate = new DateTime(2026, 7, 5),
                EndDate = new DateTime(2026, 7, 19),
                Location = "Иркутская область, побережье Байкала",
                LeaderId = spec1.Id,
                HousingConditions = "Палаточный лагерь, горячее питание организовано, душ раз в 2 дня",
                MaxParticipants = 25
            },
            new Expedition
            {
                Title = "Экспедиция «Карелия — край озёр»",
                Description = "Экологическая экспедиция в карельские леса. Мониторинг состояния водоёмов, посадка деревьев, помощь местным жителям.",
                StartDate = new DateTime(2026, 8, 10),
                EndDate = new DateTime(2026, 8, 24),
                Location = "Республика Карелия, г. Петрозаводск и окрестности",
                LeaderId = spec2.Id,
                HousingConditions = "База отдыха, двухместные комнаты, трёхразовое питание",
                MaxParticipants = 20
            }
        };
        db.Expeditions.AddRange(expeditions);
        await db.SaveChangesAsync();

        var newsItems = new[]
        {
            new News
            {
                Title = "Наши волонтёры получили награды на форуме «ДОБРО.РФ»",
                Content = "На всероссийском форуме добровольцев «ДОБРО.РФ» в Москве волонтёры РТУ МИРЭА были отмечены специальными наградами за вклад в экологические проекты. Сидорова Мария получила звание «Волонтёр года Москвы 2025».",
                PublishedAt = new DateTime(2026, 5, 10),
                IsActive = true
            },
            new News
            {
                Title = "Открыт набор на летние экспедиции 2026 года",
                Content = "Волонтёрский центр РТУ МИРЭА объявляет набор участников на летние экспедиции «Байкал-2026» и «Карелия — край озёр». Заявки принимаются до 1 июня. Подробности в разделе «Экспедиции».",
                PublishedAt = new DateTime(2026, 5, 15),
                IsActive = true
            },
            new News
            {
                Title = "День донора прошёл с рекордным числом участников",
                Content = "5 мая волонтёры МИРЭА провели акцию «День донора». В ней приняли участие 87 студентов и сотрудников университета — это рекорд за всю историю ВЦ МИРЭА. Собранная кровь поступила в московские больницы.",
                PublishedAt = new DateTime(2026, 5, 6),
                IsActive = true
            },
            new News
            {
                Title = "Волонтёрский центр МИРЭА — лауреат премии «Доброволец России»",
                Content = "Наш центр стал лауреатом национальной премии «Доброволец России» в номинации «Студенческое добровольчество». Спасибо всем волонтёрам за ваш труд и преданность делу!",
                PublishedAt = new DateTime(2026, 4, 20),
                IsActive = true
            }
        };
        db.News.AddRange(newsItems);
        await db.SaveChangesAsync();
    }
}
