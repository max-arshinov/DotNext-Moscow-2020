# 1. Начало: логика в контроллерах или сервисах
## Ссылки
- [Толстые Тупые Уродливые Контроллеры](https://habr.com/ru/post/175465/)
- [Tutorial: Implement CRUD Functionality - ASP.NET MVC with EF Core](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/crud?view=aspnetcore-5.0)
- [Razor Pages with Entity Framework Core in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/crud?view=aspnetcore-5.0)

## Плюсы
- Просто
- Лучшая возможная производительность

## Минусы
- Затраты на поддержку растут с количеством кода; по закону брукса разработка в какой-то момент остановится
 
# 2. Вертикальные слайсы / CQRS
## Мотивация
- Не допустить превращения кодовой базы в [Big Ball of Mud](https://en.wikipedia.org/wiki/Big_ball_of_mud) 
- [CRUD](https://walkerjordan.com/task-based-user-interfaces/) VS [Task-Based UI](http://balazblogspot.blogspot.com/2018/07/what-is-task-based-ui.html)

### 2.1 Логика в контроллерах
- нарушение [SRP](https://en.wikipedia.org/wiki/SOLID)
- смешение бизнес логики и инфраструктурного кода
- затруднено повторное использование

### 2.2. В сервисах
- `Services`, `Helpers`, `Managers`, `...` повышается связность по мере роста проекта, в итоге имеется тенденция к [Big Ball of Mud](https://en.wikipedia.org/wiki/Big_ball_of_mud) 
- [Cross-cutting concerns](https://en.wikipedia.org/wiki/Cross-cutting_concern#:~:text=Cross%2Dcutting%20concerns%20are%20parts,oriented%20programming%20or%20procedural%20programming.)
- Затруднен [Feature Toggle](https://en.wikipedia.org/wiki/Feature_toggle) и [рефакторинг](https://refactoring.guru/)
- Конфликты при мердже, не соблуюдается принцип [OCP](https://en.wikipedia.org/wiki/SOLID)
- Потеря контекста при навигации по файлам

## Задача
- Реализовать вертикальные слайсы без использования внешних библиотек, вроде `MediatR` или `SimpleInjector`
- Или применить одну из них

## Ссылки
### Доклады
- [Быстрорастворимое проектирование](https://habr.com/ru/company/jugru/blog/447308/) 
- [Vertical Slices](https://www.youtube.com/watch?v=SUiWfhAhgQw) 

### Статьи
- [CQRS. Факты и заблуждения](https://habr.com/ru/post/347908/) 
- [Meanwhile... on the command side of my architecture](https://blogs.cuttingedge.it/steven/posts/2011/meanwhile-on-the-command-side-of-my-architecture/) 
- [Meanwhile... on the query side of my architecture](https://blogs.cuttingedge.it/steven/posts/2011/meanwhile-on-the-query-side-of-my-architecture/) 
- [Report progress and cancel long running queries](https://github.com/dotnetjunkie/solidservices/issues/19#issuecomment-364044515) 
- [DDD, Hexagonal, Onion, Clean, CQRS, … How I put it all together](https://herbertograca.com/2017/11/16/explicit-architecture-01-ddd-hexagonal-onion-clean-cqrs-how-i-put-it-all-together/)

### Библиотеки
- [MediatR](https://github.com/jbogard/MediatR) 
- [Simple Injector](https://simpleinjector.org/)

### Актуальные техники 
- [Цепочка обязанностей](https://refactoring.guru/ru/design-patterns/chain-of-responsibility) 
- [Рефакторинг: раздувальщики](https://refactoring.guru/ru/refactoring/smells/bloaters) 

## Плюсы
- Меньше конфликтов
- Больше добавления и удаления кода, меньше редактирования
- Базовые хендлеры, меньше бойлерплейта
- Обобщенная обработка Cross-cutting concerns 

## Минусы
- Больше инфраструктурного кода
- Непривычно

# 3. Модули
## Мотивация
- Циклические зависимости
- Когнитивная сложность
- Затруднен рефакторинг
- Время компиляции, запуска
- Убрать явную зависимость от конкретной ORM

## Задача
Разделить проект на модули (отдельные сборки)

## Ссылки
- [Feature modules](https://angular.io/guide/feature-modules)
- [Как запустить MVP и не превратить его в техдолг](https://youtu.be/Y9NMuziC4Ms) 
- [Table splitting](https://docs.microsoft.com/en-us/ef/core/modeling/table-splitting) 
- [Domain-driven design: рецепт для прагматика](https://habr.com/ru/company/jugru/blog/440772/)

## Плюсы
- Зависимости инвертированы, нет циклических зависимостей
- Нет зависимостей от конкретной ORM в домене
- Видны зависимости между модулями

## Минусы
- Больше абстраций
- Больше инфраструктурного кода
- Нужны `Domain Events`

# 4. Rich Domain Model
## Мотивация
- Борьба с [NRE](https://habr.com/ru/post/309462/), [частичной инициализацией](https://enterprisecraftsmanship.com/posts/partially-initialized-entities-anti-pattern/)
- Код определяет допустимые и недопустимые состояния объектов

## Задача
- Применить инкапсуляцию
- Создать спецификации
- Удалить `<FK>Id`-properties
- Определить конструкторы с параметрами
- Выделить методы сущностей

## Ссылки
- [Блеск и нищета модели предметной области](https://habr.com/ru/company/jugru/blog/503868/)
- [Entity Base Class](https://enterprisecraftsmanship.com/posts/entity-base-class/)
- [Value Object: a better implementation](https://enterprisecraftsmanship.com/posts/value-object-better-implementation/)
- [In Defense of Lazy Loading](https://enterprisecraftsmanship.com/posts/defense-lazy-loading/)

## Плюсы
- Защита от NRE
- Соблюдение инвариантов
- Лучшая читаемость кода
- Следование принципам ООП

## Минусы
- Возможное дублирование валидации в конструкторах и в валидаторах
- Ухудшение производительности
- Сложность моделирования
- Куда мне класть этот метод?

# 5. Контексты операций
## Мотивация
- Дублирование кода в валидаторах/контроллера
- Повторные загрузки сущностей
- Создание обобщенных валидаторов

## Задача
- Написать фабрику контекстов
- Написать обобщенные валидаторы 

## Плюсы
- Нет повторных чтений из БД
- Обобщенная валидация

## Минусы
- Не используются [Nullable Reference Type](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references)

# 6. State
## Мотивация
- [Make illegal states unrepresentable](https://blog.janestreet.com/effective-ml-revisited/)
- [Control Flow с помощью Exception](https://habr.com/ru/post/347284/) - плохая идея

## Задача
- Объединить паттерн `State` и идеи функционального программирования
- Запрограммировать конечный автомат

## Ссылки
- [Designing with types: Making illegal states unrepresentable](https://fsharpforfunandprofit.com/posts/designing-with-types-making-illegal-states-unrepresentable/) 
- [Шаблон проектирования «состояние» двадцать лет спустя](https://habr.com/ru/post/341134/)

## Плюсы
- Комплируется - значит работает
- Переходы между состояниями и правила - явные

## Минусы
- Смешение ответственности между валидаторами и `State`

# 7. Domain Services
## Мотивация
- [The CAP theorem of domain modeling](https://medium.com/@vkhorikov/the-cap-theorem-of-domain-modeling-2e3763301caf)
- [Asynchronous Injection](https://blog.ploeh.dk/2019/02/11/asynchronous-injection/) 
- Не тестировать моки

## Задача
- Перенести работу с IO в отдельный слой
- Не потерять преимущества обобщенных интерфейсов, но сохранить холистические абстракции

## Ссылки
- [Holistic Abstractions](http://scrapbook.qujck.com/holistic-abstractions-take-2/)

## Плюсы
- Более формальные правила структурирования кода 
- Упрощение тестирования

## Минусы
- Фрагментация логики

# 8. Domain Events
## Мотивация
- Отделение ~~мух от котлет~~ основного процесса от побочных эффектов
- Коммуникация между модулями
- Упрощение перехода к шине данных и микросервисам
- [Аудит лог](https://martinfowler.com/eaaDev/AuditLog.html)

## Задача
- Запрограммировать диспетчер
- Заменить прямую работу с `DbContext` на `UnitOfWork`

## Ссылки
- [Domain events: design and implementation](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation)
- [Better domain event pattern](https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/)
- [Multiple dispatch в C#](https://habr.com/ru/post/283522/)
- [Event storming](https://www.eventstorming.com/) 

## Плюсы
- Слабая связанность

## Минусы
- Неявность
- Управление транзакциями
- События в обработчиках событий
- Синхронные и асинхронные обработчики

# 9. Массовые операции
## Мотивация
- Производительность: импорт, экспорт, массовое обновление, денормализация

## Задача
- Реализовать массовую операцию
- Подписаться на событие одного модуля и обработать его в другом

## Ссылки
- [Bulk Extensions](https://github.com/borisdj/EFCore.BulkExtensions)
- [Linq2db](https://github.com/linq2db/linq2db)
 
## Плюсы
- Не нужно писать хранимые процедуры

## Минусы
- LINQ [нарушает LSP](https://blog.ploeh.dk/2012/03/26/IQueryableTisTightCoupling/)