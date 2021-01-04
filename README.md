# Как пользоваться репозиторием
- Создайте fork репозитория
- Начните с ветки step-1, прочитайте пункт 1 этого readme
- Прочтиайте второй пункт этого readme.  Обратите внимание на подпункты "задача" и "что делаем"
- Переключитесь на ветку step-2, посмотрите на результат рефакторинга
- Вернитесь на ветку step-1 и проведите рефакторинг
- Повторите процесс для веток step-2 - step-9
- Каждый следующий шаг более сложен и дорог в реализации. В реальных проектах используйте инструменты там, где действительно необходимо. 

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
- Или применить одну из них :)

### Что делаем
- Создать папку Features
- Внтури создать папку на каждую "фичу"
- Разложить ```DTO```, ```Handler```, ```Controller``` по фича-папкам
- Использовать базовый класс ```GetEnumerableQueryHandlerBase``` для стека чтения
- Автоматически вызывать ```SaveChanges``` в стеке записи

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
- [Scrutor](https://github.com/khellang/Scrutor)

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

### Что делаем
- Создать сборку на для каждого модуля
- Перенести фича-папки в соответствущие сборки

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

### Что делаем
- Ипользовать базоый класс ```EntityBase```
- Создать специализированные конструкторы, соблюдающие инварианты
- Изменить модификаторы доступа свойств на ```public get; protected set;```  
- Создать публичные методы вместо публичных property setters
- Создать спецификации
- Удалить foreign key `<FK>Id` properties

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
- Дублирование кода в валидаторах/контроллерахc
- Повторные загрузки сущностей
- Создание обобщенных валидаторов

## Задача
- Использовать фабрику контекстов
- Использовать обобщенные валидаторы 

### Что делаем
- Добавить в параметры методов контроллеров ```Func<T, TObjectContext<T>>```
- Использовать фабрику для инициализации контекстов операций
- Добавить ```DataAnnotation``` атрибуты на обязательные поля
- Использовать ```DataAnnotationValidator``` по-умолчанию

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

### Что делаем
- Создать класс на каждое состояние заказа, распределеить методы между классами-состояниями
- Использовать контекст для валидации

## Ссылки
- [Designing with types: Making illegal states unrepresentable](https://fsharpforfunandprofit.com/posts/designing-with-types-making-illegal-states-unrepresentable/) 
- [Шаблон проектирования «состояние» двадцать лет спустя](https://habr.com/ru/post/341134/)

## Плюсы
- Компилируется - значит работает
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

### Что делаем
- Использовать ```IDomainHandler``` в качестве ```Domain Service```
- Использовать ```CommandResult``` для распределенных операций, которые могут завершиться ошибкой

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
- Добавить обработку событий в пайплайн

### Что делаем
- Запрограммировать диспетчер
- Добавить шаг с обработкой событий в пайплайн

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
- Реализовать массовую операцию с помощью [```Bulk Extensions```](https://github.com/borisdj/EFCore.BulkExtensions)

### Что делаем  
- Добавить публикацию события в модуле
- В другом модуле подписаться на событие
- Выполнить массовую операцию в обработчике события

## Ссылки
- [Bulk Extensions](https://github.com/borisdj/EFCore.BulkExtensions)
- [Linq2db](https://github.com/linq2db/linq2db)
 
## Плюсы
- Не нужно писать хранимые процедуры

## Минусы
- LINQ [нарушает LSP](https://blog.ploeh.dk/2012/03/26/IQueryableTisTightCoupling/)