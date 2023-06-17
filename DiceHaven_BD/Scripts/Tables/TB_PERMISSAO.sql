CREATE TABLE TB_PERMISSAO(
ID_PERMISSAO INT NOT NULL PRIMARY KEY,
DS_PERMISSAO VARCHAR(50) NOT NULL,
DS_DESCRICAO VARCHAR(200) NOT NULL
);

INSERT INTO TB_PERMISSAO 
VALUES
(1, 'PMS_Ver_Permissao', 'Permissão para visualizar permissoes de grupos e usuários'),
(2, 'PMS_Adm_Permissao', 'Permissão para vincular e desvincular permissões de grupos')