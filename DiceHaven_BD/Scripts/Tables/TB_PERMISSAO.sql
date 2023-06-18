CREATE TABLE TB_PERMISSAO(
ID_PERMISSAO INT NOT NULL PRIMARY KEY,
DS_PERMISSAO VARCHAR(50) NOT NULL,
DS_DESCRICAO VARCHAR(200) NOT NULL
);



INSERT INTO TB_PERMISSAO 
VALUES
(1, 'PMS_Ver_Permissao', 'Permissão para visualizar permissoes de grupos e usuários'),
(2, 'PMS_Adm_Permissao', 'Permissão para vincular e desvincular permissões de grupos'),
(3, 'PMS_Ver_Grupos', 'Permissão para visualizar grupos e usuarios vinculados'),
(4, 'PMS_Adm_Grupos', 'Permissão para vincular e desvincular usuarios a grupos'),
(5, 'PMS_Adm_Fichas', 'Permissão para fazer todas operações relacionadas a suas fichas'),
(6, 'PMS_Ver_Fichas_Privadas', 'Permissão para visualizar todas as fichas do sistema')