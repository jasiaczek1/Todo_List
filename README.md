README.md
# Todo_List

* Aplikacja desktopowa WPF, wykorzystująca Entity Framework Core oraz SQLite do prostego zarządzania listą zadań.

# Funkcje

* Dodawanie nowych zadań.

* Edycja istniejących zadań.

* Usuwanie wybranych zadań.

* Filtrowanie zadań według daty.

* Zapisywanie danych w lokalnej bazie SQLite.

# Technologie

* WPF (.NET 7)

* Entity Framework Core (SQLite)

* C#

# Struktura projektu

```javascript
Todo_List/
│
├── Commands/
│   └── RelayCommand.cs         Komenda wykorzystywana w MVVM.
│
├── Data/
│   └── AppDbContext.cs         Konfiguracja EF Core i definicja tabel.
│
├── Migrations/
│   └── ...                     Migracje EF Core (jeśli utworzone).
│
├── Models/
│   └── TodoItem.cs             Model ORM mapowany do tabeli SQLite.
│
├── Services/
│   ├── ITodoService.cs         Abstrakcja logiki danych.
│   └── TodoService.cs          Implementacja operacji CRUD.
│
├── ViewModels/
│   ├── ViewModelBase.cs        Implementacja INotifyPropertyChanged.
│   ├── MainViewModel.cs        Logika głównego okna: listy zadań.
│   └── TaskItemViewModel.cs    ViewModel pojedynczego zadania.
│
├── Views/
│   ├── MainWindow.xaml         Główne UI.
│   ├── MainWindow.xaml.cs      Kod-behind ograniczony do inicjalizacji.
│   ├── TaskEditorWindow.xaml   UI edytora zadania.
│   └── TaskEditorWindow.xaml.cs VM edytora zadania.
│
├── App.xaml                    Konfiguracja aplikacji.
└── App.xaml.cs                 Inicjalizacja kontekstu i serwisów.

```

# Instalacja i uruchamianie

* Pobierz projekt lub klonuj repozytorium.

* Otwórz solution w Visual Studio.

* Uruchom aplikację (F5).
Baza todo.db zostanie automatycznie utworzona w katalogu bin.

## Upewnij się, że masz zainstalowane:

* .NET 7

* Microsoft.EntityFrameworkCore

* Microsoft.EntityFrameworkCore.Sqlite

# Nie wymaga instalatora. Aby przekazać aplikację dalej:

* zbuduj projekt (Release),

* wyślij cały folder bin/Release/net7.0-windows/,

* w katalogu będzie plik exe i baza SQLite.

# Użycie

* Wybierz datę w kalendarzu.

* Kliknij "Dodaj", aby stworzyć nowe zadanie.

* Kliknij "Edytuj", aby zmienić zaznaczone zadanie.

* Kliknij "Usuń", aby usunąć wybrane zadanie.

* Zmiany zapisują się automatycznie w SQLite.

# DOKUMENTACJA TECHNICZNA
## Architektura

### Warstwy:

* Models
* Definiują encje bazodanowe mapowane przez EF Core.
* TodoItem zawiera: Id, Title, Description, Date.

### Data
* AppDbContext zarządza połączeniem z SQLite oraz tworzy tabelę TodoItems.

### Services
* Logika biznesowa komunikująca się z bazą danych.
* TodoService implementuje CRUD.

### ViewModels
* Logika dla widoków (UI).
* MainViewModel pobiera dane z TodoService i wystawia je do bindingu.

### Views
* XAML + minimalny code-behind ograniczony do inicjalizacji.

## Przepływ danych

* UI wywołuje komendę (RelayCommand).

* Komenda uruchamia metodę w ViewModel.

* ViewModel korzysta z serwisu (ITodoService).

* Serwis wykonuje operacje na SQLite przez AppDbContext.

* Wyniki są odświeżane w observable kolekcji bindowanej z UI.
## Kontekst bazy danych
### AppDbContext tworzy tabelę:
```vbnet
TodoItems(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT,
    Description TEXT,
    Date TEXT
)
```
## Migracje
```pgsql
Add-Migration Init
Update-Database
```
