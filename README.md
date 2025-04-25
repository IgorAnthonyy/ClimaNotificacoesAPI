🌤️ Nome do Projeto: ClimaNotificacoesAPI
🎯 Objetivo:
Criar uma API que permite o cadastro de usuários e cidades de interesse para previsão do tempo. O sistema verifica periodicamente a previsão do tempo e, em caso de chuva ou tempestade, envia alertas por e-mail para os respectivos usuários.

🧩 Funcionalidades principais:
✅ 1. Cadastro de Usuário e Cidade
O usuário informa:

Seu e-mail

Nome da cidade (ex: "São Paulo")

O sistema registra:

Usuário (com dados como e-mail e senha, se houver autenticação)

Cidade vinculada ao usuário (UsuarioId)
✅ 2. Listagem de Cidades por Usuário
Endpoint que retorna todas as cidades cadastradas por cada usuário com seus respectivos e-mails.

Pode incluir os dados do tempo mais recente, se desejar.

✅ 3. Remoção de Cidade
O usuário pode remover uma cidade cadastrada (ex: se não quiser mais receber alertas sobre ela).

A cidade é removida da base e o vínculo com o usuário é desfeito.

✅ 4. Verificação automática do clima
A cada X horas (ou diariamente), o sistema:

Consulta a API do OpenWeather para cada cidade cadastrada.

Verifica se há previsão de chuva ou tempestade.

Atualiza a entidade PrevisaoTempo associada à cidade.

✅ 5. Envio de e-mail automático
Se for detectada chuva ou tempestade, o sistema envia um e-mail de alerta para o usuário que cadastrou aquela cidade.

O e-mail contém:

A condição climática (ex: “chuva forte”)

A temperatura atual

Nome da cidade e data/hora da previsão

✅ 6. Agendamento com Hangfire
O sistema usa o Hangfire para agendar e executar periodicamente o job de verificação do clima.

O job percorre todas as cidades cadastradas e realiza a lógica de verificação e envio de alerta.