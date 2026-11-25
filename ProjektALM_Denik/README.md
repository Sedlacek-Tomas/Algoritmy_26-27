# Deník – Projekt ALM

## Popis projektu
Konzolová aplikace v jazyce C#, která umožňuje vytvářet osobní deník, přidávat záznamy k jednotlivým dnům, procházet mezi dny a mazat jednotlivé záznamy.

## Použité technologie
- Programovací jazyk: C#
- Platforma: .NET 10.0
- Vývojové prostředí: JetBrains Rider
- Ukládání dat: JSON

## Použité datové struktury
- Dny jsou uloženy ve vlastním spojovém seznamu.
- Záznamy jednotlivých dnů jsou uloženy ve vlastním seznamu postaveném nad polem s možností dynamického zvětšování.

## Spuštění
1. Naklonujte Git repozitář.
2. Otevřete soubor projektu `.csproj` v prostředí JetBrains Rider.
3. Spusťte aplikaci pomocí tlačítka Run.

## Dostupné příkazy
- `novy` – přidání nového záznamu k zadanému datu
- `smaz` – smazání vybraného záznamu
- `predchozi` – přechod na předchozí den
- `dalsi` – přechod na následující den
- `zacatek` – přechod na první den v deníku
- `konec` – přechod na poslední den v deníku
- `uloz` – uložení deníku do souboru
- `nacti` – načtení deníku ze souboru
- `zavri` – ukončení programu
