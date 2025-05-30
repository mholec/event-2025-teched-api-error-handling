# TechEd 2025 - Workshops API
## Ukázka práce s chybovými stavy v ASP.NET Core Minimal APIs

Tato aplikace je ukázkou pro konferenci TechEd 2025, která demonstruje pokročilé techniky řešení chybových stavů v ASP.NET Core Minimal APIs. Aplikace implementuje systém pro správu workshopů a registrací účastníků s komplexním zpracováním chyb.

## Popis aplikace

Aplikace poskytuje REST API pro správu vzdělávacích workshopů a registrací účastníků. Hlavní funkcionalita zahrnuje:

- **Správa workshopů**: Vytváření, zobrazení, aktualizace a mazání workshopů
- **Správa registrací**: Zobrazení a rušení registrací účastníků na workshopy
- **Pokročilé zpracování chyb**: Různé typy výjimek a jejich standardizované zpracování
- **Validace dat**: Robustní validace vstupních dat pomocí FluentValidation

## Technologie a architektura

### Použité technologie
- **.NET 8.0**: Hlavní runtime pro aplikaci
- **ASP.NET Core Minimal APIs**: Pro definici API endpointů
- **Entity Framework Core**: Pro přístup k datům
- **SQLite**: Databáze pro ukládání dat
- **FluentValidation**: Pro validaci vstupních dat
- **Problem Details (RFC 7807)**: Pro standardizované chybové odpovědi

### Architektura
```
Workshops.WebApi/           # Hlavní webové API
├── Endpoints/             # Definice API endpointů
├── ErrorHandling/         # Systém zpracování chyb
│   ├── Exceptions/        # Vlastní výjimky
│   ├── Handlers/          # Exception handlery
│   └── Filters/           # Endpoint filtry
├── Contracts/             # Data transfer objekty
├── Mapping/               # Mapování mezi entitami a DTO
├── Routing/               # Vlastní route constraints
└── HostedServices/        # Background služby

Workshops.Data/            # Datová vrstva
├── Entities/              # Databázové entity
└── AppDbContext.cs        # EF Core kontext
```

## API Endpointy

### Workshopy
- `GET /workshops` - Seznam všech workshopů s filtrováním a stránkováním
- `GET /workshops/{id}` - Detail konkrétního workshopu dle ID
- `POST /workshops` - Vytvoření nového workshopu
- `PUT /workshops/{id}` - Aktualizace existujícího workshopu
- `DELETE /workshops/{id}` - Smazání workshopu

### Registrace
- `GET /registrations` - Seznam registrací pro konkrétní workshop
- `DELETE /registrations/{id}` - Zrušení registrace

### Datové struktury

#### Workshop
```json
{
  "id": "work1001",
  "title": "Název workshopu",
  "slug": "nazev-workshopu",
  "startDate": "2025-03-15T10:00:00",
  "capacity": 25,
  "price": 2500.00
}
```

#### Registrace
```json
{
  "id": "guid",
  "workshopId": "work1001",
  "name": "Jméno účastníka",
  "created": "2025-02-01T14:30:00",
  "price": 2500,
  "paidDate": "2025-02-01T15:00:00"
}
```

## Funkce pro zpracování chyb

Aplikace demonstruje pokročilé techniky zpracování chyb:

### Vlastní výjimky
- `ApiNotFoundException<T>`: Pro nenalezené zdroje (404)
- `ApiValidationException`: Pro chyby validace (400)
- `ApiEmptyBodyException`: Pro prázdné požadavky (400)

### Exception handlery
- `NotFoundExceptionHandler`: Zpracování 404 chyb
- `ValidationExceptionHandler`: Zpracování validačních chyb
- `EmptyBodyExceptionHandler`: Zpracování prázdných těl požadavků
- `ExceptionHandler`: Obecný handler pro neočekávané chyby

### Middleware a filtry
- `TraceMiddleware`: Přidání trace/correlation ID do odpovědí
- `MediaTypeFilter`: Kontrola Accept headerů
- `CustomizedProblemDetails`: Standardizace Problem Details odpovědí

### Route constraints
- `ApidRouteConstraint`: Validace ID ve formátu APID (8 alfanumerických znaků)

### Validace
Aplikace používá FluentValidation pro validaci vstupních dat:
- Kontrola délky textových polí
- Validace rozsahů číselných hodnot
- Kontrola platnosti dat (např. budoucí datum začátku)

## Spuštění aplikace

### Požadavky
- .NET 8.0 SDK nebo novější
- Visual Studio, Visual Studio Code, nebo jiné IDE s podporou .NET

### Instalace a spuštění
1. Klonování repository:
```bash
git clone https://github.com/mholec/event-2025-teched-api-error-handling.git
cd event-2025-teched-api-error-handling
```

2. Sestavení aplikace:
```bash
dotnet build
```

3. Spuštění aplikace:
```bash
dotnet run --project Workshops.WebApi
```

4. Aplikace bude dostupná na `https://localhost:5001` nebo `http://localhost:5000`

### Inicializace dat
Při prvním spuštění se automaticky vytvoří SQLite databáze s testovacími daty:
- 1001 ukázkových workshopů (work1000 - work2000)
- Náhodné registrace účastníků na workshopy

## Demonstrace chybových stavů

Aplikace obsahuje speciální scénáře pro demonstraci různých chybových stavů:

### 404 - Not Found
- Požadavek na neexistující workshop: `GET /workshops/neexistuje`
- Dva typy 404 odpovědí:
  - Standardní 404 pro skutečně neexistující zdroje
  - 410 Gone pro "smazané" workshopy (ID začínající "work")

### 400 - Validation Error
- Vytvoření workshopu s neplatnými daty (např. záporná cena)
- Aktualizace s prázdným tělem požadavku

### 500 - Internal Server Error
- Speciální workshop s ID "work1055" vyvolá testovací výjimku

### 406 - Not Acceptable
- Požadavky s nepodporovaným Accept headerem

## Účel a využití

Tato aplikace slouží jako:
- **Vzdělávací materiál** pro konferenci TechEd 2025
- **Referenční implementace** pokročilého zpracování chyb v .NET
- **Ukázka best practices** pro Minimal APIs
- **Demonstrace** standardizovaných chybových odpovědí podle RFC 7807

## Dokumentace API

Podrobná dokumentace API je dostupná na: https://workshopy.apidog.io/

## Licence

Tento projekt je poskytován pro vzdělávací účely v rámci konference TechEd 2025.
