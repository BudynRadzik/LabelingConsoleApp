<div align="center">

# LabelingConsoleApp

### Aplikacja do ważenia produktów i drukowania etykiet

**Projekt konsolowy w C# / .NET 8**

<img src="https://capsule-render.vercel.app/api?type=rect&color=0:101827,50:203B55,100:2D6A73&height=170&section=header&text=LABELING%20CONSOLE%20APP&fontSize=34&fontColor=FFFFFF&fontAlignY=42&desc=Projekt%20programistyczny%20%7C%20C%23%20%7C%20.NET%208&descAlignY=68&descSize=14" width="100%" />

[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Typ aplikacji](https://img.shields.io/badge/Typ-Console%20Application-345995?style=for-the-badge&logo=windows-terminal&logoColor=white)](https://github.com/BudynRadzik/LabelingConsoleApp)
[![GitHub](https://img.shields.io/badge/Kod%20źródłowy-GitHub-181717?style=for-the-badge&logo=github)](https://github.com/BudynRadzik/LabelingConsoleApp)

</div>

---

## Nawigacja po projekcie

| Sekcja | Opis |
|---|---|
| [O projekcie](#o-projekcie) | Cel i zastosowanie aplikacji |
| [Technologie](#wykorzystane-technologie) | Stack technologiczny |
| [Funkcjonalności](#funkcjonalności) | Najważniejsze możliwości |
| [Wymagania](#wymagania) | Oprogramowanie i sprzęt |
| [Instalacja](#instalacja-i-uruchomienie) | Uruchomienie projektu |
| [Przepływ działania](#przepływ-działania-aplikacji) | Schemat procesu |
| [Tryby ważenia](#tryby-ważenia) | AUTO i MANUAL |
| [Drukowanie etykiet](#drukowanie-etykiet) | Konfiguracja drukarki |
| [Menu aplikacji](#menu-aplikacji) | Dostępne opcje |
| [Konfiguracja](#konfiguracja-i-testowanie) | Testowanie bez urządzeń |
| [Plan rozwoju](#plan-dalszego-rozwoju) | Możliwe rozszerzenia |
| [Licencja](#licencja) | Informacje o licencji |

---

<a id="o-projekcie"></a>
## 1. O projekcie

**LabelingConsoleApp** to aplikacja konsolowa napisana w języku C# z wykorzystaniem platformy .NET 8.

Jej głównym zadaniem jest obsługa procesu ważenia produktów oraz generowania i drukowania etykiet na podstawie uzyskanej masy.

Aplikacja została przygotowana z myślą o współpracy z urządzeniami wykorzystywanymi w procesie ważenia i etykietowania.

Projekt obsługuje dwa tryby pracy:

- **AUTO** — automatyczny, ciągły odczyt masy z podłączonej wagi.
- **MANUAL** — ręczne wprowadzanie wartości masy przez operatora.

Możliwe jest również uruchomienie aplikacji bez fizycznej wagi i drukarki, w trybie testowym bez drukowania etykiet.

### Testowane urządzenia

| Urządzenie | Model | Komunikacja |
|---|---|---|
| Drukarka etykiet | Zebra LP 2844 | USB, ZPL/EPL |
| Waga | OHAUS DEFENDER 2200 | RS232 / RS232-USB |

---

<a id="wykorzystane-technologie"></a>
## 2. Wykorzystane technologie

<div align="center">

[![Technologie](https://skillicons.dev/icons?i=cs,dotnet,visualstudio,git,github)](https://skillicons.dev)

</div>

| Technologia | Zastosowanie |
|---|---|
| C# | Główny język programowania |
| .NET 8 | Platforma uruchomieniowa |
| Console Application | Interfejs użytkownika |
| RS232 / USB | Komunikacja z urządzeniami |
| ZPL / EPL | Obsługa języków drukowania etykiet |

---

<a id="funkcjonalności"></a>
## 3. Funkcjonalności

Najważniejsze funkcje aplikacji:

- Automatyczny odczyt masy z wagi.
- Ręczne wprowadzanie wartości masy.
- Przetwarzanie danych dotyczących ważenia.
- Generowanie etykiet produktowych.
- Drukowanie etykiet na kompatybilnej drukarce.
- Wybór formatu etykiety.
- Zmiana liczby drukowanych etykiet.
- Możliwość testowania bez podłączonych urządzeń.
- Obsługa dwóch trybów pracy: AUTO i MANUAL.

---

<a id="wymagania"></a>
## 4. Wymagania

### Oprogramowanie

- .NET 8 SDK.
- Środowisko Windows zalecane dla testowanej konfiguracji sprzętowej.
- Terminal lub wiersz poleceń.

### Sprzęt — opcjonalnie

| Urządzenie | Model | Złącze |
|---|---|---|
| Drukarka etykiet | Zebra LP 2844 | USB |
| Waga | OHAUS DEFENDER 2200 | RS232 lub konwerter RS232/USB |

**Nie posiadasz urządzeń?** Możesz uruchomić aplikację w trybie testowym. Wystarczy wybrać tryb `MANUAL` i ustawić format etykiety na `0`.

---

<a id="instalacja-i-uruchomienie"></a>
## 5. Instalacja i uruchomienie

### Krok 1 — Pobranie repozytorium

```bash
git clone https://github.com/BudynRadzik/LabelingConsoleApp.git
```

### Krok 2 — Przejście do katalogu projektu

```bash
cd LabelingConsoleApp
```

### Krok 3 — Przywrócenie zależności

```bash
dotnet restore
```

### Krok 4 — Kompilacja

```bash
dotnet build
```

### Krok 5 — Uruchomienie

```bash
dotnet run
```

---

<a id="przepływ-działania-aplikacji"></a>
## 6. Przepływ działania aplikacji

Poniższy diagram przedstawia główny przebieg pracy programu — od wyboru formatu etykiety, przez pobranie masy, aż do ewentualnego wydrukowania etykiety.

```mermaid
flowchart TD
    A([START]) --> B[Wybór formatu etykiety]
    B --> C{Format = 0?}
    C -->|Tak| D[Drukowanie wyłączone]
    C -->|Nie| E[Drukowanie włączone]
    D --> F{Wybór trybu ważenia}
    E --> F
    F -->|AUTO| G[Ciągły odczyt masy]
    F -->|MANUAL| H[Ręczne wpisanie masy]
    G --> I[Przetwarzanie danych]
    H --> I
    I --> J{Drukowanie aktywne?}
    J -->|Tak| K[Generowanie etykiety]
    K --> L[Drukowanie etykiety]
    J -->|Nie| M[Pominięcie drukowania]
    L --> N{Tryb AUTO?}
    M --> N
    N -->|Tak| G
    N -->|Nie| O([KONIEC])
```

### Przebieg procesu

1. Operator wybiera format etykiety.
2. Następnie wybiera tryb ważenia.
3. Program pobiera masę automatycznie lub przyjmuje wartość wprowadzoną ręcznie.
4. Dane dotyczące masy są przetwarzane.
5. Jeżeli drukowanie jest aktywne, program generuje i drukuje etykietę.
6. W trybie AUTO proces odczytu masy jest kontynuowany w pętli.

---

<a id="tryby-ważenia"></a>
## 7. Tryby ważenia

### AUTO — automatyczne ważenie

Tryb przeznaczony do współpracy z podłączoną wagą.

- Ciągły odczyt wartości masy.
- Automatyczne pobieranie danych z urządzenia.
- Przetwarzanie kolejnych odczytów.
- Możliwość ciągłej pracy aplikacji.

### MANUAL — ręczne ważenie

Tryb umożliwiający pracę bez fizycznego podłączenia wagi.

- Operator samodzielnie wprowadza wartość masy.
- Nie jest wymagane połączenie z urządzeniem pomiarowym.
- Tryb przydatny podczas testowania aplikacji.

---

<a id="drukowanie-etykiet"></a>
## 8. Drukowanie etykiet

Aplikacja umożliwia generowanie i drukowanie etykiet produktowych.

### Testowana drukarka

**Zebra LP 2844**

- Połączenie USB.
- Obsługa języków drukowania ZPL/EPL.

### Format etykiety

Format etykiety określa, czy program ma realizować proces drukowania.

| Format | Działanie |
|---|---|
| `0` | Drukowanie wyłączone |
| Inny skonfigurowany format | Generowanie i drukowanie etykiet |

Dostępne formaty zależą od konfiguracji aplikacji.

---

<a id="menu-aplikacji"></a>
## 9. Menu aplikacji

Aplikacja udostępnia menu umożliwiające zmianę wybranych parametrów pracy.

Dostępne opcje obejmują:

- Zmianę formatu etykiety.
- Zmianę liczby etykiet.
- Wybór trybu ważenia.
- Przejście do procesu ważenia.

---

<a id="konfiguracja-i-testowanie"></a>
## 10. Konfiguracja i testowanie

### Uruchomienie bez wagi i drukarki

Aplikację można przetestować bez podłączania urządzeń zewnętrznych.

W tym celu:

1. Uruchom program.
2. Wybierz tryb ważenia `MANUAL`.
3. Ustaw format etykiety na `0`.
4. Wprowadź ręcznie wartość masy.

Ustawienie formatu `0` wyłącza drukowanie etykiet. Dzięki temu można sprawdzić działanie aplikacji bez fizycznej drukarki.

### Konfiguracja sprzętowa

W przypadku pracy z urządzeniami:

- Podłącz drukarkę Zebra LP 2844 przez USB.
- Podłącz wagę OHAUS DEFENDER 2200 przez RS232 lub konwerter RS232/USB.
- Upewnij się, że wymagane sterowniki i ustawienia systemowe są poprawnie skonfigurowane.

---

<a id="plan-dalszego-rozwoju"></a>
## 11. Plan dalszego rozwoju

Możliwe kierunki rozbudowy projektu:

- [ ] Rozszerzenie konfiguracji drukarek.
- [ ] Obsługa kolejnych modeli drukarek.
- [ ] Integracja z dodatkowymi modelami wag.
- [ ] Rozbudowa obsługi błędów i diagnostyki.
- [ ] Więcej opcji konfiguracyjnych.
- [ ] Elastyczniejsze szablony etykiet.

---

<a id="zrzuty-ekranu"></a>
## 12. Zrzuty ekranu

W tej sekcji można umieścić zrzuty ekranu prezentujące działanie aplikacji.

Proponowana struktura katalogu:

```text
docs/
└── screenshots/
    ├── menu-glowne.png
    ├── tryb-wazenia.png
    └── drukowanie-etykiet.png
```

Przykład dodania obrazu:

```markdown
![Menu główne aplikacji](docs/screenshots/menu-glowne.png)
```

---

<a id="licencja"></a>
## 13. Licencja

Informacje o licencji nie zostały jeszcze określone.

---

<div align="center">

## Autor projektu

**LabelingConsoleApp**

Projekt programistyczny napisany w C# z wykorzystaniem .NET 8.

[![GitHub Repository](https://img.shields.io/badge/Zobacz%20repozytorium-GitHub-181717?style=for-the-badge&logo=github)](https://github.com/BudynRadzik/LabelingConsoleApp)

<img src="https://capsule-render.vercel.app/api?type=waving&color=0:2D6A73,50:203B55,100:101827&height=110&section=footer" width="100%" />

</div>
