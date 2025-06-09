-- Desabilitar verificação de chaves estrangeiras temporariamente
SET FOREIGN_KEY_CHECKS = 0;
USE kira2db;

-- Limpar tabelas existentes
TRUNCATE TABLE ProdutoCategoria;
TRUNCATE TABLE Carrinhos;
TRUNCATE TABLE Produtos;
TRUNCATE TABLE Marcas;
TRUNCATE TABLE Categorias;
TRUNCATE TABLE Usuarios;

-- Reiniciar IDs (opcional)
ALTER TABLE Usuarios AUTO_INCREMENT = 1;
ALTER TABLE Categorias AUTO_INCREMENT = 1;
ALTER TABLE Marcas AUTO_INCREMENT = 1;
ALTER TABLE Produtos AUTO_INCREMENT = 1;

-- Inserir Usuários
INSERT INTO Usuarios (Nome, Email, Senha, CPF, Tipo)
VALUES
("João Silva", "joao.silva@example.com", "202cb962ac59075b964b07152d234b70", "11122233344", 0),
("Maria Souza", "maria.souza@example.com", "202cb962ac59075b964b07152d234b70", "55566677788", 0),
("Admin Kira", "admin@kirashop.com", "202cb962ac59075b964b07152d234b70", "99988877766", 1); 

-- Inserir Categorias
INSERT INTO Categorias (Nome) VALUES
("Processadores"), ("Placas de Vídeo"), ("Memória RAM"), ("Armazenamento"),
("Placas-Mãe"), ("Fontes de Alimentação"), ("Gabinetes"),
("Monitores"), ("Periféricos"), ("Refrigeração");

-- Inserir Marcas
INSERT INTO Marcas (Nome) VALUES
("Intel"), ("AMD"), ("NVIDIA"), ("Kingston"), ("Corsair");

-- Inserir Produtos
INSERT INTO Produtos (Nome, Descricao, Preco, MarcaId, ImagemUrl)
VALUES
("Intel Core i9-13900K", "Processador de alta performance para jogos e produtividade.", 3500.00, 1, "https://www.gigantec.com.br/media/catalog/product/cache/66c3fa0fb26d248d0ca40a64a387c3da/p/r/processador_intel_core_i9_bx8071513900k-001.jpg"),
("Intel Core i7-13900K", "Processador de alta performance para jogos e produtividade.", 2500.00, 1, "https://www.gigantec.com.br/media/catalog/product/p/r/processador_intel_core_i7_13700f_bx8071513700f_002.jpg"),
("AMD Ryzen 9 7950X", "Processador de ponta com muitos núcleos para multitarefas.", 3200.00, 2, "https://images.kabum.com.br/produtos/fotos/426260/processador-amd-ryzen-9-7950x3d-5-7ghz-max-turbo-cache-144mb-am5-16-nucleos-video-integrado-100-100000908wof_1676550951_gg.jpg"),
("AMD Ryzen 7 7650X", "Processador de ponta com muitos núcleos para multitarefas.", 2200.00, 2, "hhttps://images3.kabum.com.br/produtos/fotos/378413/processador-amd-ryzen-7-7700x-5-4ghz-max-turbo-cache-40mb-am5-8-nucleos-video-integrado-100-100000591wof_1662136328_g.jpg"),
("NVIDIA GeForce RTX 4090", "Placa de vídeo top de linha para jogos em 4K.", 12000.00, 3, "https://images.kabum.com.br/produtos/fotos/387450/placa-de-video-asus-nvidia-tuf-gaming-geforce-rtx4090-edition-24-gb-gddr6x-dlss-ray-tracing-90yv0ie0-m0na00_1665170251_original.jpg"),
("NVIDIA GeForce RTX 3060", "Placa de vídeo custo-benefício para jogos em 1080p.", 4000.00, 3, "https://mirandacomputacao.jetassets.com.br/produto/49254-principal.png"),
("Kingston Fury Beast 16GB DDR5", "Módulo de memória RAM de alta velocidade.", 600.00, 4, "https://http2.mlstatic.com/D_865921-MLB84810145063_052025-C.jpg"),
("Kingston Fury Beast 32GB DDR5", "Módulo de memória RAM de alta velocidade.", 800.00, 4, "https://media.pichau.com.br/media/catalog/product/cache/2f958555330323e505eba7ce930bdf27/k/f/kf560c36bbe-32.jpg"),
("Corsair RM850e", "Fonte de alimentação modular de 850W com certificação 80 Plus Gold.", 850.00, 5, "https://cdn.shoppub.io/cdn-cgi/image/w=1000,h=1000,q=80,f=auto/oficinadosbits/media/uploads/produtos/foto/cuxeiebc/file.png"),
("Corsair RM650e", "Fonte de alimentação modular de 650W com certificação 80 Plus Gold.", 650.00, 5, "https://assets.corsair.com/image/upload/c_pad,q_auto,h_1024,w_1024,f_auto/products/Power-Supply-Units/CP-9020194-AR/Gallery/RM650_01.webp");

-- Associar Produtos a Categorias
INSERT INTO ProdutoCategoria (ProdutosId, CategoriasId)
VALUES
(1, 1), (2, 1),   -- Processadores
(3, 1), (4, 1),   -- Processadores (corrigido)
(5, 2), (6, 2),   -- Placas de vídeo
(7, 3), (8, 3),   -- Memória RAM
(9, 6), (10, 6);  -- Fontes de alimentação

-- Reativar checagem de chaves estrangeiras
SET FOREIGN_KEY_CHECKS = 1;
