# CustomHint
## Опис
Плагін, який дозволяє створити власний HUD для сервера.  
Для нормальної роботи плагіна необхідно використовувати **AdvancedHints** та **Newtonsoft.Json**, які є у релізах для всіх версій.  
Для пропозицій, будь ласка, пінгуйте у Discord на сервері EXILED або пишіть у приватні повідомлення: @narin4ik  
## Навчання
### Як встановити плагін?
Переходимо на [останній реліз.](https://github.com/BTF-SCPSL/CustomHint/releases) Після цього завантажуємо всі *dll* файли з релізу, а потім завантажуємо на сервер: *CustomHint.dll* та *AdvancedHints.dll* у папку Plugins (`.../EXILED/Plugins`), а *Newtonsoft.Json.dll* у папку dependencies (`.../EXILED/Plugins/dependencies`).  
Після встановлення *запускаємо/перезапускаємо* сервер.  
Як тільки ви виконали всі маніпуляції, у `.../EXILED/Configs` у файлах `[port]-config.yml` і `[port]-translation.yml` згенерується конфігурація.
### Налаштування плагіна
Почнемо з найпростішого, а саме з `[port]-config.yml`. Використовуючи комбінацію *CTRL+F* та вводячи `custom_hint`, знаходимо конфігурацію плагіна.  
Дефолтна конфігурація `[port]-config.yml` виглядає так, де описані всі пункти:
```yaml
custom_hint:
# Plugin enabled (bool)?
  is_enabled: true
  # Debug mode?
  debug: false
  # Enable or disable HUD-related commands.
  enable_hud_commands: true
  # Enable or disable automatic plugin updates.
  auto_updater: true
  # Enable or disable game hints.
  game_hint: true
  # Enable or disable hints for spectators.
  hint_for_spectators_is_enabled: true
  # The interval for changing spectator hints (in seconds).
  hint_message_time: 5
  # Default role name for players without a role.
  default_role_name: 'Player'
  # Default role color (for players without roles).
  default_role_color: 'white'
  # Ignored roles.
  excluded_roles:
  - Overwatch
  - Filmmaker
  - Scp079
```
Після того як налаштували `[port]-config.yml`, переходимо до `[port]-translation.yml`, знову використовуємо комбінацію *CTRL+F* та вводимо `custom_hint`.  
Там побачимо таке:
```yaml
custom_hint:
# Hint message for spectators.
  hint_message_for_spectators: |-
    <size=75%>{servername}
    {ip}:{port}

    {player_nickname}, spec, duration: {round_duration_hours}:{round_duration_minutes}:{round_duration_seconds}.
    Role: {player_role}
    TPS: {tps}/60

    Information:
    Class-D personnal: {classd_num} || Scientists: {scientist_num} || Facility Guards: {facilityguard_num} || MTF: {mtf_num} || CI: {ci_num} || SCPs: {scp_num} || Spectators: {spectators_num}
    Generators activated: {generators_activated}/{generators_max}

    {hints}</size>
  # Hint message for rounds lasting up to 59 seconds.
  hint_message_under_minute: |-
    <size=75%>{servername}
    {ip}:{port}

    Quick start! {player_nickname}, round time: {round_duration_seconds}s.
    Role: {player_role}
    TPS: {tps}/60</size>
  # Hint message for rounds lasting from 1 minute to 59 minutes and 59 seconds.
  hint_message_under_hour: |-
    <size=75%>{servername}
    {ip}:{port}

    Still going, {player_nickname}! Time: {round_duration_minutes}:{round_duration_seconds}.
    Role: {player_role}
    TPS: {tps}/60</size>
  # Hint message for rounds lasting 1 hour or more.
  hint_message_over_hour: |-
    <size=75%>{servername}
    {ip}:{port}

    Long run, {player_nickname}! Duration: {round_duration_hours}:{round_duration_minutes}:{round_duration_seconds}.
    Role: {player_role}
    TPS: {tps}/60</size>
  # Message displayed when the HUD is successfully hidden.
  hide_hud_success_message: '<color=green>You have successfully hidden the server HUD! To get the HUD back, use .showhud</color>'
  # Message displayed when the HUD is already hidden.
  hide_hud_already_hidden_message: '<color=red>You''ve already hidden the server HUD.</color>'
  # Message displayed when the HUD is successfully shown.
  show_hud_success_message: '<color=green>You have successfully returned the server HUD! To hide again, use .hidehud</color>'
  # Message displayed when the HUD is already shown.
  show_hud_already_shown_message: '<color=red>You already have the server HUD displayed.</color>'
  # Message displayed when DNT (Do Not Track) mode is enabled.
  dnt_enabled_message: '<color=red>Disable DNT (Do Not Track) mode.</color>'
  # Message displayed when commands are disabled on the server.
  command_disabled_message: '<color=red>This command is disabled on the server.</color>'
```
І після локалізації... Voilà! Все готово! Можете перезапустити *(повністю)* сервер, і CustomHint буде працювати на відмінно.  
Дякуємо тим, хто використовує цей плагін. Удачі вам!  
## Placeholders
| Placeholder       | Опис                                 |
| ----------------- | ------------------------------------ |
| {servername}      | Назва сервера.                      |
| {ip}              | IP-адреса сервера.                  |
| {port}            | Порт сервера.                       |
| {tps}             | TPS сервера.                        |
| {player_nickname} | Нікнейм гравця.                     |
| {player_role}     | Роль гравця.                        |
| {player_gamerole}       | Player's Game role                         |
| {round_duration_seconds} | Тривалість раунду у секундах. |
| {round_duration_minutes} | Тривалість раунду у хвилинах. |
| {round_duration_hours}   | Тривалість раунду у годинах.  |
| {classd_num}      | Кількість персоналу Класу-D.        |
| {scientist_num}   | Кількість науковців.                |
| {facilityguard_num} | Кількість охоронців комплексу.     |
| {mtf_num}         | Кількість МОГ.                      |
| {ci_num}          | Кількість ПХ.                       |
| {scp_num}         | Кількість об’єктів SCP.             |
| {spectators_num}  | Кількість глядачів (включаючи Overwatch). |
| {generators_activated} | Кількість активованих генераторів. |
| {generators_max}  | Максимальна кількість генераторів.  |
| {hints}           | Hints із файлу Hints.txt.           |
