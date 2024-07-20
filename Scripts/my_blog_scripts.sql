-- Criação da tabela de Categorias
CREATE TABLE Categories (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
INSERT INTO Categories(Name) Values('Development');
INSERT INTO Categories(Name) Values('DevOps');
INSERT INTO Categories(Name) Values('Observability');

-- Criação da tabela de Tags
CREATE TABLE Tags (
    TagID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
INSERT INTO Tags(Name) Values('dotnet');
INSERT INTO Tags(Name) Values('csharp');
INSERT INTO Tags(Name) Values('aspire');
INSERT INTO Tags(Name) Values('github actions');
INSERT INTO Tags(Name) Values('grafana');

-- Criação da tabela de Posts
CREATE TABLE Posts (
    PostID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    CategoryID INT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

-- Criação da tabela de PostTags (tabela de junção para muitos-para-muitos)
CREATE TABLE PostTags (
    PostID INT,
    TagID INT,
    PRIMARY KEY (PostID, TagID),
    FOREIGN KEY (PostID) REFERENCES Posts(PostID),
    FOREIGN KEY (TagID) REFERENCES Tags(TagID)
);

-- Criação da tabela de Comentários
CREATE TABLE Comments (
    CommentID INT IDENTITY(1,1) PRIMARY KEY,
    PostID INT,
    Author NVARCHAR(100) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (PostID) REFERENCES Posts(PostID)
);

-- Populando o Banco de Dados

INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Aspire: O Futuro da Tecnologia', 'Este é um post detalhado sobre o Aspire e seu impacto no futuro da tecnologia...', 1, GETDATE());

DECLARE @PostID INT
SET @PostID = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID, 1);
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID, 2);
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID, 3);

-- Inserir comentários fakes na tabela Comments
INSERT INTO Comments (PostID, Author, Content, CreatedAt)
VALUES (@PostID, 'João Silva', 'Excelente post! Muito informativo.', GETDATE());

INSERT INTO Comments (PostID, Author, Content, CreatedAt)
VALUES (@PostID, 'Maria Oliveira', 'Estou ansiosa para ver como o Aspire vai mudar o mercado.', GETDATE());

INSERT INTO Comments (PostID, Author, Content, CreatedAt)
VALUES (@PostID, 'Carlos Santos', 'Muito bom! Continue postando conteúdos desse tipo.', GETDATE());


-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Automatização com GitHub Actions', 'Este post discute como utilizar GitHub Actions para automatizar fluxos de trabalho no desenvolvimento de software...', 2, GETDATE());

-- Obter o PostID do post recém-inserido
SET @PostID = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID, 4);

-- Inserir comentários fakes na tabela Comments
INSERT INTO Comments (PostID, Author, Content, CreatedAt)
VALUES (@PostID, 'Ana Costa', 'Ótimo guia sobre GitHub Actions!', GETDATE());

INSERT INTO Comments (PostID, Author, Content, CreatedAt)
VALUES (@PostID, 'Pedro Lima', 'Muito útil para o meu projeto. Obrigado!', GETDATE());

INSERT INTO Comments (PostID, Author, Content, CreatedAt)
VALUES (@PostID, 'Fernanda Souza', 'Adorei as dicas sobre automação.', GETDATE());

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Observabilidade com Grafana', 'Neste post, exploramos como usar Grafana para monitorar e visualizar métricas de sistemas em tempo real...', 3, GETDATE());

-- Obter o PostID do post recém-inserido
SET @PostID = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID, 5);

-- Inserir comentários fakes na tabela Comments
INSERT INTO Comments (PostID, Author, Content, CreatedAt)
VALUES (@PostID, 'Lucas Almeida', 'Grafana é uma ferramenta incrível para observabilidade.', GETDATE());

INSERT INTO Comments (PostID, Author, Content, CreatedAt)
VALUES (@PostID, 'Juliana Ferreira', 'Excelente explicação sobre Grafana!', GETDATE());

INSERT INTO Comments (PostID, Author, Content, CreatedAt)
VALUES (@PostID, 'Rafael Gomes', 'Estou usando Grafana no meu trabalho e está sendo muito útil.', GETDATE());

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Entendendo o .NET Core', 'Este post explora as funcionalidades e benefícios do .NET Core para desenvolvimento moderno...', 1, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID1 INT
SET @PostID1 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID1, 1);
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID1, 2);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Automação com GitHub Actions', 'Automatize seus fluxos de trabalho de desenvolvimento utilizando GitHub Actions...', 2, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID2 INT
SET @PostID2 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID2, 4);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Monitoramento com Grafana', 'Como utilizar Grafana para monitorar aplicações e infraestrutura...', 3, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID3 INT
SET @PostID3 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID3, 5);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('C# para Iniciantes', 'Uma introdução ao C# para quem está começando na programação...', 1, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID4 INT
SET @PostID4 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID4, 2);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Melhores Práticas em DevOps', 'Dicas e práticas recomendadas para implementar uma cultura DevOps...', 2, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID5 INT
SET @PostID5 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID5, 4);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('A Importância da Observabilidade', 'Por que a observabilidade é crucial para a manutenção de sistemas modernos...', 3, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID6 INT
SET @PostID6 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID6, 5);


-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Avançando com ASP.NET', 'Técnicas avançadas para desenvolvimento web com ASP.NET...', 1, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID7 INT
SET @PostID7 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID7, 3);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Integração Contínua com GitHub Actions', 'Como configurar pipelines de integração contínua com GitHub Actions...', 2, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID8 INT
SET @PostID8 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID8, 4);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Visualização de Dados com Grafana', 'Melhores práticas para visualização de métricas e logs com Grafana...', 3, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID9 INT
SET @PostID9 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID9, 5);
-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Desenvolvimento Ágil com .NET', 'Como aplicar metodologias ágeis no desenvolvimento com .NET...', 1, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID10 INT
SET @PostID10 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID10, 1);
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID10, 2);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('C# Avançado', 'Explorando funcionalidades avançadas da linguagem C#...', 1, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID11 INT
SET @PostID11 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID11, 2);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Aprimorando a Observabilidade', 'Estratégias para melhorar a observabilidade de sistemas distribuídos...', 3, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID12 INT
SET @PostID12 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID12, 5);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Automatizando Deployments com GitHub Actions', 'Passo a passo para automatizar deployments utilizando GitHub Actions...', 2, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID13 INT
SET @PostID13 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID13, 4);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Introdução ao ASP.NET Core', 'Um guia para começar a desenvolver aplicações web com ASP.NET Core...', 1, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID14 INT
SET @PostID14 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID14, 3);


-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Monitoramento de Aplicações com Grafana', 'Como configurar o Grafana para monitorar a performance de aplicações...', 3, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID15 INT
SET @PostID15 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID15, 5);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('DevOps na Prática', 'Exemplos práticos de implementação de DevOps em projetos reais...', 2, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID16 INT
SET @PostID16 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID16, 4);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Desenvolvimento de APIs com .NET', 'Como criar e consumir APIs utilizando .NET...', 1, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID17 INT
SET @PostID17 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID17, 1);
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID17, 3);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Observabilidade com Grafana e Prometheus', 'Integrando Grafana e Prometheus para uma observabilidade completa...', 3, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID18 INT
SET @PostID18 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID18, 5);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Integração Contínua e Entrega Contínua com GitHub Actions', 'Implementando CI/CD pipelines com GitHub Actions...', 2, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID19 INT
SET @PostID19 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID19, 4);

-- Inserir um post na tabela Posts
INSERT INTO Posts (Title, Content, CategoryID, CreatedAt)
VALUES ('Novidades do C# 9', 'Explorando as novas funcionalidades da versão 9 do C#...', 1, GETDATE());

-- Obter o PostID do post recém-inserido
DECLARE @PostID20 INT
SET @PostID20 = SCOPE_IDENTITY();

-- Inserir associações de tags na tabela PostTags
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID20, 2);
INSERT INTO PostTags (PostID, TagID) VALUES (@PostID20, 3);


-- Inserir comentários aleatórios na tabela Comments

-- Comentários para o Post 1
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (1, 'João Silva', 'Excelente post sobre .NET Core!', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (1, 'Maria Oliveira', 'Muito informativo, obrigado!', GETDATE());

-- Comentários para o Post 2
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (2, 'Carlos Santos', 'GitHub Actions é realmente uma ferramenta poderosa.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (2, 'Ana Costa', 'Vou implementar no meu projeto, valeu pelas dicas!', GETDATE());

-- Comentários para o Post 3
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (3, 'Pedro Lima', 'Grafana facilita muito o monitoramento.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (3, 'Fernanda Souza', 'Adorei aprender mais sobre Grafana.', GETDATE());

-- Comentários para o Post 4
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (4, 'Rafael Gomes', 'Ótima introdução ao C#!', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (4, 'Juliana Ferreira', 'Muito útil para iniciantes.', GETDATE());

-- Comentários para o Post 5
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (5, 'Lucas Almeida', 'Boas práticas são sempre bem-vindas.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (5, 'Mariana Silva', 'DevOps é essencial!', GETDATE());

-- Comentários para o Post 6
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (6, 'Thiago Pinto', 'Observabilidade é crucial para sistemas modernos.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (6, 'Camila Rocha', 'Muito bom aprender mais sobre este tema.', GETDATE());

-- Comentários para o Post 7
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (7, 'Eduardo Lima', 'ASP.NET é uma ótima tecnologia.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (7, 'Renata Carvalho', 'Post avançado e interessante.', GETDATE());

-- Comentários para o Post 8
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (8, 'André Martins', 'Integração contínua é fundamental.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (8, 'Patrícia Fernandes', 'Muito bom aprender mais sobre GitHub Actions.', GETDATE());

-- Comentários para o Post 9
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (9, 'Gabriel Santos', 'Grafana é excelente para visualização de dados.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (9, 'Beatriz Ribeiro', 'Gostei das dicas!', GETDATE());

-- Comentários para o Post 10
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (10, 'Rodrigo Costa', 'Desenvolvimento ágil é o caminho.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (10, 'Vanessa Lima', 'Ótimo conteúdo sobre .NET.', GETDATE());

-- Comentários para o Post 11
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (11, 'Daniel Souza', 'Exploração avançada do C#.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (11, 'Aline Silva', 'Muito bom, quero aprender mais!', GETDATE());

-- Comentários para o Post 12
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (12, 'Rafaela Martins', 'Observabilidade é essencial para a manutenção de sistemas.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (12, 'Fernando Costa', 'Post muito bem escrito.', GETDATE());

-- Comentários para o Post 13
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (13, 'Gustavo Lima', 'Automatizar deployments é muito útil.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (13, 'Natália Ferreira', 'Vou aplicar isso no meu projeto.', GETDATE());

-- Comentários para o Post 14
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (14, 'Paulo Santos', 'ASP.NET Core é muito poderoso.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (14, 'Isabela Souza', 'Ótima introdução!', GETDATE());

-- Comentários para o Post 15
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (15, 'Mateus Oliveira', 'Monitoramento de aplicações é crucial.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (15, 'Carla Rodrigues', 'Adorei as dicas de Grafana.', GETDATE());

-- Comentários para o Post 16
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (16, 'Ricardo Almeida', 'DevOps na prática é muito interessante.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (16, 'Tatiane Costa', 'Post muito prático e útil.', GETDATE());

-- Comentários para o Post 17
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (17, 'Fábio Pereira', 'APIs são essenciais para integração.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (17, 'Larissa Lima', 'Ótimo conteúdo sobre desenvolvimento de APIs.', GETDATE());

-- Comentários para o Post 18
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (18, 'Sérgio Almeida', 'Grafana e Prometheus são ferramentas poderosas.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (18, 'Bianca Ribeiro', 'Adorei aprender sobre esta integração.', GETDATE());

-- Comentários para o Post 19
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (19, 'Guilherme Ferreira', 'CI/CD é essencial para qualquer projeto.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (19, 'Clarissa Souza', 'Muito bom aprender sobre GitHub Actions.', GETDATE());

-- Comentários para o Post 20
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (20, 'Alexandre Silva', 'C# 9 trouxe muitas novidades.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (20, 'Tatiana Oliveira', 'Ótima explicação sobre as novas funcionalidades.', GETDATE());


-- Comentários para o Post 21
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (21, 'Marcos Almeida', 'Ótima introdução ao assunto!', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (21, 'Cláudia Ferreira', 'Muito informativo, obrigado!', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (21, 'Rafael Souza', 'Vou começar a usar essa ferramenta no meu projeto.', GETDATE());

-- Comentários para o Post 22
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (22, 'Fernanda Lima', 'Quw ferramenta incrível.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (22, 'Carlos Pereira', 'Muito bom, aprendi bastante!', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (22, 'Tatiana Martins', 'Excelente conteúdo.', GETDATE());

-- Comentários para o Post 23
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (23, 'Luiz Henrique', 'Sensacional. Ja deixei seu post no favoritos.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (23, 'Amanda Costa', 'Gostei muito do conteúdo.', GETDATE());
INSERT INTO Comments (PostID, Author, Content, CreatedAt) VALUES (23, 'Paulo Roberto', 'Post muito bem explicado. Parabéns', GETDATE());
