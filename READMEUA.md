# CustomHint
## Опис
Плагін, який дозволяє створити власний HUD для сервера.  
Для нормальної роботи плагіна необхідно використовувати **HintServiceMeow**, **SSMenuSystem** та **Newtonsoft.Json**, які є у релізах для всіх версій.  
Для пропозицій, будь ласка, пінгуйте у Discord на сервері EXILED або пишіть у приватні повідомлення: @narin4ik  
## Навчання
### Як встановити плагін?
Переходимо на [останній реліз.](https://github.com/BTF-SCPSL/CustomHint/releases) Після цього завантажуємо всі файли *dll* та *zip* з релізу, потім завантажуємо на сервер *CustomHint.dll* і *dependencies.zip* у папку Plugins (`.../EXILED/Plugins`), після чого розпаковуємо *dependencies.zip*.   
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
  # Provide data for plugin statistics (data will not be shared with third parties).
  send_anon_info: true
  # Enable or disable the HUD settings in the game menu.
  hud_settings: true
  # Enable or disable automatic plugin updates.
  auto_updater: true
  # The interval for changing {hints} placeholder (in seconds).
  hint_message_time: 5
  # Default role name for players without a role.
  default_role_name: 'Player'
  # Default role color (for players without roles).
  default_role_color: 'white'
  # Server timezone for placeholder. Use 'UTC' by default or a valid timezone ID (e.g., 'Europe/Kyiv').
  server_time_zone: 'UTC'
  # Enable counting Overwatch players in placeholder {spectators_num}.
  enable_overwatch_counting: true
  # Sync speed for hints. Available values: UnSync, Slowest, Slow, Normal, Fast, Fastest.
  sync_speed: 'Fastest'
  # List of hints.
  hints:
  - id: 'firsthint'
    text: 'Hello World!'
    font_size: 15
    position_x: 500
    position_y: 500
    can_be_hidden: true
    roles:
    - ClassD
```
Після того як налаштували `[port]-config.yml`, переходимо до `[port]-translation.yml`, знову використовуємо комбінацію *CTRL+F* та вводимо `custom_hint`.  
Там побачимо таке:
```yaml
custom_hint:
# Settings header text.
  header_text: 'CustomHint'
  # Name of the item in the settings.
  button_name: 'Server HUD display'
  # Buttom hint.
  button_hint: 'Enable or disable server HUD display.'
  # Enable button.
  button_enable: 'Enable'
  # Disable button.
  button_disable: 'Disable'
  # Round time.
  round_time_formats:
    seconds: '{round_duration_seconds} seconds'
    minutes: '{round_duration_minutes} minutes {round_duration_seconds} seconds'
    hours: '{round_duration_hours} hours {round_duration_minutes} minutes {round_duration_seconds} seconds'
  # Game Role of a player, {player_gamerole} is placeholder.
  game_roles:
  - role: Tutorial
    name: 'Tutorial'
  - role: ClassD
    name: 'Class-D'
  - role: Scientist
    name: 'Scientist'
  - role: FacilityGuard
    name: 'Facility Guard'
  - role: Filmmaker
    name: 'Film Maker'
  - role: Overwatch
    name: 'Overwatch'
  - role: NtfPrivate
    name: 'MTF Private'
  - role: NtfSergeant
    name: 'MTF Sergeant'
  - role: NtfSpecialist
    name: 'MTF Specialist'
  - role: NtfCaptain
    name: 'MTF Captain'
  - role: ChaosConscript
    name: 'CI Conscript'
  - role: ChaosRifleman
    name: 'CI Rifleman'
  - role: ChaosRepressor
    name: 'CI Repressor'
  - role: ChaosMarauder
    name: 'CI Marauder'
  - role: Scp049
    name: 'SCP-049'
  - role: Scp0492
    name: 'SCP-049-2'
  - role: Scp079
    name: 'SCP-079'
  - role: Scp096
    name: 'SCP-096'
  - role: Scp106
    name: 'SCP-106'
  - role: Scp173
    name: 'SCP-173'
  - role: Scp939
    name: 'SCP-939'
  - role: Scp3114
    name: 'SCP-3114'
```
Щодо **SSMenuSystem**, щоб видалити приклади налаштувань, знайдіть `ss_menu_system` у конфігурації та встановіть значення `enable_examples` на `false`.   
І після всього цього... Voilà! Все готово! Можете перезапустити *(повністю)* сервер, і CustomHint буде працювати на відмінно.  
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
| {player_gamerole}       | Ігрова роль гравця.                         |
| {round_time} | Загальная тривалість раунда. |
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
| {current_time}  | Поточний реальний час за вашим часовим поясом.  |
| {hints}           | Hints із файлу Hints.txt.           |

## Сторонні placeholders  
Якщо ви хочете додати свій плагін для додавання placeholders, зверніться в особисті повідомлення: *@narin4ik*
| Автор            | Назва               | Опис                                     |  
| ----------------- | ------------------- | ---------------------------------------- |  
| [Narin](https://github.com/Narin4ik)            | [SteamIDPlaceholder](https://github.com/Narin4ik/SteamIDPlaceholder) | Відображає STEAMID64 у вигляді плейсхолдера.                                 |  
| [Chuppa2](https://github.com/Chuppa2)            | [CRCHPlaceholder](https://github.com/Chuppa2/CRCHPlaceholder)                               | Відображає користувацьку інформацію спостережуваного гравця.                               |
| [Narin](https://github.com/Narin4ik)            | [RespawnTimer](https://github.com/Narin4ik/RespawnTimer)                               | Додає таймера до прибуття МОГ та ПХ.                               |
