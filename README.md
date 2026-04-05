# Todo API

REST API на ASP.NET Core с JWT-аутентификацией и PostgreSQL.

## Технологии

- ASP.NET Core 10
- Entity Framework Core 10
- PostgreSQL
- JWT (JSON Web Tokens)
- Swagger / OpenAPI
- BCrypt для хеширования паролей

## Возможности

- Регистрация и вход пользователей (JWT)
- Создание, просмотр, редактирование и удаление задач/записей
- Каждый пользователь видит только свои задачи
- Swagger документация

## Установка и запуск

### Требования

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Git](https://git-scm.com/)

### Шаги

1. Клонируй репозиторий:
   ```bash
   git clone https://github.com/ТВОЙ_ЛОГИН/TodoApi.git
   cd TodoApi
   
2. Настрой подключение к базе данных в appsettings.json:
    "ConnectionStrings": 
    {
        "DefaultConnection": "Host=localhost;Port=5432;Database=TodoDb;Username=postgres;Password=ВАШ_ПАРОЛЬ_БД"
    }

3. Установи пакеты и создай базу данных (Пропиши команды в консоли диспечера пакетов):

    dotnet restore
    dotnet ef database update
    
4. Запусти проект:

    dotnet run

5. Открой Swagger: https://localhost:7295/swagger


## API Эндпоинты

### Auth
| Метод | Эндпоинт | Описание |
|-------|----------|----------|
| POST | `/api/Auth/register` | Регистрация |
| POST | `/api/Auth/login` | Вход (получение токена) |

### TodoItems 
| Метод | Эндпоинт | Описание |
|-------|----------|----------|
| GET | `/api/TodoItems` | Все задачи |
| GET | `/api/TodoItems/{id}` | Задача по ID |
| GET | `/api/TodoItems/incomplete` | Невыполненные |
| POST | `/api/TodoItems` | Создать задачу |
| PUT | `/api/TodoItems/{id}` | Обновить |
| DELETE | `/api/TodoItems/{id}` | Удалить |

## Автор
Намазов Сохбат Аким оглы
C# / .NET разработчик

