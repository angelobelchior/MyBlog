# MyBlog
Repositório usado em diversos vídeos do canal https://www.youtube.com/@angelobelchior

## Como executar o projeto

Antes de sair rodando o projeto, são necessárias algumas configurações.
Abaixo eu as descrevo. É pouca coisa, não se preocupe!

É importante destacar que esse projerto foi sendo desenvolvido e evoluído ao longo de diversos vídeos.

Eu não vou explicar cada detalhe aqui, mas você pode acessar o canal e assistir aos vídeos para entender melhor o que está acontecendo.

Abaixo eu deixo a lista de vídeos que referenciam esse repositório.

### Docker #FTW

Antes de tudo, é necessário ter o Docker instalado na máquina. Caso não tenha ele instalado siga as instruções em https://docs.docker.com/get-docker/.

Esse projeto utiliza MSSQL Server, Redis, Elasticsearch, Kibana e Jaeger, todos sendo executados em containers Docker.

### Configurando as Secrets

O projeto de backend utiliza secrets para armazenar as strings de conexão com o banco de dados e Redis. 
Se você não sabe o que são secrets, acesse https://docs.microsoft.com/pt-br/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows.
Para facilitar a sua vida, segue a estrutura prontinha para ser colada no arquivo secrets.json

```json
{
  "ConnectionStrings": {
    "SqlServerConnectionString": "Server=localhost;Database=MyBlog;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;",
    "RedisConnectionString": "localhost:6379"
  }
}

```

### Subindo a infraestrutura

Após clone do repositório, acesse a pasta `infra` e execute o comando `docker-compose up -d`. 
Isso irá baixar as imagens necessárias e subir os containers.

Se tudo der certo e nada der errado, você vai conseguir acessar os seguintes serviços:

- Para acessar o Jaeger: http://localhost:16686/search
- Para acessar o Kibana: http://localhost:5601
- Para acessar o Elasticsearch: http://localhost:9200 (vai apresentar apenas um json)

### Executando os projetos 

Com a infra de pé, é hora de executar os projetos.

Comece pelo projeto de backend. Acesse a pasta `src/MyBlog.Backend` e execute o comando `dotnet run`.

Em seguida, acesse a pasta `src/MyBlog.Frontend` e execute o comando `dotnet run`.

_É possível ainda executar os dois projetos ao mesmo tempo. Dependendo da sua IDE, é possível configurar isso._

Feito isso, você poderá acessar:

- Para acessar o frontend: http://localhost:5039
- Para acessar o backend: http://localhost:5057/scalar/v1

## Canal do YouTube (https://www.youtube.com/@angelobelchior)

### Vídeos que referenciam esse repo:

- https://youtu.be/rE-PPMhQi44
- https://youtu.be/3Um-XF5GgmE
- https://youtu.be/jLHue_28yXg
- https://youtu.be/Btq6LbeVt84
- https://youtu.be/14kQtXX4x7Y

### Referências e documentações citadas nos vídeos:

- https://github.com/docker/awesome-compose/tree/master/elasticsearch-logstash-kibana
- https://opentelemetry.io/
- https://github.com/renatogroffe/OpenTelemetry-Jaeger-DotNet8-APIs-PostgreSQL-Loki-Grafana
- https://github.com/renatogroffe/AzureAzureAPIM-Grafana-Integration
- https://www.youtube.com/watch?v=VptRKOvBGIY&ab_channel=CanaldotNET
- https://www.youtube.com/watch?v=rE-PPMhQi44&ab_channel=AngeloBelchior
- https://www.youtube.com/watch?v=3Um-XF5GgmE&ab_channel=AngeloBelchior
- https://refactoring.guru/pt-br
- https://github.com/dotnet/aspnetcore/issues/54599
- https://github.com/scalar/scalar/blob/main/packages/scalar.aspnetcore/README.md
