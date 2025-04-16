🌤️ Nome do Projeto: ClimaNotificacoesAPI
🎯 Objetivo:
Criar uma API que permite o cadastro de cidades por usuários e, diariamente, verifique a previsão do tempo dessas cidades. Se houver previsão de chuva ou tempestade, o sistema envia alertas por e-mail para os usuários cadastrados.
🧩 Funcionalidades principais:
✅ 1. Cadastro de Cidade e E-mail
• O usuário informa:
    ◦ Nome da cidade (ex: "São Paulo")
    ◦ Seu e-mail
• O sistema armazena essas informações para futuras verificações.
✅ 2. Listagem de Cidades
• Endpoint para retornar todas as cidades cadastradas com seus respectivos e-mails.
✅ 3. Remoção de Cadastro
• O usuário pode remover uma cidade cadastrada (ex: parou de se interessar por aquela cidade).
✅ 4. Verificação automática do clima
• A cada X horas (ou diariamente), o sistema:
    ◦ Consulta a API do OpenWeather para cada cidade cadastrada.
    ◦ Verifica se há previsão de chuva ou tempestade.
✅ 5. Envio de e-mail automático
• Se for detectada chuva ou tempestade, o sistema envia um e-mail de alerta para o usuário que cadastrou aquela cidade, com:
    ◦ Condição climática (ex: “chuva forte”)
    ◦ Temperatura atual
✅ 6. Agendamento com Hangfire
• O sistema usa o Hangfire para agendar a execução do job de verificação do clima de forma periódica.