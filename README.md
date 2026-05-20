# Волонтёрский центр РТУ МИРЭА

Веб-приложение для управления волонтёрским центром РТУ МИРЭА.  
Курсовой проект по дисциплине «Кроссплатформенная среда исполнения программного обеспечения».

## Технологический стек

| Компонент | Технология |
|-----------|-----------|
| Backend / UI | ASP.NET Core + Blazor Server (.NET 10) |
| ORM | Entity Framework Core 10 (Code-First, миграции) |
| База данных | SQLite |
| Стилизация | Bootstrap 5 + Bootstrap Icons + кастомная CSS-тема |
| Контейнеризация | Docker / Docker Compose |

## Функциональность

- **Главная страница** — hero-секция, статистика, ближайшие мероприятия, руководство ВЦ, контакты
- **Все мероприятия** — список за текущий месяц, прошедшие выделены серым, предстоящие — зелёным; поиск с подсветкой на странице
- **Мероприятие** — детали, куратор, запись (для волонтёров)
- **Новости** — авто-прокручиваемый карусель
- **Экспедиции** — список с подачей заявки на участие
- **Заказ СМИ** — форма для кураторов
- **Школа волонтёра** — информационная страница
- **Стать волонтёром** / **Запрос волонтёров** — формы с сохранением в БД
- **Личный кабинет волонтёра** — профиль, аватар, приглашения, запись/отмена мероприятий, достижения, экспедиции, выгрузка в Excel
- **Личный кабинет куратора** — профиль, уведомления об отказах, приглашение волонтёров, выгрузка мероприятий в Excel, достижения

## Схема базы данных

```
Volunteer ──── Specialist (CuratorId, nullable)
Volunteer ──── EventRegistration ──── Event
Volunteer ──── EventInvitation  ──── Event
Volunteer ──── EventInvitation  ──── Specialist (Curator)
Volunteer ──── VolunteerBeneficiary ──── Beneficiary
Volunteer ──── ExpeditionParticipation ──── Expedition
Volunteer ──── UserAchievement
Specialist ──── Event (CuratorId)
Specialist ──── Expedition (LeaderId)
Specialist ──── CuratorNotification
```

## Запуск локально

### Требования
- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Шаги

```bash
# Клонировать репозиторий
git clone <repo-url>
cd VolunteerCenterCurse

# Восстановить зависимости
dotnet restore

# Применить миграции и запустить
dotnet run
```

Приложение будет доступно по адресу: **http://localhost:5050**

### Тестовые аккаунты

| Роль | Email | Пароль |
|------|-------|--------|
| Волонтёр | sidorova@edu.mirea.ru | vol123 |
| Куратор | naumov@mirea.ru | curator123 |

## Запуск через Docker

### Требования
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Шаги

```bash
# Собрать и запустить
docker compose up --build

# Запуск в фоне
docker compose up -d --build
```

Приложение будет доступно по адресу: **http://localhost:5050**

База данных сохраняется в Docker volume `db-data` — данные сохраняются между перезапусками.

```bash
# Остановить
docker compose down

# Остановить и удалить данные
docker compose down -v
```

## Миграции EF Core

```bash
# Добавить новую миграцию
dotnet ef migrations add <MigrationName>

# Применить миграции вручную
dotnet ef database update

# Откатить последнюю миграцию
dotnet ef migrations remove
```

Миграции применяются автоматически при старте приложения через `db.Database.Migrate()`.

## Структура проекта

```
VolunteerCenterCurse/
├── Components/
│   ├── Layout/          # MainLayout, NavMenu
│   └── Pages/           # Blazor-страницы
├── Data/
│   └── AppDbContext.cs  # EF Core контекст + Fluent API
├── Migrations/          # EF Core миграции
├── Models/              # Сущности БД
├── Services/            # Бизнес-логика
├── wwwroot/
│   ├── css/app.css      # Кастомная зелёная тема
│   └── js/site.js       # JS-хелперы (поиск, скачивание)
├── Dockerfile
├── docker-compose.yml
└── Program.cs
```
