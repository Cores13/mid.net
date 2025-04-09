# Projektna dokumentacija

Ovaj projekat koristi modernu arhitekturu i obrasce dizajniranja kako bi stvorio efikasan, održiv i skalabilan softverski sistem. Uključuje primjenu sljedećih tehnika:

- **MediatR**
- **Clean Architecture**
- **Domain Driven Design (DDD)**
- **CQRS Pattern**
- **Global Error Handling**
- **Logging and Validation Pipelines**
- **Unit of Work Pipeline**

## Tehničke specifikacije

### 1. **MediatR**
MediatR je biblioteka za implementaciju obrasca **Mediator**, koja omogućava efikasnu komunikaciju između različitih dijelova sistema bez direktnih zavisnosti. Koristi se za delegiranje svih poslovnih logika i upita izvan kontrolera i servisa.

### 2. **Clean Architecture**
Arhitektura je organizovana prema **Clean Architecture** principima, što znači da je aplikacija podijeljena na više slojeva:
- **Domain**: Domenski model, poslovna logika i interfejsi.
- **Application**: Implementacija poslovne logike, komandnih handler-a (CQRS).
- **Infrastructure**: Implementacije za pristup podacima, API pozive, itd.
- **WebApi**: Prezentacija, API kontroleri.

### 3. **Domain Driven Design (DDD)**
Arhitektura je zasnovana na **Domain Driven Design** principima, gde su svi objekti (entiteti, agregati, vrijednosni objekti) modelovani tako da predstavljaju domenski entitet. To omogućava jasnu podijelu između slojeva i lakše razumijevanje poslovnog domena.

### 4. **CQRS (Command Query Responsibility Segregation)**
CQRS obrazac se koristi za odvajanje operacija za čitanje (upiti) i pisanje (komande). Ovo omogućava optimizaciju performansi za svaku vrstu operacija, kao i lakše upravljanje skaliranjem sistema.

### 5. **Global Error Handling**
Cijeli sistem koristi centralizovano upravljanje greškama, gde se sve greške obrađuju i vraćaju u uniformisanom formatu putem middleware-a, što poboljšava korisničko iskustvo i lakše je za debagovanje.

### 6. **Logging and Validation Pipelines**
Za praćenje i analizu rada sistema koristi se centralizovan **logging** sistem. Takođe, cijeli proces obrade zahtjeva i odgovora je obuhvaćen pipeline-ima za **validaciju** i **logovanje**.

### 7. **Unit of Work**
Implementiran je **Unit of Work** obrazac kako bi se obezbjedila koherentnost transakcija kroz različite slojeve aplikacije i omogućilo jednostavno praćenje svih promjena u jednoj transakciji.

## Uputstvo za podešavanje projekta

### Pre-rekviziti

Pre nego što pokrenete projekat, uvjerite se da imate sljedeće instalirano na svom računaru:

1. **.NET SDK** (najnovija verzija)
2. **SQL Server**
3. **Visual Studio** ili bilo koji drugi IDE koji podržava .NET razvoj

### 1. **Kloniranje repozitorijuma**

Klonirajte repozitorij sa GitHub-a koristeći sljedeću komandu:

```bash
git clone https://github.com/your-repository-url.git
```

## 2. Konfiguracija baze podataka

Ako koristite SQL Server, podesite konekcioni string u appsettings.json datoteci.

Primjer:
```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Initial Catalog=MidNet;User Id=your-username;Password=your-password;"
    }
}
```

## 3. Primjena migracija

Pokrenite sljedeću komandu da primijenite migracije na vašu bazu podataka:

```bash
dotnet ef database update
```

ili

```bash
Update-Database
```

## 4. Pokretanje projekta

Pokrenite aplikaciju pomoću sljedeće komande:

```bash
dotnet run
```

ili klikom na dugme "Run" u Visual Studiu

## 5. Testiranje

Testiranje API-a možete izvršiti korištenjem swagger-a ili pomoću postmana na sljedećem linku:
[Postman workspace](https://www.postman.com/security-geoscientist-13986592/kingict-test/collection/dhgeoub/min-net?action=share&creator=15333092)


