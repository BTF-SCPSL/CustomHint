# CustomHint
## Descrição
Um plug-in que permite que você crie sua própria HUD personalizada para o servidor.
Para garantir funcionalidade adequada, o plug-in exige o **HintServiceMeow** e o **Newtonsoft.json**, que estão incluídos nos lançamentos para todas as versões.  
Para sugestões, me mencione no servidor do Discord EXILED ou na DM: @narin4ik.

## Guia

### Como instalar o plug-in?
Vá para o [último lançamento](https://github.com/BTF-SCPSL/CustomHint/releases). Depois disso, baixe todos os arquivos *dll* e *zip* do lançamento, faça upload dos arquivos *CustomHint.dll* e *dependencies.zip* ao servidor na pasta Plugins (`.../EXILED/Plugins`), e então extraia o *dependencies.zip*.  
Após a instalação, *inicie/reinicie* o servidor. 
Assim que você completar todos os passos, a configuração será gerada em `.../EXILED/Configs` dentro do `[porta]-config.yml` e do `[porta]-translation.yml`.

### Configurando o plug-in
Vamos começar com a parte mais fácil, `[porta]-config.yml`. Use o atalho *CTRL+F* para pesquisar por `custom_hint` e localizar a configuração do plug-in. 
Por padrão, o `[porta]-config.yml` do nosso plug-in se parece assim, com todos os pontos explicados:
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
Depois de configurar o `[porta]-config.yml`, vá para o  `[porta]-translation.yml`. Use *CTRL+F* novamente para pesquisar por `custom_hint`.  
Você encontrará o seguinte:
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
E depois de tudo isso... **Voilà**! Tudo está pronto! Você pode reiniciar o servidor *(completamente)*, e o CustomHint vai funcionar perfeitamente. 
Meus agradecimentos a todos que usarem esse plug-in. Boa sorte!  

## Placeholders
| Placeholder             | Descrição                                                        |
| ----------------------- | ---------------------------------------------------------------  |
| {servername}            | Nome do servidor.                                                |
| {ip}                    | Endereço de IP do servidor.                                      |
| {port}                  | Porta do servidor.                                               |
| {tps}                   | TPS do servidor.                                                 |
| {player_nickname}       | Apelido do jogador.                                              | 
| {player_role}           | Cargo do jogador.                                                |
| {player_gamerole}       | Classe do jogador.                                               |
| {round_time}            | Duração geral da partida.                                        |
| {round_duration_seconds}| Duração da partida em segundos.                                  |
| {round_duration_minutes}| Duração da partida em minutos.                                   |
| {round_duration_hours}  | Duração da partida em horas.                                     |
| {classd_num}            | Número de funcionários Classe-D.                                 |
| {scientist_num}         | Número de Cientistas.                                            |
| {facilityguard_num}     | Número de Guardas da Instalação.                                 |
| {mtf_num}               | Número de membros da FTM.                                        |
| {ci_num}                | Número de Insurgentes do Caos.                                   |
| {scp_num}               | Número de objetos SCP.                                           |
| {spectators_num}        | Número de espectadores (incluindo aqueles em modo Supervisor).|
| {generators_activated}  | Número de geradores ativados.                                    |
| {generators_max}        | Número máximo de geradores.                                      |
| {current_time}          | Horário atual em tempo real do seu fuso-horário.                 |
| {hints}                 | Hints do arquivo Hints.yml.                                      |

## Placeholders de terceiros
Se você quiser adicionar seu próprio plug-in para implementação de placeholders, entre em contato comigo via DM: *@narin4ik*
| Autor(a)                                          | Nome                                                                 | Descrição                                                         |
| ------------------------------------------------- | -------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Narin](https://github.com/Narin4ik)              | [SteamIDPlaceholder](https://github.com/Narin4ik/SteamIDPlaceholder) | Exibe o STEAMID64.                                                |
| [Chuppa2](https://github.com/Chuppa2)             | [CRCHPlaceholder](https://github.com/Chuppa2/CRCHPlaceholder)        | Exibe informações personalizadas sobre jogadores espectados.      |
| [Narin](https://github.com/Narin4ik)              | [RespawnTimer](https://github.com/Narin4ik/RespawnTimer)             | Adiciona temporizadores que destacam quando a FTM ou IC nascerão. |
