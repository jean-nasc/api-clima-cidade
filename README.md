# API Clima Cidade :sunny:

*API integrada ao MySQL desenvolvida para consumir e retornar as temperaturas **atual, mínima e máxima** de uma API meteorológica com base na cidade passada como referência.*

#### COMO EXECUTAR A API:

**1.** Crie um banco de dados com o nome "clima" ou caso queira dar outro nome, não esqueça de alterar também a **Connection String** no arquivo **appsettings.json**.

**2.** Acesse: <https://home.openweathermap.org/users/sign_up> e faça seu cadastro para obter acesso a chave que será utilizada para o consumo da API **OpenWeather**. Copie e cole essa chave na key **APPID** no arquivo **appsettings.json**.

**3.** Rode as Migrations com o comando CLI "dotnet ef database update".

**4.**  Por fim, execute o comando "dotnet run".