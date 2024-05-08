**REST API сервис заказной системы интернет магазина**

Стек Технологий:

- C#
- EntityFramework
- PostgreSQL
- Swagger
- Кэширование данных

Входные данные проходят валидацию:
•	Номер телефона в 10-тизначном формате
•	ФИО только на русском языке

Сервис реализовывает следующие методы:
1)	Метод добавления клиента.
2)	Метод получения клиента по номеру телефона.
3)	Метод получения списка товаров, с возможностью фильтрации по типу товара и/или по наличию на складе и сортировки по цене (возрастанию и убыванию).
4)	Метод получения списка заказов по конкретному клиенту за выбранный временной период, отсортированный по дате создания.
5)	Метод формирования заказа с проверкой наличия требуемого количества товара на складе, а также уменьшение доступного количества товара на складе в БД в случае успешного создания заказа.

Методы сервиса обрабатывают внештатные ситуации, а исключения залогированы.

Для ускорения выдачи результатов для повторных вызовов, реализовано кэширование данных.

Сервис API  задокументирован с помощью Swagger.
